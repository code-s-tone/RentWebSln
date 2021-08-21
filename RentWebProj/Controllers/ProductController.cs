using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.Services;
using RentWebProj.ViewModels;
using RentWebProj.Models;

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
            //取當前登入者?//memberID的取法session["Email"]?
            ProductDetailToCart VM = _service.getProductDetail(PID,1);
            return View(VM);
        }
        [HttpPost]
        public ActionResult Product(ProductDetailToCart PostVM , string ProductName, string PID) {
            //不使用架構
            //if (ModelState.IsValid)
            //{
            //    RentContext ctx = new RentContext();
            //    //VM->DM
            //    Cart cart = new Cart()
            //    {
            //        MemberID = (int)VM.CurrentMemberID,
            //        ProductID = VM.ProductID,
            //        StartDate = VM.StartDate,
            //        ExpirationDate = VM.ExpirationDate
            //    };

            //    //判斷加入或更新
            //    if(VM.StartDate !=null && VM.ExpirationDate != null)
            //    {//更新
            //    }
            //    else
            //    {//加入
            //        ctx.Carts.Add(cart);
            //    }
            //    ctx.SaveChanges();
            //    return Content("資料庫寫入成功!");
            //}


            //也可以只用ID重新查詢，但比較慢
            //ProductDetailToCart VM = _service.getProductDetail(PID, PostVM.CurrentMemberID);
            return View(PostVM);//回填的體貼。由於共用View、網址，型別必須跟Get方法的一致
        }

        [HttpPost]
        public ActionResult ProductToCart(string PID , DateTime StartDate , DateTime ExpirationDate)
        {
            //資料庫寫入的程式

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