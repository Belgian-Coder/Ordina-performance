using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPerformance.Entities
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int? AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
