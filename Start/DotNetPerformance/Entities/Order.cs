using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetPerformance.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }

        public int? DeliveryAddressId { get; set; }
        [ForeignKey(nameof(DeliveryAddressId))]
        public virtual Address DeliveryAddress { get; set; }

        public int? InvoiceAddressId { get; set; }
        [ForeignKey(nameof(InvoiceAddressId))]
        public virtual Address InvoiceAddress { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
