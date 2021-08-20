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
        //private CartService _CartService;
        public ProductController()
        {
            _service = new ProductService();
            //_CartService = new ProductService();
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
        public ActionResult Product(string PID)
        {
            //string PID = "PplPg002";//未來放參數
            ProductDetailView VM = _service.getProductDetail(PID);
            ViewBag.PID = PID;
            return View(VM);
        }

        [HttpPost]
        public ActionResult ProductToCart(string PID , DateTime StartDate , DateTime ExpirationDate)
        {

            //收集VM群，傳給service
            //(cartCreate方法 的參數應該是 PID DateTime StartDate , DateTime ExpirationDate)
            //ProductDetailView VM ;

            //var result = service.Create(viewModel);
            //if (result.IsSuccessful)
            //{
            //    //成功
            //    MessageBox.Show("業務員加入成功");
            //}
            //else
            //{
            //    //失敗
            //    var path = result.WriteLog();
            //    MessageBox.Show($"發生錯誤，請參考 {path}");
            //    ViewBag.PID = PID;
            //    return View("Product", VM);
            //}

            return null;

        }
    }
}