using DotNetPerformance.Data.Contexts;
using System;
using System.Linq;
using DotNetPerformance.Data._base;

namespace DotNetPerformance.Data.Seeders
{
    public class WebshopDbInitializer : BaseDbInitializer<WebshopContext>
    {
        public override void Initialize(IServiceProvider serviceProvider, WebshopContext context)
        {
            base.Initialize(serviceProvider, context);
            // check if database is healthy
            if (!DbIsHealthy(context)) base.RecreateDatabase(context); else return;

            // seed data
            var productCategories = ProductCategorySeeder.GetSeedList();
            context.ProductCategories.AddRange(productCategories);
            context.SaveChanges();

            var addresses = AddressSeeder.GetSeedList();
            context.Addresses.AddRange(addresses);
            context.SaveChanges();

            var customers = CustomerSeeder.GetSeedList();
            context.Customers.AddRange(customers);
            context.SaveChanges();

            var images = ImageSeeder.GetSeedList();
            context.Images.AddRange(images);
            context.SaveChanges();

            var warehouses = WarehouseSeeder.GetSeedList();
            context.Warehouses.AddRange(warehouses);
            context.SaveChanges();

            var products = ProductSeeder.GetSeedList();
            context.Products.AddRange(products);
            context.SaveChanges();

            var orders = OrderSeeder.GetSeedList();
            context.Orders.AddRange(orders);
            context.SaveChanges();

            var orderProducts = OrderProductSeeder.GetSeedList();
            context.OrderProducts.AddRange(orderProducts);
            context.SaveChanges();
        }

        public bool DbIsHealthy(WebshopContext context)
        {
            return context.ProductCategories.Count() == ProductCategorySeeder.Count() 
                && context.Addresses.Count() == AddressSeeder.Count() 
                && context.Customers.Count() == CustomerSeeder.Count() 
                && context.Images.Count() == ImageSeeder.Count() 
                && context.Warehouses.Count() == WarehouseSeeder.Count() 
                && context.Products.Count() == ProductSeeder.Count() 
                && context.Orders.Count() == OrderSeeder.Count() 
                && context.OrderProducts.Count() == OrderProductSeeder.Count();
        }
    }
}
