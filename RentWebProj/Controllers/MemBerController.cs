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
    }
}