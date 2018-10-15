using DotNetPerformance.Api.ViewModels.Statistics;
using DotNetPerformance.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetPerformance.Business.Readers
{
    public interface IStatisticsReader
    {
        IEnumerable<ItemsSoldTop> GetItemsSoldTop(int numberOfItems);
        Task<IEnumerable<OrderTotalAmount>> GetTotalAmountPerInvoice(PageModel page);
    }
}
