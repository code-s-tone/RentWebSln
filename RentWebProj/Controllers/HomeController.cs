using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.Models;
using RentWebProj.Services;

namespace RentWebProj.Controllers
{
    public class HomeController : Controller
    {
        private IndexService _service;
        public HomeController(){
            _service = new IndexService();
        }

        public ActionResult Index()
        {
            ViewBag.Plist = _service.getCategoryData();
            return View(_service.getCategoryData() );
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult ContributionProgram()
        {
            return View();
        }
        public ActionResult HowToRent()
        {
            return View();
        }
        public ActionResult OurStory()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}