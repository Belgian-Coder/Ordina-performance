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
    public class ProductsController : ControllerBase
    {
        public ProductsController(IProductReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(reader)} cannot be null.");
        }

        public readonly IProductReader _reader;

        /*
         * TODO 1: Rewrite the code in ProductReader into LINQ method syntax
         * 
         * We want to retrieve all products between a price range
         * These products need to be ordered ascending by price
         * The list will not be used for updates (no change tracking)
         */
        // GET statistics/productsinpricerangeascending
        [HttpGet("productsinpricerangeascending")]
        [Produces(MediaType.Json)]
        [ProducesResponseType(typeof(TimedProcess<IEnumerable<Product>>), 200)]
        public IActionResult Get([FromQuery] int lowerbound, [FromQuery] int upperbound)
        {
            var timedProcess = new TimedProcess<IEnumerable<Product>>();
            timedProcess.StartTimer();

            var result = _reader.GetProductsBetweenPriceRange(lowerbound, upperbound);

            timedProcess.StopTimer();
            timedProcess.Result = result;
            return Ok(timedProcess);
        }

        /*
         * TODO 2: Rewrite the code in ProductReader for more efficient data retrieval, use SQL profiler to compare execution plans
         * 
         * We want to retrieve all unique products for a specific warehouse
         * Products include their image for sharing with our external partners
         * All products are sorted by their productname
         */
        // GET statistics/productssold
        [HttpGet("productssold")]
        [Produces(MediaType.Json)]
        [ProducesResponseType(typeof(TimedProcess<IEnumerable<Product>>), 200)]
        public async Task<IActionResult> Get([FromQuery] int warehouseId)
        {
            var timedProcess = new TimedProcess<IEnumerable<Product>>();
            timedProcess.StartTimer();

            var result = await _reader.GetAllProductsSoldInWarehouse(warehouseId);

            timedProcess.StopTimer();
            timedProcess.Result = result;
            return Ok(timedProcess);
        }
    }
}
