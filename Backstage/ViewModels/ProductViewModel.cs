using Backstage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.ViewModels
{
    public class ProductViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal DailyRate { get; set; }
        public DateTime? LaunchDate { get; set; }
        public DateTime? WithdrawalDate { get; set; }
        public bool? Discontinuation { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual List<ImageViewModel> ProductImages { get; set; }
    }

    public class ImageViewModel
    {
        public string SourceImages { get; set; }
    }
}
