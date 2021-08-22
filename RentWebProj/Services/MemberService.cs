using RentWebProj.Models;
using RentWebProj.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.ViewModels;


namespace RentWebProj.Services
{
    public class MemberService
    {
        private readonly CommonRepository _repository;
        public MemberService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public IEnumerable<MemberPersonDataViewModel> GetMemberData()
        {
            IEnumerable<MemberPersonDataViewModel> MemberCenterVM;
            IEnumerable<MemberOrderDetailViewModel> MemberOrderDetailVM;
            IEnumerable<MemberDiffDateViewModel> MemberDiffDateVM;
            var MemberDMList = _repository.GetAll<Member>();
            var BranchDMList = _repository.GetAll<BranchStore>();
            var OrderDMList = _repository.GetAll<Order>();
            var OrderDetailDMList = _repository.GetAll<OrderDetail>();
            var ProductDMList = _repository.GetAll<Product>();


            MemberDiffDateVM = from m in MemberDMList
                               join o in OrderDMList
                               on m.MemberID equals o.MemberID
                               join od in OrderDetailDMList
                               on o.OrderID equals od.OrderID
                               where m.MemberID == 38
                               select new MemberDiffDateViewModel
                               {
                                   StartDate = (DateTime)od.StartDate,
                                   ExpirationDate = (DateTime)od.ExpirationDate,
                                   DiffRentDate = (DateTime)od.ExpirationDate - (DateTime)od.StartDate
                                   //DiffDate = od.ExpirationDate.Subtract(od.StartDate),
                               };

            MemberOrderDetailVM = from od in OrderDetailDMList
                                  join o in OrderDMList
                                  on od.OrderID equals o.OrderID
                                  join b in BranchDMList
                                  on o.StoreID equals b.StoreID
                                  join p in ProductDMList
                                  on od.ProductID equals p.ProductID
                                  join m in MemberDMList
                                  on o.MemberID equals m.MemberID
                                  where m.MemberID == 38
                                  select new MemberOrderDetailViewModel
                                  {
                                      BranchName = b.StoreName,

                                      RentDate = (DateTime)od.ExpirationDate,
                                      //RentDate = DiffRentDate(od.ExpirationDate, od.StartDate),
                                      //RentDate = (DateTime)od.ExpirationDate - (DateTime)od.StartDate),
                                      //RentDate = (DateTime)od.ExpirationDate-(DateTime)od.StartDate,
                                      //RentDate = new DateTime(ExpirationDate.Ticks - StartDate.Ticks),
                                      //RentDate = od.ExpirationDate.ToString().Substring(0,11),

                                      TotalAmount = (int)od.TotalAmount,
                                      StartDate = (DateTime)od.StartDate,
                                      //StartDate = od.StartDate.ToString(),

                                      ProductName = p.ProductName,
                                      ExpirationDate = (DateTime)od.ExpirationDate,
                                      //ExpirationDate = od.ExpirationDate.ToString(),

                                      DailyRate = (int)od.DailyRate,
                                  };

            MemberCenterVM = from m in MemberDMList
                             join o in OrderDMList
                             on m.MemberID equals o.MemberID
                             join od in OrderDetailDMList
                             on o.OrderID equals od.OrderID
                             join b in BranchDMList
                             on o.StoreID equals b.StoreID
                             //join p in ProductDMList
                             //on od.ProductID equals p.ProductID
                             where m.MemberID == 38
                             select new MemberPersonDataViewModel
                             {
                                 MemberId = m.MemberID,
                                 MemberName = m.FullName,
                                 MemBerBirthday = (DateTime)m.Birthday,
                                 MemberPhone = m.Phone,
                                 MemberEmail = m.Email,
                                 MemberPasswordHash = m.PasswordHash,
                                 MemberBranchName = b.StoreName,
                                 MemberOrderDetail = MemberOrderDetailVM
                             };

            return MemberCenterVM;
        }


        public static int DiffRentDate(DateTime? expirationDate, DateTime? startDate)
        {
            DateTime date_1 = (DateTime)startDate;
            DateTime date_2 = (DateTime)expirationDate;

            TimeSpan Diff_dates = date_2.Subtract(date_1);
            return Diff_dates.Days;
        }

        //public int DiffRentDate(DateTime x, DateTime y)
        //{
        //    return 3;
        //}

        //差異天數Subtraction
        //private TimeSpan Subtraction(DateTime? expirationDate, DateTime? startDate)
        //{
        //    string dateDiff = null;
        //    TimeSpan ts1 = new TimeSpan(expirationDate.Ticks);
        //    TimeSpan ts2 = new TimeSpan(startDate.Ticks);
        //    TimeSpan ts = ts1.Subtract(ts2).Duration();
        //    dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小時" + ts.Minutes.ToString() + "分鐘" + ts.Seconds.ToString() + "秒";
        //    return dateDiff;
        //}



        ////單讀取一個表
        //public Member getMemberData()
        //{
        //    Member VMList;
        //    var DMList = _repository.GetAll<Member>();
        //    VMList = DMList.Where(x => x.MemberID == 38).FirstOrDefault();
        //    return VMList;
        //}

        //預計搭配Session使用
        //public Member getMemberData(int y)
        //{
        //    Member VMList;
        //    var DMList = _repository.GetAll<Member>();
        //    VMList = DMList.Where(x => x.MemberID == y).FirstOrDefault();
        //    return VMList;
        //}

    }
}