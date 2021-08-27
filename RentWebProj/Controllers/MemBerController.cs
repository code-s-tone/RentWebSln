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

namespace RentWebProj.Controllers
{
    public class MemberController : Controller
    {

        private MemberService _service;
        public MemberController()
        {
            _service = new MemberService();
        }

        // GET: Member
        public ActionResult MemberCenter()
        {
            return View(_service.GetMemberData().FirstOrDefault());//可以強型別

            //單讀取一個表方法
            
            //預計搭配Session方法
            //return View(_service.getMemberData((int)Session["userId"]));//可以強型別
        }

        // Post: Member
        [HttpPost]
        public ActionResult MemberCenter( MemberPersonDataViewModel x)
        {

            //model是否合法驗證
            if (ModelState.IsValid) 
            {
                ModelState.AddModelError("ComfirMemberEmail", "無效的帳號或密碼!");
                //return View(x.MemberEmail);
                return View(_service.GetMemberData().FirstOrDefault());
            }
            return View();//可以強型別

            //單讀取一個表方法
            //ViewData.Model = _service.getMemberData();
            //預計搭配Session方法
            //return View(_service.getMemberData((int)Session["userId"]));//可以強型別
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
    }
}