using System.Threading.Tasks;

namespace DotNetPerformance.Business
{
    public interface IMailGenerator
    {
        string GenerateNewsletter(string body, string firstname);
    }
}
