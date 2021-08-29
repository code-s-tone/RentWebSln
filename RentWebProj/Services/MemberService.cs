using RentWebProj.Models;
using RentWebProj.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.ViewModels;
using System.Data.Entity.Core.Objects;
using System.Globalization;

namespace RentWebProj.Services
{
    public class MemberService
    {
        private readonly CommonRepository _repository;
        public MemberService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public IEnumerable<MemberPersonDataViewModel> GetMemberData(string LoginEmail)
        {
            IEnumerable<MemberPersonDataViewModel> MemberCenterVM;
            IEnumerable<MemberOrderDetailViewModel> MemberOrderDetailVM;
            var MemberDMList = _repository.GetAll<Member>();
            var BranchDMList = _repository.GetAll<BranchStore>();
            var OrderDMList = _repository.GetAll<Order>();
            var OrderDetailDMList = _repository.GetAll<OrderDetail>();
            var ProductDMList = _repository.GetAll<Product>();


            MemberOrderDetailVM = from od in OrderDetailDMList
                                  join o in OrderDMList
                                  on od.OrderID equals o.OrderID
                                  join b in BranchDMList
                                  on o.StoreID equals b.StoreID
                                  join p in ProductDMList
                                  on od.ProductID equals p.ProductID
                                  join m in MemberDMList
                                  on o.MemberID equals m.MemberID
                                  //where m.MemberID == 38
                                  where m.Email == LoginEmail
                                  select new MemberOrderDetailViewModel
                                  {
                                      BranchName = b.StoreName,
                                      RentDate = (int)EntityFunctions.DiffDays((DateTime)od.StartDate, (DateTime)od.ExpirationDate),
                                      TotalAmount = (int)od.TotalAmount,
                                      StartDate = (DateTime)od.StartDate,
                                      ProductName = p.ProductName,
                                      ExpirationDate = (DateTime)od.ExpirationDate,
                                      DailyRate = (int)od.DailyRate,
                                  };


            MemberCenterVM = from m in MemberDMList
                                 //join o in OrderDMList
                                 //on m.MemberID equals o.MemberID
                                 //join od in OrderDetailDMList
                                 //on o.OrderID equals od.OrderID
                                 //join b in BranchDMList
                                 //on o.StoreID equals b.StoreID
                                 //where m.MemberID == 38
                             where m.Email == LoginEmail
                             select new MemberPersonDataViewModel
                             {
                                 //系統自動產生
                                 MemberId = m.MemberID,
                                 MemberName = (String.IsNullOrEmpty(m.FullName)) ? null : m.FullName,
                                 //會員生日判斷如果為"null"則給預設值
                                 MemBerBirthday = (DateTime)(((DateTime)m.Birthday == null) ? DateTime.MinValue : m.Birthday),
                                 MemberPhone = (String.IsNullOrEmpty(m.Phone)) ? null : m.Phone,
                                 //Email回必填欄位
                                 MemberEmail = m.Email,
                                 MemberPasswordHash = (String.IsNullOrEmpty(m.PasswordHash)) ? null : m.PasswordHash,
                                 //MemberBranchName = b.StoreName,
                                 //測試訂單中
                                 //MemberOrderDetail = MemberOrderDetailVM,
                                 //MemberOrderDetail = (MemberOrderDetailVM == null) ? null : MemberOrderDetailVM,
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
            var result = pDMList.Where(x => x.Email == email && x.PasswordHash == password).FirstOrDefault();
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

        public string ChangeProfile(string UserEmail, string ChangeEmail, string UserPassword, string ChangePassword)
        {
            var result = _repository.GetAll<Member>().ToList();
            result.Find(x => x.Email == UserEmail).Email = ChangeEmail;
            result.Find(x => x.PasswordHash == UserPassword).PasswordHash = ChangePassword;
            _repository.SaveChanges();

            return "修改成功";
        }

        //取得與目前登入User對應的"密碼"
        public string CheckName(string UserEmail)
        {
            var result = _repository.GetAll<Member>();
            var Memberpassword = from s in result
                                 where s.Email == UserEmail
                                 select new CheckPassword
                                 {
                                     Password = s.PasswordHash
                                 };
            string MemberPasswordString = "";
            foreach (var item in Memberpassword)
            {   //因為IQueryable故需要轉型為ToString
                MemberPasswordString = item.Password.ToString();
            }
            return MemberPasswordString;
        }
    }
}