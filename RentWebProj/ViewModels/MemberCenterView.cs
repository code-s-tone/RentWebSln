using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentWebProj.ViewModels
{
    public class MemberPersonDataViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public DateTime MemBerBirthday { get; set; }
        public string MemberPhone { get; set; }
        public string MemberBranchName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPasswordHash { get; set; }
        //public IEnumerable<MemberOrderDetailViewModel> OrderDetail {get;set;}
    }


    public class MemberOrderDetailViewModel
    {
        public string BranchName { get; set; }

        //ExpirationDate - RentDate = RentDate
        public DateTime RentDate { get; set; }

        public int TotalAmount { get; set; }
        public DateTime StartDate { get; set; }
        public string ProductName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int DailyRate { get; set; }

        //public int Available { get; set; }
        //public int TotalAmount { get; set; }
    }
}