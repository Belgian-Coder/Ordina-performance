using System.ComponentModel.DataAnnotations;

namespace DotNetPerformance.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
