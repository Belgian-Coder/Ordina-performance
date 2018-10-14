using DotNetPerformance.Api.Filters;
using DotNetPerformance.Api.Models._base;
using DotNetPerformance.Business;
using DotNetPerformance.Shared.Swagger;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetPerformance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        public MailController(IMailProcessor mailProcessor)
        {
            _mailProcessor = mailProcessor ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(mailProcessor)} cannot be null.");
        }

        private readonly IMailProcessor _mailProcessor;

        /*
         * TODO 4: use StringBuilder to generate newsletters, use Task.WhenAll as optimization, use automatic dispose for HttpClient
         * 
         * We want to send a newsletter to all our clients
         * The newsletters need to be personalized with their firstname
         */
        // POST mail/newsletter/send
        [HttpPost("newsletter/send")]
        [Consumes(MediaType.Json)]
        [ValidateModelState]
        [ProducesResponseType(typeof(TimedProcess<object>), 200)]
        public async Task<IActionResult> SendNewsLetter([FromBody] string body)
        {
            if (String.IsNullOrWhiteSpace(body)) throw new ArgumentException($"Parameter {nameof(body)} cannot be empty.");

            var timedProcess = new TimedProcess<object>();
            timedProcess.StartTimer();

            await _mailProcessor.SendNewsletter(body);

            timedProcess.StopTimer();
            return Ok(timedProcess);
        }
    }
}
