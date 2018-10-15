using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetPerformance.Api.Models._base;
using DotNetPerformance.Api.ViewModels.Statistics;
using DotNetPerformance.Business.Readers;
using DotNetPerformance.Shared.Models;
using DotNetPerformance.Shared.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPerformance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        public StatisticsController(IStatisticsReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(reader)} cannot be null.");
        }

        public readonly IStatisticsReader _reader;

        /*
         * TODO 5: Try and fix the ClientQueryEvaluationException
         * 
         * We want to retrieve a given set of the best sold products
         * Best sold means the most amount of items sold
         */
        // GET statistics/products/topsold
        [HttpGet("products/topsold")]
        [Produces(MediaType.Json)]
        [ProducesResponseType(typeof(TimedProcess<IEnumerable<ItemsSoldTop>>), 200)]
        public IActionResult GetTopSoldProducts([FromQuery] int numberOfItems)
        {
            var timedProcess = new TimedProcess<IEnumerable<ItemsSoldTop>>();
            timedProcess.StartTimer();

            var result = _reader.GetItemsSoldTop(numberOfItems);

            timedProcess.StopTimer();
            timedProcess.Result = result;
            return Ok(timedProcess);
        }

        /*
         * TODO 6: Optimize the calculation process by using multiple threads for calculation
         * 
         * We want to know the total invoice values
         * This is calculated by taking all sold products and combine them
         * With 21% (VAT) and 10% (Handling cost)
         * Orders greater than 1,000 euro will receive a 5% deduction
         */
        // GET statistics/order/totalamount
        [HttpGet("order/totalamount")]
        [Produces(MediaType.Json)]
        [ProducesResponseType(typeof(TimedProcess<IEnumerable<OrderTotalAmount>>), 200)]
        public async Task<IActionResult> GetOrdersTotalAmount([FromQuery] PageModel page)
        {
            var timedProcess = new TimedProcess<IEnumerable<OrderTotalAmount>>();
            timedProcess.StartTimer();

            var result = await _reader.GetTotalAmountPerInvoice(page);

            timedProcess.StopTimer();
            timedProcess.Result = result;
            return Ok(timedProcess);
        }
    }
}
