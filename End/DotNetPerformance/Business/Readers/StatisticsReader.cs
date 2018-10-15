using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetPerformance.Api.ViewModels.Statistics;
using DotNetPerformance.Data.Contexts;
using DotNetPerformance.Entities;
using DotNetPerformance.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetPerformance.Business.Readers
{
    public class StatisticsReader : IStatisticsReader
    {
        public StatisticsReader(WebshopContext context, IMapper mapper, IOrderProcessor orderProcessor)
        {
            _context = context ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(context)} cannot be null.");
            _mapper = mapper ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(mapper)} cannot be null.");
            _orderProcessor = orderProcessor ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(orderProcessor)} cannot be null.");
        }

        private readonly WebshopContext _context;
        private readonly IMapper _mapper;
        private readonly IOrderProcessor _orderProcessor;

        /*
         * Try and fix the ClientQueryEvaluationException
         * 
         * We want to retrieve a given set of the best sold products
         * Best sold means the most amount of items sold
         */
        public IEnumerable<ItemsSoldTop> GetItemsSoldTop(int numberOfItems)
        {
            var topItems = (from op in _context.OrderProducts
                            group op by op.ProductId into opGroup
                            select new
                            {
                                ProductId = opGroup.Key,
                                AmountSold = opGroup.Sum(x => x.Amount),
                            })
                            .OrderByDescending(x => x.AmountSold)
                            .Take(numberOfItems)
                            .AsNoTracking()
                            .ToList();

            //var products = _context.Products.Where(prod => topItems.Select(ti => ti.ProductId).Contains(prod.Id)).AsNoTracking().ToList();

            var productIds = topItems.Select(ti => ti.ProductId);
            var products = _context.Products.Where(prod => productIds.Contains(prod.Id)).AsNoTracking().ToList();

            var results = new List<ItemsSoldTop>();
            for (var i = 0; i < topItems.Count; i++)
            {
                var topItem = topItems[i];
                results.Add(new ItemsSoldTop
                {
                    TopPosition = i + 1,
                    AmountSold = topItem.AmountSold,
                    Product = _mapper.Map<Api.Models.Product>(products.FirstOrDefault(p => p.Id == topItem.ProductId))
                });
            }
            return results;
        }

        /*
         * Optimize the calculation process by using multiple threads (8) for calculation
         * 
         * We want to know the total invoice values
         * This is calculated by taking all sold products and combine them
         * With 21% (VAT) and 10% (Handling cost)
         * Orders greater than 1,000 euro will receive a 5% deduction before calculating additional costs
         * 
         * Use blockingcollection
         */
        public async Task<IEnumerable<OrderTotalAmount>> GetTotalAmountPerInvoice(PageModel page)
        {
            var orders = await _context.Orders
                .Skip((page.Page - 1) * page.PageSize)
                .Take(page.PageSize)
                .Include(o => o.OrderProducts)
                .AsNoTracking()
                .ToListAsync();

            //List<OrderTotalAmount> result = new List<OrderTotalAmount>(orders.Count);
            //foreach (var order in orders ?? Enumerable.Empty<Order>())
            //{
            //    result.Add(_orderProcessor.CalculateInvoiceTotal(order));
            //}

            //return result;

            var result = new BlockingCollection<OrderTotalAmount>(orders.Count);

            Parallel.ForEach(
                orders,
                new ParallelOptions { MaxDegreeOfParallelism = 8 }, // how many threads will be used at one moment
                order =>
                {
                    result.Add(_orderProcessor.CalculateInvoiceTotal(order));
                }
            );

            return result;
        }
    }
}
