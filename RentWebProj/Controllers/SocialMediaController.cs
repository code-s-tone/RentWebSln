using RentWebProj.Services;
using RentWebProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentWebProj.Controllers
{
    public class SocialMediaController : Controller
    {
        private BlogService _service;
        public SocialMediaController()
        {
            _service = new BlogService();
        }
        
        public ActionResult BlogList()
        {
            var blogList = _service.GetAllBlogs();
            return View(blogList);
        }
        public ActionResult BlogPage(int Id)
        {
             _service.FindBlogById(Id);
            return View();
        }
        public ActionResult News()
        {
            return View();
        }
        public ActionResult SharingEconomy()
        {
            return View();
        }
    }
}