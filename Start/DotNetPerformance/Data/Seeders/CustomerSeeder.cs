using System.Collections.Generic;
using System.Linq;
using DotNetPerformance.Entities;
using DotNetPerformance.Shared.Constants;

namespace DotNetPerformance.Data.Seeders
{
    public static class CustomerSeeder
    {
        private static List<Customer> _customers;

        static CustomerSeeder()
        {
            _customers = new List<Customer>();
        }

        public static IList<Customer> GetSeedList()
        {
            Build();
            return _customers;
        }

        public static int Count()
        {
            Build();
            return _customers.Count();
        }

        private static void Build()
        {
            if (_customers.Any()) return;
            var firstNames = new string[]
            {
                "Ella", "Arthur", "Marie", "Leon", "Louise", "Liam", "Mila", "Louis", "Elise", "Victor",
                "Lina", "Emma", "Juliette", "Anna", "Noor", "Adam", "Lars", "Jules", "Noah", "Gabriel",
                "Aline", "Simon", "Bjorn", "Ellen", "Birgit", "Paul", "Bert", "Koen", "Claude", "Greta",
                "Dave", "Rudy", "Christel", "Nathalie", "Shana", "Wesley", "Annie"
            };
            var lastNames = new string[]
            {
                "de Jong", "Janssens", "de Vries", "van den Berg", "Bakker", "Visser", "Smit", "Jacobs", "de Bruin",
                "Koster", "Hoekstra", "Maes", "Kuipers", "Peeters", "Willems", "Mertens", "Claes", "Goossens", "Wouters",
                "Leclerq", "Dubois", "Dumont", "Lejeune", "Lambert", "Van Leemput", "Moris", "Weyers", "Van Camp",
                "Gerritsen", "Barens", "Demoor", "Nauelaerts", "De Bondt", "Meijs", "Vanhoof"
            };
            List<Customer> customers = new List<Customer>();
            var lastId = 0;
            foreach (var lastName in lastNames)
            {
                foreach (var firstName in firstNames)
                {
                    customers.Add(BuildItem(++lastId, firstName, lastName, "0400/99.88.77"));
                }
            }
            _customers = customers;
        }

        public static Customer BuildItem(int id, string firstName, string lastName, string telephone)
        {
            var item = new Customer();
            if (Startup.Startup.DbMode == DbModeEnum.Unconfigured) item.Id = id;
            item.Email = $"{firstName}.{lastName}@telenet.be";
            item.Email = item.Email.Replace(' ', '.');
            item.FirstName = firstName;
            item.LastName = lastName;
            item.Telephone = telephone;
            return item;
        }
    }
}
