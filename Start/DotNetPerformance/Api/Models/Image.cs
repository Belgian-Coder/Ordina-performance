namespace DotNetPerformance.Api.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] Blob { get; set; }
    }
}
