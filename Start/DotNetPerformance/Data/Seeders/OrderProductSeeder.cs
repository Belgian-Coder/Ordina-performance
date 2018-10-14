using System.Collections.Generic;
using System.Linq;
using DotNetPerformance.Entities;

namespace DotNetPerformance.Data.Seeders
{
    public static class OrderProductSeeder
    {
        private static List<OrderProduct> _orderProducts;

        static OrderProductSeeder()
        {
            _orderProducts = new List<OrderProduct>();
        }

        public static IList<OrderProduct> GetSeedList()
        {
            Build();
            return _orderProducts;
        }

        public static int Count()
        {
            Build();
            return _orderProducts.Count();
        }

        private static void Build()
        {
            if (_orderProducts.Any()) return;
            var productsCount = ProductSeeder.Count();
            var ordersCount = OrderSeeder.Count();
            var amounts = new[] { 1, 2, 3, 5, 10 };
            var prices = new[] { 5, 10, 15, 20, 25, 30, 40, 50 };
            var orderProducts = new List<OrderProduct>();

            for (var i = 1; i <= ordersCount; i++)
            {
                var offset = i % 2 == 0 ? (productsCount - i) % 7 : 0;
                var numberOfProducts = ((i + offset) % 5 * 4);

                for (var j = 1; j <= numberOfProducts; j++)
                {
                    var price = prices[(i + j) % prices.Length];
                    var amount = amounts[(i + j + offset) % amounts.Length];
                    var productId = (price * amount * i * j * 7) % productsCount;
                    productId = productId == 0 ? 5 : productId;

                    if (orderProducts.Any(x => x.ProductId == productId && x.OrderId == i)) continue;

                    orderProducts.Add(new OrderProduct()
                    {
                        ProductId = productId,
                        Amount = amount,
                        OrderId = i,
                        Price = price
                    });
                }
            }

            _orderProducts = orderProducts;
        }
    }
}
