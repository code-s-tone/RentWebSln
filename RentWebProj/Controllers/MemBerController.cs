using RentWebProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.Services;
using RentWebProj.ViewModels;
using System.Data.Entity;

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

            else
            {   
                if(_service.getMemberLogintData(s.Email, s.Password))
                {
                    TempData["Message2"] = "註冊成功";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message2"] = "無帳號或密碼錯誤";
                    return View(s);
                }
            }

        }
    }
}