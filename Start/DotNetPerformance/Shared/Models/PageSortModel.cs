namespace DotNetPerformance.Shared.Models
{
    public class PageSortModel : PageModel
    {
        public string SortingField { get; set; }
        public bool? ReverseSorting { get; set; } = false;
    }
}
