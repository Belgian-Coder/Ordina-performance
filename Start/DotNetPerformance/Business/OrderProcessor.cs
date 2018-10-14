using System;
using System.Collections.Generic;
using System.Linq;
using DotNetPerformance.Api.ViewModels.Statistics;
using DotNetPerformance.Entities;

namespace DotNetPerformance.Business
{
    public class OrderProcessor : IOrderProcessor
    {
        public OrderTotalAmount CalculateInvoiceTotal(Order order)
        {
            var products = order.OrderProducts;
            double total = 0.0;
            foreach (var product in products ?? Enumerable.Empty<OrderProduct>())
            {
                total += product.Amount * product.Price;
            }
            if (total > 1000)
                total *= 0.95;
            total *= 1.21;
            total *= 1.10;

            // some calculation
            Enumerable.Range(0, 100000).Aggregate(0d, (tot, next) => tot += Math.Pow(-1d, next) / (2 * next + 1) * 4);

            return new OrderTotalAmount
            {
                OrderId = order.Id,
                TotalExpense = total
            };
        }
    }
}
