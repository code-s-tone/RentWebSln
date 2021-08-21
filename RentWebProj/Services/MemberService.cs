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
            var MemberDMList = _repository.GetAll<Member>();
            var BranchDMList = _repository.GetAll<BranchStore>();
            var OrderDMList = _repository.GetAll<Order>();
            var OrderDetailDMList = _repository.GetAll<OrderDetail>();
            var ProductDMList = _repository.GetAll<Product>();

            MemberCenterVM = from m in MemberDMList
                             join o in OrderDMList
                             on m.MemberID equals o.MemberID
                             join od in OrderDetailDMList
                             on o.OrderID equals od.OrderID
                             join b in BranchDMList
                             on o.StoreID equals b.StoreID
                             where m.MemberID == 38
                             select new MemberPersonDataViewModel
                             {   
                                 MemberId = m.MemberID,
                                 MemberName = m.FullName,
                                 MemBerBirthday = (DateTime)m.Birthday,
                                 MemberPhone = m.Phone,
                                 MemberEmail = m.Email,
                                 MemberPasswordHash = m.PasswordHash,
                                 MemberBranchName = b.StoreName
                             };

            return MemberCenterVM;
        }



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