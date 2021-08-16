using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.Services;
using RentWebProj.ViewModels;

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
        public ActionResult Product(string PID)
        {
            //string PID = "PplPg002";//未來放參數
            ProductDetailView VM = _service.getProductDetail(PID);
            ViewBag.PID = PID;
            return View(VM);
        }
    }
}