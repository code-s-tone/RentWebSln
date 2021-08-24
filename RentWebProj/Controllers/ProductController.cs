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
            //接收路由PID撈產品資料、取當前登入者，傳到View
            //User.Identity.Name
            ProductDetailToCart VM = _service.getProductDetail(PID,1);
            return View(VM);
        }

        //通過模型驗證=>	呼叫service 寫入資料庫
        //不通過=> 路由PID撈產品資料，加入表單post過來的租借期間=>回填
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Product([Bind(Include = "isExisted,StartDate,ExpirationDate")] ProductDetailToCart PostVM , string PID) {
            //紀錄操作種類、成敗
            string OperationType = null;
            OperationResult result = new OperationResult();
            if (ModelState.IsValid)
            {
                result = new CartService().CreateOrUpdate(PostVM, PID, ref OperationType);
            }
            //購物車可能已變動，需重撈
            ProductDetailToCart VM = _service.getProductDetail(PID, 1);
            VM.OperationType = OperationType;
            VM.OperationSuccessful = result.IsSuccessful;


            return View(VM);//由於共用View，型別必須跟Get方法的一致
        }
    }
}