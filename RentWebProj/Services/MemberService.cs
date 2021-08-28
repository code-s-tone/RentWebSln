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
                                 //join o in OrderDMList
                                 //on m.MemberID equals o.MemberID
                                 //join od in OrderDetailDMList
                                 //on o.OrderID equals od.OrderID
                                 //join b in BranchDMList
                                 //on o.StoreID equals b.StoreID
                             where m.MemberID == 38
                             select new MemberPersonDataViewModel
                             {
                                 MemberId = m.MemberID,
                                 MemberName = m.FullName,
                                 MemBerBirthday = (DateTime)m.Birthday,
                                 MemberPhone = m.Phone,
                                 MemberEmail = m.Email,
                                 MemberPasswordHash = m.PasswordHash,
                                 //MemberBranchName = b.StoreName
                             };

            return MemberCenterVM;
        }

        public bool getMemberLogintData(string Email, string Password)
        {
            var pDMList = _repository.GetAll<Member>();
            string email = HttpUtility.HtmlEncode(Email);
            string password = HttpUtility.HtmlEncode(Password);

            var result = pDMList.Where(x => x.Email == Email && x.PasswordHash == Password).FirstOrDefault() == null ? false : true;
            return result;

        }
        public bool getMemberRegistertData(MemberRegisterDetailViewModel s)
        {
            var pDMList = _repository.GetAll<Member>();
            string email = HttpUtility.HtmlEncode(s.Email);
            string password = HttpUtility.HtmlEncode(s.Password);
            var result = pDMList.Where(x => x.Email == email && x.PasswordHash == password || x.Email == email).FirstOrDefault();
            if (result == null)
            {
                var entity = new Member { Email = s.Email, PasswordHash = s.Password, SignWayID = 1 };
                _repository.Create(entity);
                _repository.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public void getMemberGoogleData(string id, string picture, string email, string name)
        {
            var pDMList = _repository.GetAll<Member>();
            var result = pDMList.Where(x => x.Email == email && x.FullName == name && x.Account == id).FirstOrDefault();
            if (result == null)
            {
                var entity = new Member { Email = email, SignWayID = 1, FullName = name, Account = id, ProfilePhotoUrl = picture };
                _repository.Create(entity);
                _repository.SaveChanges();
            }

        }

        public void getMemberLineData(string name, string picture, string email)
        {
            var pDMList = _repository.GetAll<Member>();
            var result = pDMList.Where(x => x.Email == email && x.FullName == name || x.Email == email).FirstOrDefault();
            if (result == null)
            {
                var entity = new Member { Email = email, SignWayID = 1, FullName = name, ProfilePhotoUrl = picture };
                _repository.Create(entity);
                _repository.SaveChanges();
            }

        }

    }
}