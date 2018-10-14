using DotNetPerformance.Entities;
using DotNetPerformance.Shared.Constants;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPerformance.Data.Seeders
{
    public static class WarehouseSeeder
    {
        private static List<Warehouse> _warehouses;

        static WarehouseSeeder()
        {
            _warehouses = new List<Warehouse>();
            Build();
        }

        public static IList<Warehouse> GetSeedList()
        {
            Build();
            return _warehouses;
        }

        public static int Count()
        {
            Build();
            return _warehouses.Count();
        }

        private static void Build()
        {
            if (_warehouses.Any()) return;
            List<Warehouse> warehouses = new List<Warehouse>();
            warehouses.Add(BuildItem(1, "Magazijn Antwerpen", 1));
            warehouses.Add(BuildItem(2, "Magazijn Heusden-Zolder", 2));
            _warehouses = warehouses;
        }

        private static Warehouse BuildItem(int id, string name, int addressId)
        {
            var item = new Warehouse();
            if (Startup.Startup.DbMode == DbModeEnum.Unconfigured) item.Id = id;
            item.AddressId = addressId;
            item.Name = name;
            return item;
        }
    }
}
