using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.ViewModels;
using RentWebProj.Services;
using RentWebProj.Models;

namespace RentWebProj.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProductService _service = new ProductService();

            List<IndexProductView> VMList = new List<IndexProductView>
            {
                new IndexProductView
                {
                    Title = "便宜到老闆生無可戀(눈_눈)",
                    Url = "Product/Search?Keyword=&Category=0&SubCategory=0&RateBudget=0&OrderBy=Price",
                    Cards = _service.GetCheapestProductCardData().Take(6)
                },
                new IndexProductView
                {
                    Title = "30天內最熱門ლ(´ڡ`ლ)",
                    Url = "Product/Search?Keyword=&Category=0&SubCategory=0&RateBudget=0&OrderBy=Stars",
                    Cards = _service.ProductDataWithStars().Take(6)
                }
            };

            //判斷登入之後動態顯示大頭貼跟名子 by _家承
            if (User.Identity.IsAuthenticated)
            {
                var result = Helper.ConvertMemberIdToMemberProfile(Int32.Parse(User.Identity.Name));
                TempData["img"] = result.ProfilePhotoUrl;
                TempData["name"] = result.Fullname;
            }

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