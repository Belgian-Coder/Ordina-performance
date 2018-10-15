using DotNetPerformance.Api.Models;
using DotNetPerformance.Shared.Models;
using System.Threading.Tasks;

namespace DotNetPerformance.Business.Readers
{
    public interface ICustomerReader
    {
        Task<PagedResult<Customer>> GetCustomersAsync(PageModel page);
    }
}
