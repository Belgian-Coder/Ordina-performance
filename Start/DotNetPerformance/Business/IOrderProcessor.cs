using DotNetPerformance.Api.ViewModels.Statistics;
using DotNetPerformance.Entities;
using System.Collections.Generic;

namespace DotNetPerformance.Business
{
    public interface IOrderProcessor
    {
        OrderTotalAmount CalculateInvoiceTotal(Order products);
    }
}
