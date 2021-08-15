using RentWebProj.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email,string Password)
        {
            if (_service.getMemberLogintData(Email, Password))
            {
                Session["UserID"] = Email.ToString();
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Register(string Email, string Password)
        {
            _service.getMemberRegistertData(Email, Password);
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}