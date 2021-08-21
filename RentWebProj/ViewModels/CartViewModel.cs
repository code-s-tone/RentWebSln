using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class ProductDetailToCart
    {
        
        public int? CurrentMemberID { get; set; }
        //public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal DailyRate { get; set; }

        //圖片群 來自ProductImage
        public List<string> ImgSources { get; set; }

        //來自Cart
        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }


    public class CartViewModel
    {
    }


}