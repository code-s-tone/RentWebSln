using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentWebProj.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        //實際頁面
        public ActionResult GeneralCategories()
        {          
            return View();
        }
        public ActionResult ProductCards()
        {
            return View();
        }

        public ActionResult ProductCategory()
        {
            //先不刪 名駿說要留著研究
            return View();
        }

        public ActionResult ProductSubCategory()
        { //先不刪 名駿說要留著研究
            return View();
        }



    }
}