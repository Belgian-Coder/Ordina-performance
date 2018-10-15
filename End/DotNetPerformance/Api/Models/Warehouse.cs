using System.Collections.Generic;

namespace DotNetPerformance.Api.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
