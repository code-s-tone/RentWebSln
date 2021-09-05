using RentWebProj.Models;
using RentWebProj.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentWebProj.ViewModels;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Windows;

namespace RentWebProj.Services
{
    public class MemberService
    {
        private readonly CommonRepository _repository;
        public MemberService()
        {
            _repository = new CommonRepository(new RentContext());
        }

        public IEnumerable<MemberPersonDataViewModel> GetMemberData(int LoginMemeberId)
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
                                  where m.MemberID == LoginMemeberId
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
                             where m.MemberID == LoginMemeberId
                             select new MemberPersonDataViewModel
                             {
                                 //系統自動產生
                                 MemberId = m.MemberID,
                                 MemberName = (String.IsNullOrEmpty(m.FullName)) ? null : m.FullName,
                                 //MemberName = m.FullName,
                                 //會員生日判斷如果為"null"則給預設值
                                 MemBerBirthday = (DateTime)(((DateTime)m.Birthday == null) ? DateTime.MinValue : m.Birthday),
                                 MemberPhone = (String.IsNullOrEmpty(m.Phone)) ? null : m.Phone,
                                 //Email為必有欄位
                                 MemberEmail = m.Email,
                                 MemberPasswordHash = (String.IsNullOrEmpty(m.PasswordHash)) ? null : m.PasswordHash,
                                 //MemberBranchName = b.StoreName,
                                 //測試中訂單
                                 MemberOrderDetail = MemberOrderDetailVM,
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
        public bool getMemberRegistertData(string Email,string Password)
        {
            var pDMList = _repository.GetAll<Member>();
            string email = HttpUtility.HtmlEncode(Email);
            string password = HttpUtility.HtmlEncode(Password);
            var result = pDMList.Where(x => x.Email == email && x.PasswordHash == password).FirstOrDefault();
            if (result == null)
            {
                var entity = new Member { Email = email, PasswordHash = password, SignWayID = 1 ,FullName="未設定",ProfilePhotoUrl= "https://res.cloudinary.com/dgaodzamk/image/upload/v1629979251/%E9%BC%BB%E6%B6%95%E8%B2%93.png" };
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

        public void getMemberFbData(string name, string email)
        {
            var pDMList = _repository.GetAll<Member>();
            var result = pDMList.Where(x => x.Email == email && x.FullName == name || x.Email == email).FirstOrDefault();
            if (result == null)
            {
                var entity = new Member { Email = email, SignWayID = 1, FullName = name, ProfilePhotoUrl = "https://res.cloudinary.com/dgaodzamk/image/upload/v1629979251/%E9%BC%BB%E6%B6%95%E8%B2%93.png" };
                _repository.Create(entity);
                _repository.SaveChanges();
            }

        }
        //public MessageBoxResult ChangeProfile(string UserEmail, string ChangeEmail, string UserPassword, string ChangePassword , string UserFullName , string ChangeFullName , string UserPhone ,string ChangePhone)
        //public MessageBoxResult ChangeProfile(string UserEmail, string ChangeEmail,string UserFullName , string ChangeFullName , string UserPhone ,string ChangePhone)
        public MessageBoxResult ChangeProfile(int UserMemberId, string ChangeEmail , string UserPassword, string ChangePassword, string UserFullName , string ChangeFullName , string UserPhone , string ChangePhone)
        {
            var result = _repository.GetAll<Member>().FirstOrDefault(x=>x.MemberID==UserMemberId);

            result.Email = ChangeEmail;

            result.PasswordHash = Helper.SHA1Hash(ChangePassword);

            if (UserFullName == null || UserFullName == "")
            {
                result.FullName = ChangeFullName;
            }
            else
            {
                result.FullName = ChangeFullName;
            }
            //result.Find(x => x.MemberID == UserMemberId).Email = ChangeEmail;
            //result.Find(x => x.PasswordHash == UserPasswordHash).PasswordHash = ChangePasswordHash;
            //result.Find(x => x.FullName == UserFullName).FullName = ChangeFullName;
            //會員電話判斷
            if (UserPhone == null)
            {
                result.Phone = ChangePhone;
            }
            else
            {
                result.Phone = ChangePhone;
            }
            
            _repository.SaveChanges();

            //return "修改成功";
            return MessageBox.Show("修改成功");
        }

        //取得與目前登入User對應的"密碼"
        //public List<CheckInfo> CheckInfo(string UserEmail)
        public string CheckPassword(int MemberId)
        {
            var result = _repository.GetAll<Member>();
            var Memberpassword = from s in result
                                 where s.MemberID == MemberId
                                 select new CheckPassword
                                 {
                                     Password = s.PasswordHash
                                 };
            string MemberPasswordString = "";
            //List<CheckInfo> MemberPasswordString = new List<CheckInfo>();
            foreach (var item in Memberpassword)
            {   //因為IQueryable故需要轉型為ToString
                MemberPasswordString = item.Password.ToString();
                //MemberPasswordString.Add;
            }
            return MemberPasswordString;
        }

        //取得與目前登入User對應的"姓名"
        public string CheckName(int MemberId)
        {
            var result = _repository.GetAll<Member>();
            var MemberFullName = from s in result
                                 where s.MemberID == MemberId
                                 select new CheckFullName
                                 {
                                     Name = s.FullName
                                 };
            string MemberNameString = "";
            foreach (var item in MemberFullName)
            {   //因為IQueryable故需要轉型為ToString
                MemberNameString = item.Name.ToString();
            }
            return MemberNameString;
        }

        //取得與目前登入User對應的"電話"
        public string CheckPhone(string UserEmail)
        {
            var result = _repository.GetAll<Member>();
            var MemberPhone = from s in result
                                 where s.Email == UserEmail
                                 select new CheckPhone
                                 {
                                    Phone  = s.Phone
                                 };
            string MemberPhoneString = "";
            foreach (var item in MemberPhone)
            {   //因為IQueryable故需要轉型為ToString
                MemberPhoneString = item.Phone.ToString();
            }
            return MemberPhoneString;
        }
    }
}