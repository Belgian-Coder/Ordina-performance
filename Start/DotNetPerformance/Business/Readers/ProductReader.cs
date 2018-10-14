using AutoMapper;
using DotNetPerformance.Api.Models;
using DotNetPerformance.Data.Contexts;
using DotNetPerformance.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetPerformance.Business.Readers
{
    public class ProductReader: IProductReader
    {
        public ProductReader(WebshopContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(context)} cannot be null.");
            _mapper = mapper ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(mapper)} cannot be null.");
        }

        private readonly WebshopContext _context;
        private readonly IMapper _mapper;

        /*
         * Rewrite the code in ProductReader into LINQ method syntax
         * 
         * We want to retrieve all products between a price range
         * These products need to be ordered ascending by price
         * The list will not be used for updates (no change tracking)
         */
        public IEnumerable<Product> GetProductsBetweenPriceRange(int lowerbound, int upperbound)
        {
            var products = _context.Products.ToList();

            var filterdProducts = new List<Product>();
            foreach (var product in filterdProducts)
            {
                if (product.Price >= lowerbound && product.Price < upperbound)
                    filterdProducts.Add(product);
            }

            filterdProducts.Sort((a, b) =>
                a.Price.CompareTo(b.Price));

            return _mapper.Map<IEnumerable<Product>>(filterdProducts);
        }

        /*
         * Rewrite the code in ProductReader for more efficient data retrieval, use SQL profiler to compare execution plans
         * 
         * We want to retrieve all unique products for a specific warehouse
         * Products include their image for sharing with our external partners
         * All products are sorted by their productname
         */
        public async Task<IEnumerable<Product>> GetAllProductsSoldInWarehouse(int warehouseId)
        {
            var result = await _context.Orders
                .SelectMany(o => o.OrderProducts)
                .Select(op => op.Product)
                .Where(p => p.WarehouseId == warehouseId)
                .OrderBy(p => p.Name)
                .Include(p => p.Image)
                .Distinct()
                .ToListAsync();

            return _mapper.Map<IEnumerable<Product>>(result);
        }
    }
}
