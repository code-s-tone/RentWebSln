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
        //未登入 => 提示需登入或一律disabled
        //已登入 => 有IsExisted屬性，=>true => 提示已加入            
        //                           false => 導到某action，給予參數：ProductID 以及 IsExisted:false (篩選狀況的JSON?)
        //                                    action執行：  以ProductDetailToCart型別  呼叫CartService.CreateOrUpdate加入購物車
        //                                    1.return重新導向原action，自然重篩
        //                                    3.return View，由於沒有重撈資料，必須透過某些手段改變ProductID搭配的IsExisted
        //              篩選狀況如何記錄起來傳遞?  或者 如何不跳轉也執行
        public ActionResult ProductToCart(string PID)
        {
            OperationResult result = new OperationResult();
            var cartService = new CartService();
            result = cartService.Create(PID);
            if (result.IsSuccessful)
            {
                //成功代表有寫入
                return RedirectToAction("ProductCardsList");
            }
            else
            {
                //失敗代表本來就在車內
                return RedirectToAction("ProductCardsList");
            }
        }

        //接收路由PID撈產品資料、取當前登入者，傳到View
        public ActionResult ProductDetail(string PID)
        {
            //User.Identity.Name
            ProductDetailToCart VM = _service.GetProductDetail(PID,1);

            return View(VM);
        }

        //通過模型驗證=>	呼叫service 寫入資料庫
        //不通過=> 路由PID撈產品資料，加入表單post過來的租借期間=>回填
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductDetail([Bind(Include = "isExisted,StartDate,ExpirationDate")] ProductDetailToCart PostVM, string PID, bool isCheckout ) {
            //紀錄操作種類、成敗
            OperationResult result = new OperationResult();
            bool isSuccessful = false;
            //錯誤訊息 違法輸入
            if (ModelState.IsValid)
            {
                if (isCheckout)
                {
                    //不寫入購物車
                    TempData["directCheckout"] = _service.ProductToCheckout(PID, PostVM.StartDate , PostVM.ExpirationDate);
                    return RedirectToAction("Checkout", "Carts");
                }
                var cartService = new CartService();
                result = cartService.CreateOrUpdate(PostVM, PID);
                isSuccessful = result.IsSuccessful;
            }

            //購物車可能已變動/違法輸入，需重撈
            ProductDetailToCart VM = _service.GetProductDetail(PID, 1);
            VM.OperationSuccessful = isSuccessful;
            VM.OperationType = PostVM.IsExisted? "Update" : "Create";

            return View(VM);
        }
    }
}