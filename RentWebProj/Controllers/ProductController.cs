using RentWebProj.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentWebProj.Controllers
{
    public class ProductController : Controller
    {
        private ProductService _service;
        public ProductController()
        {
            _service = new ProductService();
        }

        //實際頁面
        public ActionResult GeneralCategories()
        {

            return View(_service.getCategoryData()); 
        }
        public ActionResult ProductCards()
        {
            var selectedProductList = _service.getCategoryData();
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