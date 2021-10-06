using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.ViewModels
{
    public class OrderViewModel
    {
        //OID、MID、會員、分店、電話、信箱、金額、付款狀態、出貨狀態、日期
        public int OrderID { get; set; }
        public int MemberID { get; set; }
        public string FullName { get; set; }
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderStatusID { get; set; }
        public string GoodsStatusID { get; set; }
        public DateTime OrderDate { get; set; }

        //訂單細節

        public string ProductName { get; set; }
        public decimal DailyRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal TotalAmount { get; set; }
    }


    //錯誤訊息model 暫放
    public class EditOrderListResponseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
