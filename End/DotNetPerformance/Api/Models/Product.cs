namespace DotNetPerformance.Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AmountInStock { get; set; }

        public int? ImageId { get; set; }
        public Image Image { get; set; }

        public int? ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}