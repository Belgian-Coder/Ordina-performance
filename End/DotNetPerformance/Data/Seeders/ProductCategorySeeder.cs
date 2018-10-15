using DotNetPerformance.Entities;
using DotNetPerformance.Shared.Constants;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPerformance.Data.Seeders
{
    public static class ProductCategorySeeder
    {
        private static List<ProductCategory> _productCategories;

        static ProductCategorySeeder()
        {
            _productCategories = new List<ProductCategory>();
        }

        public static IList<ProductCategory> GetSeedList()
        {
            Build();
            return _productCategories;
        }

        public static int Count()
        {
            Build();
            return _productCategories.Count();
        }

        private static void Build()
        {
            if (_productCategories.Any()) return;
            List<ProductCategory> categories = new List<ProductCategory>();
            categories.Add(BuildItem(1, "T-shirts", "Some description about T-shirts"));
            categories.Add(BuildItem(2, "Shirts", "Some description about Shirts" ));
            categories.Add(BuildItem(3, "Pants", "Some description about Pants" ));
            categories.Add(BuildItem(4, "Suits", "Some description about Suitsts" ));
            categories.Add(BuildItem(5, "Sweaters", "Some description about Sweaters" ));
            categories.Add(BuildItem(6, "Shorts", "Some description about Shorts" ));
            categories.Add(BuildItem(7, "Socks", "Some description about Socks" ));
            categories.Add(BuildItem(8, "Underwear", "Some description about " ));
            categories.Add(BuildItem(9, "Swimwear", "Some description about Swimwear" ));
            categories.Add(BuildItem(10, "Jeans", "Some description about Jeans" ));
            categories.Add(BuildItem(11, "Jackets", "Some description about Jackets" ));
            _productCategories = categories;
        }

        private static ProductCategory BuildItem(int id, string name, string description)
        {
            var item = new ProductCategory();
            if (Startup.Startup.DbMode == DbModeEnum.Unconfigured) item.Id = id;
            item.Name = name;
            item.Description = description;
            return item;
        }
    }
}
