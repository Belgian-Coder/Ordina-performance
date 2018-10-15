using DotNetPerformance.Entities;
using DotNetPerformance.Shared.Constants;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPerformance.Data.Seeders
{
    public static class AddressSeeder
    {
        private static List<Address> _addresses;

        static AddressSeeder()
        {
            _addresses = new List<Address>();
        }

        public static IList<Address> GetSeedList()
        {
            Build();
            return _addresses;
        }

        public static int Count()
        {
            Build();
            return _addresses.Count();
        }

        private static void Build()
        {
            if (_addresses.Any()) return;
            List<Address> addresses = new List<Address>();
            addresses.Add(BuildItem(1, "Antwerpen", "2000", "Meir 87" ));
            addresses.Add(BuildItem(2, "Heusden-Zolder", "3560", "Klaverbladstraat 11" ));
            addresses.Add(BuildItem(3, "Mechelen", "2800", "Blarenberglaan 3B" ));
            addresses.Add(BuildItem(4, "Gent", "9000", "Ottergemsesteenweg Zuid 808" ));
            addresses.Add(BuildItem(5, "Antwerpen", "2000", "Huidevetersstraat 60" ));
            addresses.Add(BuildItem(6, "Bruxelles", "1020", "Square de l'Atomium 1" ));
            _addresses = addresses;
        }

        private static Address BuildItem(int id, string city, string postalCode, string street)
        {
            var item = new Address();
            if (Startup.Startup.DbMode == DbModeEnum.Unconfigured) item.Id = id;
            item.City = city;
            item.PostalCode = postalCode;
            item.Street = street;
            return item;
        }
    }
}
