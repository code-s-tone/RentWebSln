using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class ProductCategoryView
    {
        public string CategoryName { get; set; }
        public string ImageSrc { get; set; }

    }
    public class ProductCardView
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
    }

    //public class ProductCartsView2 
    //    //先這樣取名 不確定購物車在indexService要怎麼變動
    //{
    //    public string ImageSrc { get; set; }

    //    public string ProductName { get; set; }

    //    public decimal DailyRate { get; set; }

    //    public DateTime StartDate { get; set; }
    //    public DateTime ExpirationDate { get; set; }
    //    public decimal TotalAmount { get; set; }

    //}

}