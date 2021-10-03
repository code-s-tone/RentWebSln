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
        readonly IBlogService _iBlogService;

        public SocialMediaController(IBlogService iBlogService)
        {
            this._iBlogService = iBlogService;
        }

        public ActionResult BlogList()
        {
            var blogList = _iBlogService.GetAllBlogs();
            return View(blogList);
        }
        public ActionResult BlogPage(int id)
        {
             var blog = _iBlogService.FindBlogById(id);
            return View(blog);
        }

        public ActionResult SharingEconomy()
        {
            return View();
        }
    }
}