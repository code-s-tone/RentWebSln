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
        public IEnumerable<MemberOrderDetailViewModel> MemberOrderDetail { get; set; }
    }


    public class MemberOrderDetailViewModel
    {
        public string BranchName { get; set; }

        //ExpirationDate - RentDate = RentDate
        public DateTime RentDate { get; set; }

        //測試區
        //public TimeSpan RentDate { get; set; }
        //public int RentDate { get; set; }
        //public IEnumerable<MemberDiffDateViewModel> RentDate { get; set; }

        public int TotalAmount { get; set; }

        public DateTime StartDate { get; set; }
        //public string StartDate { get; set; }

        public string ProductName { get; set; }

        public DateTime ExpirationDate { get; set; }
        //public string ExpirationDate { get; set; }
        public int DailyRate { get; set; }

        //public int Available { get; set; }
        //public int TotalAmount { get; set; }
    }

    public class MemberDiffDateViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public TimeSpan DiffRentDate { get; set; }
    }
}