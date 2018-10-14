using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetPerformance.Api.Models;
using DotNetPerformance.Data.Contexts;
using DotNetPerformance.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetPerformance.Business.Readers
{
    public class CustomerReader : ICustomerReader
    {
        public CustomerReader(WebshopContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(context)} cannot be null.");
            _mapper = mapper ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(mapper)} cannot be null.");
        }

        private readonly WebshopContext _context;
        private readonly IMapper _mapper;

        public async Task<PagedResult<Customer>> GetCustomersAsync(PageModel page)
        {
            var result = new PagedResult<Customer>();

            var customers = await _context.Customers
                .Skip((page.Page - 1) * page.PageSize)
                .Take(page.PageSize)
                .ToListAsync();

            result.Items = _mapper.Map<IEnumerable<Customer>>(customers);
            result.Page = page.Page;
            result.PageSize = page.PageSize;
            result.PageItems = customers.Count;
            result.TotalItems = await _context.Customers.CountAsync();

            return result;
        }
    }
}
