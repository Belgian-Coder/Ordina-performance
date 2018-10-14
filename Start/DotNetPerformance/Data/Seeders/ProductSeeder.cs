using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetPerformance.Entities;
using DotNetPerformance.Shared.Constants;

namespace DotNetPerformance.Data.Seeders
{
    public static class ProductSeeder
    {
        private static List<Product> _products;

        static ProductSeeder()
        {
            _products = new List<Product>();
            Build();
        }

        public static IList<Product> GetSeedList()
        {
            Build();
            return _products;
        }

        public static int Count()
        {
            Build();
            return _products.Count();
        }

        private static void Build()
        {
            if (_products.Any()) return;
            var categories = ProductCategorySeeder.GetSeedList();
            var colors = new List<string> {"Red", "Green", "Blue", "Orange", "Yellow", "Purple", "White", "Black", "Pink", "Fuchsia", "Silver", "Gray", "Olive", "Maroon", "Aqua", "Lime", "Teal", "Navy", "Brown", "Peach", "Gold"};
            var sizes = new List<string> {"XXS", "XS", "SM", "M", "L", "XL", "XXL" };
            var colorsAndSizes = new List<string>();
            colors.ForEach(color => sizes.ForEach(size => colorsAndSizes.Add($"{color} - {size}")));
            var sb = new StringBuilder();
            var products = new List<Product>();

            var id = 1;
            for (var i = 1; i <= categories.Count; i++)
            {
                var category = categories[i - 1];
                var categoryPrice = i * 10;
                var price = categoryPrice > 50 ? 20 : categoryPrice;

                foreach (var colorsAndSize in colorsAndSizes)
                {
                    sb.Clear();
                    sb.Append(category.Name.EndsWith("s")
                        ? category.Name.Substring(0, category.Name.Length - 1)
                        : category.Name);
                    sb.Append($" {colorsAndSize}");
                    var amountInStock = id % 2 == 0 ? (50) + (id * 77) % 100 : 100 - (id * 77 )% 33;

                    products.Add(BuildItem(id, sb.ToString(), id, amountInStock, sb.ToString(), price, i,
                        id % 2 == 0 ? 1 : 2));

                    id++;
                }
            }
            _products = products;
        }

        private static Product BuildItem(int id, string description, int imageId, int amountInStock, string name,
            int price, int productCategoryId, int warehouseId)
        {
            var item = new Product();
            if (Startup.Startup.DbMode == DbModeEnum.Unconfigured) item.Id = id;
            item.AmountInStock = amountInStock;
            item.Description = description;
            item.ImageId = imageId;
            item.AmountInStock = amountInStock;
            item.Name = name;
            item.Price = price;
            item.ProductCategoryId = productCategoryId;
            item.WarehouseId = warehouseId;
            return item;
        }
    }
}
