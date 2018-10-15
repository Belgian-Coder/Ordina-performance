using System.ComponentModel.DataAnnotations;

namespace DotNetPerformance.Entities
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
    }
}
