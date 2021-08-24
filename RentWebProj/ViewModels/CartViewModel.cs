using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Services;

namespace RentWebProj.ViewModels
{

    public class ProductDetailToCart
    {
        public string dateTimeFormat;

        public ProductDetailToCart()
        {
            this.dateTimeFormat = "yyyy / MM / dd HH:mm";
        }

        //public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal DailyRate { get; set; }

        //圖片群 來自ProductImage
        public List<string> ImgSources { get; set; }

        //public int? CurrentMemberID { get; set; }
        //禁用日期
        public string[] disableDateArray { get; set; }

        //來自Cart
        public bool isExisted { get; set; }

        //前端無法使用DateTime型別
        public string StartDate { get; set; }
        public string ExpirationDate { get; set; }
        //操作結果
        public string OperationType { get; set; }
        public bool OperationSuccessful { get; set; }
        
    }



}