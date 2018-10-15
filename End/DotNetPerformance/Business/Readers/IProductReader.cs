using DotNetPerformance.Api.Models;
using DotNetPerformance.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetPerformance.Business.Readers
{
    public interface IProductReader
    {
        IEnumerable<Product> GetProductsBetweenPriceRange(int lowerbound, int upperbound);
        Task<IEnumerable<Product>> GetAllProductsSoldInWarehouse(int warehouseId);
    }
}
