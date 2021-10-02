﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.ViewModels;
using RentWebProj.Services;
using RentWebProj.Models;
using RentWebProj.Interfaces;

namespace RentWebProj.Controllers
{
    
    public class HomeController : Controller
    {
        readonly IProductService _service;
        //ProductService _service = new ProductService();
        public HomeController(IProductService iProductService)
        {
            this._service = iProductService;
        }

        public ActionResult Index()
        {
            //判斷登入之後動態顯示大頭貼跟名子 by _家承
            if (User.Identity.IsAuthenticated)
            {
                var result = Helper.ConvertMemberIdToMemberProfile(Int32.Parse(User.Identity.Name));
                TempData["img"] = result.ProfilePhotoUrl;
                TempData["name"] = result.Fullname;
            }

            List<IndexProductView> VMList = new List<IndexProductView>
            {
                new IndexProductView
                {
                    Title = "便宜到老闆生無可戀(눈_눈)",
                    Url = "Product/Search?Keyword=&Category=0&SubCategory=0&RateBudget=0&OrderBy=Price",
                    Cards = _service.GetCheapestProductCardData()
                },
                new IndexProductView
                {
                    Title = "30天內最熱門ლ(´ڡ`ლ)",
                    Url = "Product/Search?Keyword=&Category=0&SubCategory=0&RateBudget=0&OrderBy=Stars",
                    Cards = _service.ProductDataWithStars()
                }
            };
            ViewBag.Categories = _service.GetCategoryData();
            ViewBag.NewComments = new MemberService().GetAllComment().Take(10);
            ViewBag.Blogs = new BlogService().GetAllBlogs().OrderByDescending(x => x.PostDate).Take(5);

            return View(VMList);
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(string comment,int star)
        {
                MemberService memberService = new MemberService();
                memberService.Create(comment, star);

            return RedirectToAction("Index", "Home");
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