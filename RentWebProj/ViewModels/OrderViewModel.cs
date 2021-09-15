using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class RentedPeriod
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }

    public class OrderDoubleCheck
    {
        public List<string> ListProductID { get; set; }
        public List<string> ListStartDate { get; set; }
        public List<string> ListExpirationDate { get; set; }
        public List<bool> ListModified { get; set; }
        public List<bool> ListChecked { get; set; }
    }
    public class CreateOrder
    {
        public int StoreID { get; set; }
        //DeliverID 應該要傳(運送方式)，目前一種
        //從表單來
        public List<string> ListProductID { get; set; }
        public List<string> ListDailyRate { get; set; }
        public List<string> ListStartDate { get; set; }
        public List<string> ListExpirationDate { get; set; }
        public List<string> ListTotalAmount { get; set; }//小計

    }

    public class SalesAnalytic
    {
        //產品,類 , total , 店 ,會員年齡,會員
        public string PID { get; set; }
        public string ProductName { get; set; }
        public int Income { get; set; }
        public string StoreName { get; set; }
        public int MID { get; set; }
        public int? MemberAge { get; set; }
    }
}