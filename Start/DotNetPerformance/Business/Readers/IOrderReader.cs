using DotNetPerformance.Api.Models;
using DotNetPerformance.Shared.Models;
using System.Threading.Tasks;

namespace DotNetPerformance.Business.Readers
{
    public interface IOrderReader
    {
        PagedResult<Order> GetAllOrders(PageModel page);
    }
}
