using System.Collections.Generic;

namespace DotNetPerformance.Api.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }

        public int? InvoiceAddressId { get; set; }
        public Address InvoiceAddress { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
