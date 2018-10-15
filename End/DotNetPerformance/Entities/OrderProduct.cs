namespace DotNetPerformance.Entities
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Amount { get; set; }
        public int Price { get; set; }
    }
}
