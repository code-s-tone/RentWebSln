using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RentWebProj.Repositories;
using RentWebProj.Models;
using System.Security.Cryptography;
using System.Web.Security;
using System.Web;
using RentWebProj.ViewModels;

namespace RentWebProj.Services
{
    public static class Helper
    {

        private static CommonRepository _repository = new CommonRepository();
        public static string SHA1Hash(string rawString)
        {
            if (string.IsNullOrEmpty(rawString))
            {
                return "";
            }

            StringBuilder sb;

            using (SHA1 sha1 = SHA1.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(rawString);

                byte[] encryption = sha1.ComputeHash(byteArray);


                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    sb.Append(encryption[i].ToString("x2"));
                }
            }

            return sb.ToString(); ;
        }
        public static void FormsAuthorization(string Email) //擴充方法
        {

            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: ConvertEmailToMemberId(Email).ToString(), //可以放使用者Id
                issueDate: DateTime.UtcNow,//現在UTC時間
                expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                isPersistent: true,// 是否要記住我 true or false
                userData: "", //可以放使用者角色名稱
                cookiePath: FormsAuthentication.FormsCookiePath);

            //2.加密Ticket
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            //3.Create the cookie.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            //4.網頁添加cookie
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));

        }
        public static MemberProfileViewModel ConvertMemberIdToMemberProfile(int MemberID) //擴充方法
        {
            IEnumerable<MemberProfileViewModel> vmModel;
            var MemberDMList = _repository.GetAll<Member>();
            vmModel= from m in MemberDMList
                     where m.MemberID == MemberID
                     select new MemberProfileViewModel
                     {
                       Fullname=m.FullName,
                       ProfilePhotoUrl=m.ProfilePhotoUrl,
                       email=m.Email,
                     };
            var result = vmModel.FirstOrDefault();
            return result;
        }
        public static string ConvertMemberIdToFullName(int MemberID) //擴充方法
        {

            var MemberDMList = _repository.GetAll<Member>();
            var result = MemberDMList.Where(x => x.MemberID == MemberID).Select(x => x.FullName).FirstOrDefault();
            return result;
        }
        public static string ConvertMemberIdToProgilePhotoUrl(int MemberID) //擴充方法
        {

            var MemberDMList = _repository.GetAll<Member>();
            var result = MemberDMList.Where(x => x.MemberID == MemberID).Select(x => x.ProfilePhotoUrl).FirstOrDefault();
            return result;
        }
        public static int ConvertEmailToMemberId(string Email) //擴充方法
        {
            var MemberDMList = _repository.GetAll<Member>();
            var result = MemberDMList.Where(x => x.Email == Email).Select(x => x.MemberID).FirstOrDefault();
            return result;
        }

        public static int? GetMemberId() //擴充方法
        {
            int? currentMemberID = null;
            var identity = HttpContext.Current.User.Identity;

            if (identity.IsAuthenticated)
            {
                currentMemberID = ConvertAuthNameToMemberId(identity.Name);
            }

            return currentMemberID;
        }

        public static int ConvertAuthNameToMemberId(string AuthName) //擴充方法
        {
            int MID = Int32.Parse(AuthName);
            return MID;
        }

        // 查詢:購物車內 物品總數量 
        public static int? GetCartCount()
        {
            int? currentMemberID = null;
            int cartCount= 0;
            var identity = HttpContext.Current.User.Identity;

            if (identity.IsAuthenticated)
            {
                currentMemberID = ConvertAuthNameToMemberId(identity.Name);
                cartCount = _repository.GetAll<Cart>().Where(x=>x.MemberID == (int)(currentMemberID)).Count();
            }
            else cartCount = 0;

            return cartCount;
        }


        //public static string WriteLog(this OperationResult value) //擴充方法
        //{
        //    if ( value.Exception != null)
        //    {
        //        string path = $"{DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss")}.txt";
        //        File.WriteAllText(path, value.Exception.ToString());
        //        return path;
        //    }
        //    else
        //    {
        //        return "沒有存檔";
        //    }
        //}
    }

    public class OperationResult
    {
        public bool IsSuccessful { get; set; }
        public Exception Exception { get; set; }
    }

}
