using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.Services;

namespace RentWebProj.ViewModels
{
    public class ProductDetailToCart
    {
        public string DateTimeFormat;

        public ProductDetailToCart()
        {
            this.DateTimeFormat = "yyyy / MM / dd HH:mm";
        }

        //public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal DailyRate { get; set; }

        //圖片群 來自ProductImage
        public List<string> ImgSources { get; set; }

        //public int? CurrentMemberID { get; set; }
        //禁用日期
        public string DisablePeriodsJSON { get; set; }

        //來自Cart
        public bool IsExisted { get; set; }
        //前端無法使用DateTime型別
        public string StartDate { get; set; }
        public string ExpirationDate { get; set; }
        //操作結果
        public string OperationType { get; set; }
        public bool OperationSuccessful { get; set; }
        
    }

    public class DisablePeriod
    {
        public string from { get; set; }
        public string to { get; set; }
        //JS陣列：{
        //            from: "2025-04-01",
        //            to: "2025-05-01"
        //        }
    }

    public class OrderDetail_TotalAmount
    {
        public int OrderID { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class CartIndex
    {

        public int MemberID { get; set; }

        public string ProductID { get; set; }

        public string ProductName { get; set; }

        //祥聖: 是不是要加產品圖片? >_<
        public string ImgSources { get; set; }

        //禁租日期
        public string DisablePeriodsJSON { get; set; }

        //來自產品列表頁的加入，日期可能為null
        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public decimal DailyRate { get; set; }

        public int Qty { get; set; }

        public int DateDiff { get; set; }

        public decimal Sub { get; set; }

        


        //建立訂單用
        public int StoreID { get; set; }

    }
}