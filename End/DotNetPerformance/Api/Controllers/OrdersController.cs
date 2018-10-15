using System.Collections.Generic;
using DotNetPerformance.Api.Filters;
using DotNetPerformance.Shared.Swagger;
using DotNetPerformance.Api.Models;
using DotNetPerformance.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DotNetPerformance.Business.Readers;
using System;
using DotNetPerformance.Api.Models._base;

namespace DotNetPerformance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrdersController(IOrderReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(reader)} cannot be null.");
        }

        public readonly IOrderReader _reader;

        /*
         * TODO 3: Make this method async
         * 
         * Retrieve a paged set of orders
         */
        // GET orders?page=1&pagesize=10&sortingfield=id&reversesorting=false
        [HttpGet]
        [Produces(MediaType.Json)]
        [ProducesResponseType(typeof(TimedProcess<PagedResult<Order>>), 200)]
        //public IActionResult Get([FromQuery] PageModel filter)
        //{
        //    var timedProcess = new TimedProcess<PagedResult<Order>>();
        //    timedProcess.StartTimer();

        //    var result = _reader.GetAllOrders(filter);

        //    timedProcess.StopTimer();
        //    timedProcess.Result = result;
        //    return Ok(timedProcess);
        //}
        public async Task<IActionResult> Get([FromQuery] PageModel filter)
        {
            var timedProcess = new TimedProcess<PagedResult<Order>>();
            timedProcess.StartTimer();

            var result = await _reader.GetAllOrdersAsync(filter);

            timedProcess.StopTimer();
            timedProcess.Result = result;
            return Ok(timedProcess);
        }
    }
}
