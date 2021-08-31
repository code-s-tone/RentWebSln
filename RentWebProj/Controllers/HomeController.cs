using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.ViewModels;
using RentWebProj.Services;


namespace RentWebProj.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProductService _service = new ProductService();

            IEnumerable<CardsViewModel> MostPopularPList = _service.GetMostPopularProductCardData();

            Dictionary<string, IEnumerable<CardsViewModel>> VMDictionary = new Dictionary<string, IEnumerable<CardsViewModel>>
            {
                //{ "最高評價", _service.GetMostPopularProductCardData() },
                //{ "最新上架", _service.GetMostPopularProductCardData() },
                { "最熱門", _service.GetMostPopularProductCardData() }

            };

            return View(VMDictionary);
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