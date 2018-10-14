using System.Text;
using System.Threading.Tasks;

namespace DotNetPerformance.Business
{
    public class MailGenerator : IMailGenerator
    {
        private readonly StringBuilder _builder = new StringBuilder();

        /*
         * Optimize the code by using stringbuilder
         */
        public string GenerateNewsletter(string body, string firstname)
        {
            var html = "<html>";
            html += "<body>";
            html += body;
            html = html.Replace("{firstname}", $"<em>{firstname}</em>");
            html += "</body>" + "</html>";
            return html;
        }
    }
}
