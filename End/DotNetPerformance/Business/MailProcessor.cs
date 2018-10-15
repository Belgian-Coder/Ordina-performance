using DotNetPerformance.Business.Readers;
using DotNetPerformance.ServiceAgents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetPerformance.Business
{
    public class MailProcessor : IMailProcessor
    {
        public MailProcessor(ICustomerReader customerReader, IMailGenerator mailGenerator, IMailClient mailClient)
        {
            _customerReader = customerReader ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(customerReader)} cannot be null.");
            _mailGenerator = mailGenerator ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(mailGenerator)} cannot be null.");
            _mailClient = mailClient ?? throw new ArgumentNullException($"{GetType().Name}.Ctor - parameter {nameof(mailClient)} cannot be null.");
        }

        private readonly ICustomerReader _customerReader;
        private readonly IMailGenerator _mailGenerator;
        private readonly IMailClient _mailClient;


        /*
         * Use Task.WhenAll to optimize the code to send newsletters
         * 
         * Optimize the GenerateNewsletter method by using StringBuilder
         * Refactor the HttpClient in SendMail to automatically dispose itself
         */
        public async Task SendNewsletter(string body)
        {
            var batchSize = 100;
            var page = 1;
            var areItemsLeft = true;

            while (areItemsLeft)
            {
                var customers = await _customerReader.GetCustomersAsync(
                    new Shared.Models.PageModel() {
                        Page = page,
                        PageSize = batchSize
                    });

                if (customers.PageItems == 0)
                {
                    areItemsLeft = false;
                    break;
                }

                //foreach (var customer in customers.Items)
                //{
                //    var html = _mailGenerator.GenerateNewsletter(body, customer.FirstName);
                //    await _mailClient.SendMail(html, customer.Email);
                //}

                var tasks = new List<Task>(customers.PageItems);
                foreach (var customer in customers.Items)
                {
                    var html = _mailGenerator.GenerateNewsletter(body, customer.FirstName);
                    tasks.Add(_mailClient.SendMail(html, customer.Email));
                }

                await Task.WhenAll(tasks);

                page++;
            }
        }
    }
}
