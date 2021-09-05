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
        public ActionResult MemberCenter()
        {
            //已將User.Identity.Name轉成MemberId
            return View(_service.GetMemberData(Int32.Parse(User.Identity.Name)).FirstOrDefault());
        }

        //Post: Member
       [HttpPost]
        public ActionResult MemberCenter(MemberPersonDataViewModel X)
        {
            //Step1. 先"取得"目前登入的"密碼"
            var MemberPasswordHash = _service.CheckPassword(Int32.Parse(User.Identity.Name));
            var MemberFullName = _service.CheckName(Int32.Parse(User.Identity.Name));
            var MemberPhone = _service.CheckPhone(User.Identity.Name);
            var MemberBirthDay = _service.CheckBirthDay(Int32.Parse(User.Identity.Name));
            //model是否合法驗證
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("ComfirMemberEmail", "無效的電子信箱");
                ModelState.AddModelError("ComfigMemberPasswordHash", "無效的密碼!");
                return View(_service.GetMemberData(Int32.Parse(User.Identity.Name)).FirstOrDefault());
            }
            //Step2. 把信箱跟密碼進行"比對並更新"
            //ViewBag.Change = _service.ChangeProfile(User.Identity.Name, X.ComfirMemberEmail, MemberPasswordHash, X.ComfigMemberPasswordHash, MemberFullName, X.MemberName, MemberPhone, X.MemberPhone);
            //ViewBag.Change = _service.ChangeProfile(User.Identity.Name, X.ComfirMemberEmail, MemberFullName, X.MemberName, MemberPhone, X.MemberPhone);
            ViewBag.Change = _service.ChangeProfile(Int32.Parse(User.Identity.Name), X.ComfirMemberEmail, MemberPasswordHash, X.MemberPasswordHash , MemberFullName, X.MemberName , MemberPhone, X.MemberPhone , MemberBirthDay , X.MemBerBirthday );

            return View(_service.GetMemberData(Int32.Parse(User.Identity.Name)).FirstOrDefault());//可以強型別
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

            string email = HttpUtility.HtmlEncode(s.Email);
            string password = Helper.SHA1Hash(HttpUtility.HtmlEncode(s.Password));
            if (_service.getMemberLogintData(email, password))
            {
                Helper.FormsAuthorization(s.Email);
                return RedirectToAction("Index", "Home");

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
            }
            return View();
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
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Thread.Sleep(5000);
            return RedirectToAction("Index", "Home");
        }

    }
}