using System.ComponentModel.DataAnnotations;

namespace DotNetPerformance.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] Blob { get; set; }
    }
}
