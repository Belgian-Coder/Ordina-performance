using System.Net.Http;
using System.Threading.Tasks;

namespace DotNetPerformance.ServiceAgents
{
    public class MailClient: IMailClient
    {
        /*
         * Refactor the HttpClient to automatically dispose itself
         */
        public async Task SendMail(string html, string email)
        {
            var client = new HttpClient();
            // here you would find the logic for sending the e-mail

            await Task.Delay(5);
            client.Dispose();
        }
    }
}
