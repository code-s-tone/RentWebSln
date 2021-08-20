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

            return View(_service.GetCategoryData()); 
        }
        public ActionResult ProductCards()
        {
            var selectedProductList = _service.GetCategoryData();
            return View();
        }


        //---------------------------------------------------------------
        //----------------------以下先不刪 名駿說要留著研究
        public ActionResult ProductCategory()
        {
           
            return View();
        }

        public ActionResult ProductSubCategory()
        { 
            return View();
        }



    }
}