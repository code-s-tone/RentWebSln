using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.Models;

namespace RentWebProj.Controllers
{
    public class HomeController : Controller
    {
        private RentContext db = new RentContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
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
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}