using System.Threading.Tasks;

namespace DotNetPerformance.Business
{
    public interface IMailProcessor
    {
        Task SendNewsletter(string body);
    }
}
