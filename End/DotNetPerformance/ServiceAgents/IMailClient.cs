using System.Threading.Tasks;

namespace DotNetPerformance.ServiceAgents
{
    public interface IMailClient
    {
        Task SendMail(string html, string email);
    }
}
