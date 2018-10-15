using System.Collections.Generic;

namespace DotNetPerformance.Shared.Models
{
    public class PagedResult<T> : PageModel
    {
        public IEnumerable<T> Items { get; set; }
        public int PageItems { get; set; }
        public int TotalItems { get; set; }
    }
}
