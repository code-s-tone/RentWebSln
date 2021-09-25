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
        public string StoreName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatusID { get; set; }
        public string GoodsStatusID { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
