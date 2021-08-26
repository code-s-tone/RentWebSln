using RentWebProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.Services;
using RentWebProj.ViewModels;
using System.Data.Entity;
using System.Web.Security;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using System.Threading;

namespace RentWebProj.Controllers
{
    public class MemberController : Controller
    {

        private MemberService _service;
        public MemberController()
        {
            _service = new MemberService();
        }

        public ActionResult test()
        {

            return View();//可以強型別

        }

        // GET: Member
        public ActionResult MemberCenter()
        {

            return View(_service.GetMemberData());//可以強型別

        }
        public ActionResult Login()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Login(MemberLoginDetailViewModel s)
        {
                if (!ModelState.IsValid)
                {
                    return View(s);
                }
   
                if (_service.getMemberLogintData(s.Email, s.Password))
                {
                //把登入後的密碼，進行md5加密，然後去資料庫尋找
                //string name = HttpUtility.HtmlEncode(s.Email);
                //string password =HashService.MD5Hash( HttpUtility.HtmlEncode(s.Password));
                string name = HttpUtility.HtmlEncode(s.Email);
                string password = HttpUtility.HtmlEncode(s.Password);
                //1.建立FormsAuthenticationTicket
                var ticket = new FormsAuthenticationTicket(
                    version: 1,
                    name: s.Email.ToString(), //可以放使用者Id
                    issueDate: DateTime.UtcNow,//現在UTC時間
                    expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                    isPersistent: true,// 是否要記住我 true or false
                    userData: "", //可以放使用者角色名稱
                    cookiePath: FormsAuthentication.FormsCookiePath);

                    //2.加密Ticket
                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    //3.Create the cookie.
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(cookie);

                    //4.取得original URL.
                    var url = FormsAuthentication.GetRedirectUrl(name, true);

                    //5.導向original URL
                    return Redirect(url);
     
                }
                else
                {
                    ModelState.AddModelError("Password", "無效的帳號或密碼!");
                    return View(s);
                }

        }
        public ActionResult Register()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Register(MemberRegisterDetailViewModel s)
        {
            if (!ModelState.IsValid)
            {

                return View(s);
            }
            else
            {
                var result = _service.getMemberRegistertData(s);
                if (result)
                {
                    return Content("註冊成功");
                }
                else
                {
                    ModelState.AddModelError("Password", "帳號已存在，請重新輸入");
                    return View(s);
                }
            }
 
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Google(string id_token)
        {
            string msg = "ok";
            string user_id = "ok";//取得user_id
            string picture = "ok";//取得頭像
            string name = "ok";
            string email = "ok";
            GoogleJsonWebSignature.Payload payload = null;
            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(id_token, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { System.Web.Configuration.WebConfigurationManager.AppSettings["Google_clientId_forLogin"] }//要驗證的client id，把自己申請的Client ID填進去
                });
            }
            catch (Google.Apis.Auth.InvalidJwtException ex)
            {
                msg = ex.Message;
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                msg = ex.Message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            if (msg == "ok" && payload != null)
            {//都成功
                user_id = payload.Subject;//取得user_id
                picture = payload.Picture;//取得頭像
                email = payload.Email;
                name = payload.Name;
                 _service.getMemberGoogleData(user_id, picture, email,name);

                var ticket = new FormsAuthenticationTicket(
                  version: 1,
                  name: name.ToString(), //可以放使用者Id
                  issueDate: DateTime.UtcNow,//現在UTC時間
                  expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                  isPersistent: true,// 是否要記住我 true or false
                  userData: "", //可以放使用者角色名稱
                  cookiePath: FormsAuthentication.FormsCookiePath);

                //2.加密Ticket
                var encryptedTicket = FormsAuthentication.Encrypt(ticket);

                //3.Create the cookie.
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);

                //4.取得original URL.
                var url = FormsAuthentication.GetRedirectUrl(email.ToString(), true);

                //5.導向original URL

                //return View(url);
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index", "Home");

            }

            return View("test");
        }
    }
}