using AutoMapper;
using DotNetPerformance.Api.Models;
using DotNetPerformance.Data.Contexts;
using DotNetPerformance.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetPerformance.Business.Readers
{
    public class OrderReader: IOrderReader
    {
        public OrderReader(WebshopContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(context)} cannot be null.");
            _mapper = mapper ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(mapper)} cannot be null.");
        }

        private readonly WebshopContext _context;
        private readonly IMapper _mapper;

        /*
         * Make this method async
         * 
         * Retrieve a paged set of orders
         */
        public PagedResult<Order> GetAllOrders(PageModel page)
        {
            var result = new PagedResult<Order>();

            var orders = _context.Orders
                .Skip((page.Page - 1) * page.PageSize)
                .Take(page.PageSize)
                .ToList();
            result.Items = _mapper.Map<IEnumerable<Order>>(orders);
            result.Page = page.Page;
            result.PageSize = page.PageSize;
            result.PageItems = orders.Count;
            result.TotalItems = _context.Orders.Count();

            return result;
        }
    }
}
