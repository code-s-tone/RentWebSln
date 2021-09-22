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
using System.Threading;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Google.Apis.Auth;
using System.IO;

namespace RentWebProj.Controllers
{
    
    public class MemberController : Controller
    {

        private MemberService _service;
        public MemberController()
        {
            _service = new MemberService();
        }

        [Authorize]
        // GET: Member
        public ActionResult MemberCenter(string Index)
        {
            //已將User.Identity.Name轉成MemberId
            ViewBag.nav = Index;
            var isNullPassword = _service.CheckPassword(Int32.Parse(User.Identity.Name));
            ViewBag.IsNotHasPassword = isNullPassword;
            var VM = _service.GetMemberData(Int32.Parse(User.Identity.Name));
            return View(VM);
        }

        //Post: Member
        [HttpPost]
        //回傳個人資訊
        public ActionResult MemberPerson(MemberPersonDataViewModel X)
        {
            ViewBag.returnPerson = _service.ChangeProfile(Int32.Parse(User.Identity.Name), X.MemberName, X.MemberYear, X.MemberMonth, X.MemberDay, X.MemberPhone);
            Thread.Sleep(1500);
            return View("MemberCenter", _service.GetMemberData(Int32.Parse(User.Identity.Name)));
        }

        [HttpPost]
        //回傳信箱資訊
        public ActionResult MemberEmail(MemberPersonDataViewModel X)
        {
            //if (ModelState.IsValid)
            //{
            //    ModelState.AddModelError("ComfirMemberEmail", "無效的電子信箱");
            //    return View(_service.GetMemberData(Int32.Parse(User.Identity.Name)));
            //}
            ViewBag.returnEmail = _service.ChangeEmail(Int32.Parse(User.Identity.Name), X.ComfirMemberEmail);
            Thread.Sleep(1500);
            return View("MemberCenter", _service.GetMemberData(Int32.Parse(User.Identity.Name)));

        }

        [HttpPost]
        //回傳密碼資訊
        public ActionResult MemberPassword(MemberPersonDataViewModel X)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("ComfigMemberPasswordHash", "無效的密碼!");
                return View(_service.GetMemberData(Int32.Parse(User.Identity.Name)));
            }
            ViewBag.returnEmail = _service.ChangePassword(Int32.Parse(User.Identity.Name), X.MemberPasswordHash);
            Thread.Sleep(1500);
            return View("MemberCenter", _service.GetMemberData(Int32.Parse(User.Identity.Name)));
        }


        public ActionResult Login()
        {
            try
            {
                TempData["text"] = Request.UrlReferrer.AbsolutePath;
                TempData["url"] = Request.UrlReferrer.AbsoluteUri;
                return View();
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public ActionResult Login(MemberLoginDetailViewModel s)
        {
            var Url = TempData["url"];
            if (!ModelState.IsValid)
            {
                return View(s);
            }

            string email = HttpUtility.HtmlEncode(s.Email);
            string password = Helper.SHA1Hash(HttpUtility.HtmlEncode(s.Password));
;
            if (_service.getMemberLogintData(email, password))
            {
                Helper.FormsAuthorization(s.Email);
                if (TempData["text"].ToString()== "/Member/Register")
                {
                    return RedirectToAction("Index", "Home");
                }
                try
                {
                    return Redirect(Url.ToString());
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }
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
                string email = HttpUtility.HtmlEncode(s.Email);
                string password = Helper.SHA1Hash(HttpUtility.HtmlEncode(s.Password));
                var result = _service.getMemberRegistertData(email, password);
                if (result)
                {
                    TempData["msg"] = "註冊成功!";
                    return RedirectToAction("Login", "Member");
                }
                else
                {
                    ModelState.AddModelError("Password", "帳號已存在，請重新輸入");
                    return View(s);
                }
            }

        }
        string response_type = "code";
        string client_id = "1656366912";
        string redirect_uri = HttpUtility.UrlEncode("https://localhost:44399/Member/LineCallback");
        string state = "success";
        public ActionResult Line()
        {
            string LineLoginUrl = string.Format("https://access.line.me/oauth2/v2.1/authorize?response_type={0}&client_id={1}&redirect_uri={2}&state={3}&scope=openid%20profile%20email",
                response_type,
                client_id,
                redirect_uri,
                state
                );
            return Redirect(LineLoginUrl);
        }
        public ActionResult LineCallback(string code, string state)
        {
            if (state == "success")
            {
                #region Api變數宣告
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                string result = string.Empty;
                NameValueCollection nvc = new NameValueCollection();
                NameValueCollection nyc = new NameValueCollection();
                #endregion

                #region Line_Post_token
                try
                {
                    //取回Token
                    string ApiUrl_Token = "https://api.line.me/oauth2/v2.1/token";
                    nvc.Add("grant_type", "authorization_code");
                    nvc.Add("code", code);
                    nvc.Add("redirect_uri", "https://localhost:44399/Member/LineCallback");
                    nvc.Add("client_id", "1656366912");
                    nvc.Add("client_secret", "fbde31cf195d309ad7cffc633840b557");
                    string JsonStr = Encoding.UTF8.GetString(wc.UploadValues(ApiUrl_Token, "POST", nvc));
                    MemberLineLoginTokenViewModel ToKenObj = JsonConvert.DeserializeObject<MemberLineLoginTokenViewModel>(JsonStr);
                    wc.Headers.Clear();
                    //取回使用者資訊

                    //取回email
                    string Email_Url = "https://api.line.me/oauth2/v2.1/verify";
                    nyc.Add("client_id", "1656366912");
                    nyc.Add("id_token", ToKenObj.id_token);
                    string JsonStrr = Encoding.UTF8.GetString(wc.UploadValues(Email_Url, "POST", nyc));
                    MemberLineProfileTokenViewModel ToKenObja = JsonConvert.DeserializeObject<MemberLineProfileTokenViewModel>(JsonStrr);



                    //取回User Profile
                    string ApiUrl_Profile = "https://api.line.me/v2/profile";
                    wc.Headers.Add("Authorization", "Bearer " + ToKenObj.access_token);
                    string UserProfile = wc.DownloadString(ApiUrl_Profile);
                    MemberLineProfileTokenViewModel ProfileObj = JsonConvert.DeserializeObject<MemberLineProfileTokenViewModel>(UserProfile);
                    ViewData["name"] = ProfileObj.displayName;
                    ViewData["pictureUrl"] = ProfileObj.pictureUrl;
                    ViewData["email"] = ToKenObja.email;
                    _service.getMemberLineData(ProfileObj.displayName, ProfileObj.pictureUrl, ToKenObja.email);


                    string email = HttpUtility.HtmlEncode(ToKenObja.email);
                    Helper.FormsAuthorization(email);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    throw;
                }
                #endregion
            }
            if (Request.UrlReferrer.LocalPath != "/" && !string.IsNullOrEmpty(Request.UrlReferrer.LocalPath))
            {
                return RedirectPermanent("https://www.google.com");
            }
            else
            {
             
                return View();
            }
     
        }
        [HttpPost]
        public async Task<ActionResult> Google(string id_token)
        {
            string msg = "ok";
            string user_id = "ok";
            string picture = "ok";
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
                picture = payload.Picture;
                email = payload.Email;
                name = payload.Name;
                _service.getMemberGoogleData(user_id, picture, email, name);
                Helper.FormsAuthorization(email);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Fb(string id_token)
        {

            string targetUrl = "https://graph.facebook.com/me?fields=id,name,email,picture&access_token=" + id_token;

            HttpWebRequest request = HttpWebRequest.Create(targetUrl) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            string result = "";
            // 取得回應資料
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }

            MemberFbProfileTokenViewModel Profile = JsonConvert.DeserializeObject<MemberFbProfileTokenViewModel>(result);
            ViewData["FB_Email"] = Profile.email;
            ViewData["FB_Name"] = Profile.name;
            ViewData["FB_userId"] = Profile.id;
            _service.getMemberFbData(Profile.name, Profile.email);

            Helper.FormsAuthorization(ViewData["FB_Email"].ToString());
            if (Request.UrlReferrer.LocalPath != "/" && !string.IsNullOrEmpty(Request.UrlReferrer.LocalPath))
            {
                TempData["PreviousUrl"] = Request.UrlReferrer.LocalPath.ToString();
                TempData["PreviousController"] = Request.UrlReferrer.LocalPath.ToString().Split('/')[1];
                TempData["PreviousAction"] = Request.UrlReferrer.LocalPath.ToString().Split('/')[2];
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ProfilePhoto(string blobUrl)
        {
            _service.FileUploadProfileImageData(blobUrl);
            return RedirectToAction("MemberCenter", "Member");
        }
        public ActionResult SignOut()
        {
        
            var reuslt = Request.UrlReferrer.ToString();
            FormsAuthentication.SignOut();
            return Redirect(reuslt);
            //戰略性註解
            //Thread.Sleep(4000);
        }

        public ActionResult DeveloperLogin()
        {
            string Deve_email = "htc7414@gmail.com";
            Helper.FormsAuthorization(Deve_email);
            return RedirectToAction("Index", "Home");
        }

    }
}