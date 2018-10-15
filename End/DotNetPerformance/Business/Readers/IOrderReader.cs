using DotNetPerformance.Api.Models;
using DotNetPerformance.Shared.Models;
using System.Threading.Tasks;

namespace DotNetPerformance.Business.Readers
{
    public interface IOrderReader
    {
        Task<PagedResult<Order>> GetAllOrdersAsync(PageModel page);
        //PagedResult<Order> GetAllOrders(PageModel page);
    }
}
