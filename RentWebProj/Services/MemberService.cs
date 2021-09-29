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
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNet.SignalR;
using System.Data.Entity;

namespace RentWebProj.Services
{
    public class MemberService
    {
        private readonly CommonRepository _repository;
        public MemberService()
        {
            _repository = new CommonRepository();
        }

        public MemberPersonDataViewModel GetMemberData(int LoginMemeberId)
        {
            var MemberDMList = _repository.GetAll<Member>();
            var OrderDMList = _repository.GetAll<Order>();
            var BranchDMList = _repository.GetAll<BranchStore>();

            var MemberOrderVM =   
                (from o in OrderDMList                                
                join b in BranchDMList
                on o.StoreID equals b.StoreID
                where o.MemberID == LoginMemeberId
                select new MemberOrderViewModel
                {
                    OrderID = o.OrderID,
                    OrderDate = o.OrderDate,
                    BranchName = b.StoreName,
                    OrderStatus = (OrderStatusName)o.OrderStatusID,
                }).ToList();

            MemberOrderVM.ForEach(order =>
            {

                order.OrderDetails = GetOrderDetails(order.OrderID);
            });

            MemberPersonDataViewModel MemberCenterVM = (from m in MemberDMList
                             where m.MemberID == LoginMemeberId
                             select new MemberPersonDataViewModel
                             {
                                 //系統自動產生
                                 MemberId = m.MemberID,
                                 MemberName = (String.IsNullOrEmpty(m.FullName)) ? null : m.FullName,
                                 //MemberName = m.FullName,
                                 //會員生日判斷如果為"null"則給預設值
                                 MemBerBirthday = (DateTime)(((DateTime)m.Birthday == null) ? DateTime.MinValue : m.Birthday),
                                 MemberYear = ((DateTime)m.Birthday).Year.ToString(),
                                 MemberMonth = ((DateTime)m.Birthday).Month.ToString(),
                                 MemberDay = ((DateTime)m.Birthday).Day.ToString(),
                                 MemberPhone = (String.IsNullOrEmpty(m.Phone)) ? null : m.Phone,
                                 //Email為必有欄位
                                 MemberEmail = m.Email,
                                 MemberPasswordHash = (String.IsNullOrEmpty(m.PasswordHash)) ? null : m.PasswordHash,
                                 //MemberBranchName = b.StoreName,
                                 //測試中訂單                                 
                                 //MemberOrderDetail = (MemberOrderDetailVM == null) ? null : MemberOrderDetailVM,
                             }).FirstOrDefault();

            MemberCenterVM.MemberOrders = MemberOrderVM;

            return MemberCenterVM;
        }

        private List<MemberOrderDetailViewModel> GetOrderDetails(int orderID)
        {
            var OrderDetailDMList = _repository.GetAll<OrderDetail>();
            var ProductDMList = _repository.GetAll<Product>();

            var result = 
                (from od in OrderDetailDMList
                 join p in ProductDMList
                 on od.ProductID equals p.ProductID
                 where od.OrderID ==  orderID 
                 select new MemberOrderDetailViewModel
                 {
                     ProductID = p.ProductID,
                     ProductName = p.ProductName,
                     //產品圖?
                     DailyRate = (int)od.DailyRate,
                     TotalAmount = (int)od.TotalAmount,
                     StartDate = od.StartDate,
                     ExpirationDate = od.ExpirationDate,
                     //RentDate = (int)DbFunctions.DiffDays(od.StartDate, od.ExpirationDate),
                     //RentDate = (int)((od.ExpirationDate - od.StartDate).Days),
                     RentDate = 1+((int)DbFunctions.DiffMinutes(od.StartDate, od.ExpirationDate)-1)/1440,
                     GoodsStatus = (GoodsStatusName)od.GoodsStatus
                 }).ToList();

            return result;
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
        
        //回傳個人資訊
        public string ChangeProfile(int UserMemberId , string ChangeUserName , string ChangeYear , string ChangeMonth , string ChangeDay, string ChangeUserPhone)
        {   
            //先找對應會員
            var result = _repository.GetAll<Member>().FirstOrDefault(x => x.MemberID == UserMemberId);

            result.FullName = ChangeUserName;
            result.Phone = ChangeUserPhone;
            result.Birthday = DateTime.Parse($"{ChangeYear}-{ChangeMonth}-{ChangeDay}");
            _repository.SaveChanges();
            return "";
        }

        //回傳信箱資訊
        public string ChangeEmail(int UserMemberId , string ChangeEmail)
        {
            var result = _repository.GetAll<Member>().FirstOrDefault(x => x.MemberID == UserMemberId);
            result.Email = ChangeEmail;
            _repository.SaveChanges();
            return result.Email;
        }

        //回傳密碼資訊
        public string ChangePassword(int UserMemberId , string ChangePassword)
        {
            var result = _repository.GetAll<Member>().FirstOrDefault(x => x.MemberID == UserMemberId);
            result.PasswordHash = Helper.SHA1Hash(ChangePassword);
            _repository.SaveChanges();
            return "";
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
                                     //Password = s.PasswordHash,
                                     Password = (String.IsNullOrEmpty(s.PasswordHash)) ? "Null" : s.PasswordHash,
                                 };
            string MemberPasswordString = "";
            //List<CheckInfo> MemberPasswordString = new List<CheckInfo>();
            foreach (var item in Memberpassword)
            {   //因為IQueryable故需要轉型為ToString
                if (item.Password == "Null")
                {
                    MemberPasswordString = item.Password.ToString();
                }
                else
                {
                    MemberPasswordString = item.Password.ToString();

                    //MemberPasswordString.Add;
                }

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
        public string FileUploadProfileImageData(string blobUrl)
        {
            var Sname = HttpContext.Current.User.Identity.Name;
            var Tname = Int32.Parse(Sname);
            Account account = new Account(
              "dgaodzamk",
              "192222538187587",
              "OG8h1MXpd4lG1N0blyuNA4lETsQ");

            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(blobUrl),//檔案來源
                PublicId = $"MemberProfilePhoto/{Sname}"//目標路徑?
            };

            var uploadResult = cloudinary.Upload(uploadParams);//上傳

            var getResultImgUrl = cloudinary.GetResource($"MemberProfilePhoto/{Sname}").SecureUrl;
            var result = _repository.GetAll<Member>();
            result.ToList().Find(x => x.MemberID == Tname).ProfilePhotoUrl = getResultImgUrl; ;
            _repository.SaveChanges();//這邊會異動資料庫??

            return getResultImgUrl;
        }

        //public void FileUploadProductImage(string PID, int index, string blobUrl)
        //{
        //    //初始設定
        //    Account account = new Account(//這些資料從哪來?
        //      "dgaodzamk",
        //      "192222538187587",
        //      "OG8h1MXpd4lG1N0blyuNA4lETsQ");

        //    Cloudinary cloudinary = new Cloudinary(account);//需要account物件


        //    string path = $"Product/Ppl/{PID}_{index}";
        //    var uploadParams = new ImageUploadParams()
        //    {
        //        File = new FileDescription(blobUrl),//檔案來源
        //        PublicId = path//目標路徑?
        //    };
        //    //上傳
        //    var uploadResult = cloudinary.Upload(uploadParams);

        //    //取得圖片網址?
        //    var getResultImgUrl = cloudinary.GetResource(path).SecureUrl;
        //    //寫入庫
        //    var entity = new ProductImage
        //    {
        //        ProductID = PID,
        //        ImageID = index,
        //        Source = getResultImgUrl
        //    };
        //    RentContext context = new RentContext();
        //    context.ProductImages.Add(entity);
        //    context.SaveChanges();
        //    //_repository.Create(entity);
        //    //_repository.SaveChanges();
        //}

        //抓取 要在首頁 顯示留言的資料<名駿>
        public IEnumerable<CommentViewModel> GetAllComment()
        {
            IEnumerable<CommentViewModel> AllCommentVMList;
            AllCommentVMList =
                from c in _repository.GetAll<Comment>()
                join m in _repository.GetAll<Member>()
                on c.MemberID equals m.MemberID
                orderby c.CommentID descending

                select new CommentViewModel
                {
                    MemberID = c.MemberID,
                    MemberName = m.FullName,
                    Score = c.Score,
                    Time = c.Time,
                    Message = c.Message,
                    PhotoUrl = m.ProfilePhotoUrl
                };

            return AllCommentVMList;
        }

        // 將顧客留言 Create一筆新的 之後存入資料庫
        public void Create(string comment, int star)
        {
            Comment entity = new Comment()
            {
                MemberID = (int)Helper.GetMemberId(),
                Score = star,
                Message = comment,
                Time = DateTime.Now
            };
            _repository.Create(entity);
            _repository.SaveChanges();
        }

        public void OrderNotify(int UserID)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<myHub>();



            var CheckGoodStatus = from od in _repository.GetAll<OrderDetail>()
                                  join o in _repository.GetAll<Order>()
                                  on od.OrderID equals o.OrderID
                                  join p in _repository.GetAll<Product>()
                                  on od.ProductID equals p.ProductID
                                  where o.MemberID == UserID & od.Notify == 0
                                  select new NOtify
                                  {
                                      ProductName = p.ProductName,
                                      MemberID = o.MemberID,
                                      ProductID = od.ProductID,
                                      OrderID = od.OrderID,
                                      GoodsStatus = od.GoodsStatus,
                                      Notify = od.Notify
                                  };

            var temp = CheckGoodStatus.ToList();
            temp.ForEach(c =>
            {
                if (c.GoodsStatus == 1) { context.Clients.All.broadcastMessage($"{c.ProductName}", "待出貨"); }
                if (c.GoodsStatus == 2) { context.Clients.All.broadcastMessage($"{c.ProductName}", "已出貨"); }
                if (c.GoodsStatus == 3) { context.Clients.All.broadcastMessage($"{c.ProductName}", "已到貨"); }
                if (c.GoodsStatus == 4) { context.Clients.All.broadcastMessage($"{c.ProductName}", "已取貨"); }
            });
        }

        // [取消訂單]按鈕 (待付款 => 已取消)
        public void Order_Cancel(int OrderID)
        {
            var order = _repository.GetAll<Order>().FirstOrDefault(x=>x.OrderID == OrderID);
            order.OrderStatusID = 0;
            _repository.Update<Order>(order);
            _repository.SaveChanges();
        }

        // [重新付款]按鈕 => 綠界支付訂單
        public int Get_TotalAmount(int OrderID)
        {
            decimal TotalAmount = 0;

            var Order_Details = from od in _repository.GetAll<OrderDetail>()
                                  join o in _repository.GetAll<Order>()
                                  on od.OrderID equals o.OrderID
                                  where od.OrderID == OrderID
                                  select new OrderDetail_TotalAmount
                                  {
                                      OrderID = od.OrderID,
                                      TotalAmount = od.TotalAmount
                                  };

            var temp = Order_Details.ToList();
            temp.ForEach(x =>
            {
                TotalAmount += x.TotalAmount;
            });
            var total_money = (int)TotalAmount;

            return total_money;
        }

    }
}