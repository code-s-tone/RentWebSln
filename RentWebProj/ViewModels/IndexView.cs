using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    //祥聖在使用嗎，該移去別處?
    public class ProductCartsView
    {
        public string ImageSrc { get; set; }

        public string ProductName { get; set; }

        public decimal DailyRate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}