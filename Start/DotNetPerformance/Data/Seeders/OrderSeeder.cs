using System.Collections.Generic;
using System.Linq;
using DotNetPerformance.Entities;
using DotNetPerformance.Shared.Constants;

namespace DotNetPerformance.Data.Seeders
{
    public static class OrderSeeder
    {
        private static List<Order> _orders;

        static OrderSeeder()
        {
            _orders = new List<Order>();
        }

        public static IList<Order> GetSeedList()
        {
            Build();
            return _orders;
        }

        public static int Count()
        {
            Build();
            return _orders.Count();
        }

        private static void Build()
        {
            if (_orders.Any()) return;
            var addressCount = AddressSeeder.Count();
            var customerCount = CustomerSeeder.Count();
            var orders = new List<Order>();

            for (var i = 1; i <= 1500; i++)
            {
                var customerId = (customerCount - i) % i;
                if (!(customerId > 0 && customerId < customerCount)) customerId = 25;
                var deliveryAddressId = i % addressCount + 1;
                if (deliveryAddressId < 2) deliveryAddressId = 3;
                if (deliveryAddressId > addressCount) deliveryAddressId = addressCount - 1;
                var invoiceAddressId = i % 5 == 0 ? 3 : deliveryAddressId;

                orders.Add(BuildItem(i, customerId, deliveryAddressId, invoiceAddressId));
            }

            _orders = orders;
        }

        private static Order BuildItem(int id, int customerId, int deliveryAddressId, int invoiceAddressId)
        {
            var item = new Order();
            if (Startup.Startup.DbMode == DbModeEnum.Unconfigured) item.Id = id;
            item.CustomerId = customerId;
            item.DeliveryAddressId = deliveryAddressId;
            item.InvoiceAddressId = invoiceAddressId;
            return item;
        }
    }
}
