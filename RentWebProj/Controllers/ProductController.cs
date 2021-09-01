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

        public ActionResult GeneralCategories()
        {
            ViewBag.Page = nameof(Pages.CategoriesCardsPage);
            ViewBag.Container = nameof(Container.CategoriesCardsContainer);
            ViewBag.ContainerTitle = nameof(ContainerTitle.所有種類);
            ViewBag.CategoryOptions = _service.GetCategoryData();
            return View("ProductCardsList"); 
        }
        public ActionResult ProductCardsList(string categoryID) //路由先暫時用categoryID 至於搜尋待考慮是否改為productID
        {
            ViewBag.Page = nameof(Pages.ProductCardsPage);
            ViewBag.Container = nameof(Container.ProductCardsContainer);
            ViewBag.CategoryOptions = _service.GetCategoryData();
            ViewBag.selectedProductList = _service.GetProductData(categoryID);
            ViewBag.ContainerTitle = "所有"+_service.GetCategoryName(categoryID);
            return View();
        }
     
        [HttpPost] //前端選了主類選項 出現副類
        public ActionResult GetSubCategoryOptions(string categoryID)
        {
            return Json(_service.GetSubCategoryOptions(categoryID), JsonRequestBehavior.AllowGet);
        }
        
        //[HttpPost] //前端搜尋篩選
        //public ActionResult SearchProductCards(FormCollection filterForm)
        //{
        //    var selectedCtProductList = _service.SearchProductCards(filterForm);
                
        //    ViewBag.Page = nameof(Pages.ProductCardsPage);
        //    ViewBag.Container = nameof(Container.ProductCardsContainer);
        //    ViewBag.CategoryOptions = _service.GetCategoryData();

        //    ViewBag.ContainerTitle = selectedCtProductList.Count == 0? nameof(ContainerTitle.很抱歉找不到您要的商品):nameof(ContainerTitle.您要的商品);
            
        //    //ViewBag.FilterForm = filterForm;
        //    return View("ProductCardsList", selectedCtProductList);
        //}

        [HttpGet] //前端搜尋篩選
        public ActionResult SearchProductCards(string keywordInput, string categoryOptions, string subCategoryOptions, string dailyRateBudget,string orderByOptions)
        {
            var filterForm = new FilterSearchViewModel
            {
                keywordInput = keywordInput,
                categoryOptions = categoryOptions,
                subCategoryOptions = subCategoryOptions,
                dailyRateBudget = dailyRateBudget,
                orderByOptions = orderByOptions
            };

            var selectedProductList = _service.SearchProductCards(filterForm);

            ViewBag.Page = nameof(Pages.ProductCardsPage);
            ViewBag.Container = nameof(Container.ProductCardsContainer);
            ViewBag.CategoryOptions = _service.GetCategoryData();
            ViewBag.CategorySelected = filterForm.categoryOptions;
            ViewBag.SubcategoryOptions = _service.GetSubCategoryOptions(filterForm.categoryOptions);
            ViewBag.selectedProductList = selectedProductList;
            ViewBag.ContainerTitle = selectedProductList.Count == 0 ? nameof(ContainerTitle.很抱歉找不到您要的商品) : nameof(ContainerTitle.您要的商品);

            


            return View("ProductCardsList", filterForm);
        }

        //---------------------------------------------------------------


        //接收路由PID撈產品資料、取當前登入者，傳到View
        public ActionResult ProductDetail(string PID)
        {
            //User.Identity.Name
            ProductDetailToCart VM = _service.getProductDetail(PID,1);

            return View(VM);
        }

        //通過模型驗證=>	呼叫service 寫入資料庫
        //不通過=> 路由PID撈產品資料，加入表單post過來的租借期間=>回填
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDetail([Bind(Include = "isExisted,StartDate,ExpirationDate")] ProductDetailToCart PostVM , string PID) {
            //紀錄操作種類、成敗
            OperationResult result = new OperationResult();
            bool isSuccessful = false;
            //錯誤訊息
            //租借日期已被下訂、違法輸入
            if (ModelState.IsValid)
            {
                result = new CartService().CreateOrUpdate(PostVM, PID);
                isSuccessful = result.IsSuccessful;
            }

            //購物車可能已變動/違法輸入，需重撈
            ProductDetailToCart VM = _service.getProductDetail(PID, 1);
            VM.OperationSuccessful = isSuccessful;
            VM.OperationType = PostVM.IsExisted? "Update" : "Create";

            return View(VM);
        }
    }
}