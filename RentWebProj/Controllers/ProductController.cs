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
        public ProductController()
        {
            _service = new ProductService();
        }

        //實際頁面
        public ActionResult GeneralCategories()
        {
            ViewBag.Page = "CategoriesCardsPage";
            ViewBag.Container = "CategoriesCardsContainer";
            ViewBag.ContainerTitle = "所有種類";
            ViewBag.CategoryOptions = _service.GetCategoryData();
            ViewBag.SubCategoryOptions = _service.GetCategoryData();
            return View("Category_Product_Cards",_service.GetCategoryData()); 
        }
        public ActionResult Category_Product_Cards(string categoryID) //路由先暫時用categoryID 至於搜尋待考慮是否改為productID
        {
            ViewBag.Page = "ProductCardsPage";
            ViewBag.Container = "ProductCardsContainer";
            ViewBag.CategoryOptions = _service.GetCategoryData();
            ViewBag.SubCategoryOptions = _service.GetCategoryData();
            var selectedCtProductList = _service.GetProductData(categoryID);
            //ViewBag.ContainerTitle=selectedCtProductList.
            return View(selectedCtProductList);
        }
     
        [HttpPost] //前端選了主類選項 出現副類
        public ActionResult GetSubCategoryOptions(string categoryID)
        {
            return Json(_service.GetSubCategoryOptions(categoryID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost] //前端搜尋篩選
        public ActionResult GetSelectedProductCards(string categoryID)
        {
            var selectedCtProductList = _service.GetSubCategoryOptions(categoryID);
            return Json(selectedCtProductList,JsonRequestBehavior.AllowGet);
        }


        //---------------------------------------------------------------

        public ActionResult TEST_ProductCategory()
        {//----------------------以下先不刪 名駿說要留著研究

            return View();
        }

        public ActionResult TEST_ProductSubCategory()
        { //----------------------以下先不刪 名駿說要留著研究
            return View();
        }
        //---------------------------------------------------------------

        public ActionResult Product(string PID)
        {
            //接收路由PID撈產品資料、取MID，傳到View

            //如何取當前登入者?
            //memberID的取法session["Email"]?
            //C#如何取session?
            ProductDetailToCart VM = _service.getProductDetail(PID,1);
            return View(VM);
        }

        //通過模型驗證=>	呼叫service 寫入資料庫
        //不通過=> 路由PID撈產品資料，加入表單post過來的租借期間=>回填
        [HttpPost]
        public ActionResult Product(ProductDetailToCart PostVM , string ProductName, string PID) {
            //不使用架構
            if (ModelState.IsValid)
            {
                CartService service = new CartService();
                OperationResult result = service.Create(PostVM, PID);
                //以下是Bill教的錯誤log
                if (result.IsSuccessful)
                {
                    //MessageBox.Show("資料庫寫入成功");
                }
                else
                {
                    //var path = result.WriteLog();
                    //MessageBox.Show($"發生錯誤，請參考{path}");
                }
            }
            //因為購物車已變動，重新撈，重新顯示 => return View
            ProductDetailToCart VM = _service.getProductDetail(PID, PostVM.CurrentMemberID);
            return View(VM);//由於共用View、網址，型別必須跟Get方法的一致


            //表單室友指定網址  是分開的 個有一個提交按鈕
        }
    }
}