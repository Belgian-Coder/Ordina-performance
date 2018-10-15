using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetPerformance.Api.Models;

namespace DotNetPerformance.Api.ViewModels.Statistics
{
    public class ItemsSoldTop
    {
        public int TopPosition { get; set; }
        public int AmountSold { get; set; }
        public Product Product { get; set; }
    }
}
