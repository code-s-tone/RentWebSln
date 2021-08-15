using RentWebProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentWebProj.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult MemberCenter()
        {
            //初始化資料庫
            RentContext db = new RentContext();
            var item = db.Members.ToList();
            ViewData["email"] = item.Find(x => x.Account == "Code123").Email;
            ViewData["password"] = item.Find(x => x.Account == "Code123").PasswordHash;
            ViewData["ipone"] = item.Find(x => x.Account == "Code123").Phone;
            ViewData["fullName"] = item.Find(x => x.Account == "Code123").FullName;
            return View();
        }
    }
}