using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class ProductCategoryViewModel
    {
        public string CategoryName { get; set; }
        public string ImageSrcMain { get; set; }
        public string ImageSrcSecond { get; set; }


    }
    public class ProductCardViewModel
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public string DailyRate { get; set; }

    }




}