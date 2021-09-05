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
            return View("ProductList"); 
        }
        public ActionResult ProductList(string categoryID) //路由先暫時用categoryID 至於搜尋待考慮是否改為productID
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
 

        [HttpGet] //前端搜尋篩選
        public ActionResult SearchProductCards(string keyword, string category, string subCategory, string rateBudget,string orderBy)
        {
            var filterForm = new FilterSearchViewModel
            {
                Keyword = keyword,
                Category = category,
                SubCategory = subCategory,
                RateBudget = rateBudget,
                OrderBy = orderBy
            };

            var selectedProductList = _service.SearchProductCards(filterForm);

            ViewBag.Page = nameof(Pages.ProductCardsPage);
            ViewBag.Container = nameof(Container.ProductCardsContainer);
            ViewBag.CategoryOptions = _service.GetCategoryData();
            ViewBag.CategorySelected = filterForm.Category;
            ViewBag.SubcategoryOptions = _service.GetSubCategoryOptions(filterForm.Category);
            ViewBag.selectedProductList = selectedProductList;
            ViewBag.ContainerTitle = selectedProductList.Count == 0 ? nameof(ContainerTitle.很抱歉找不到您要的商品) : nameof(ContainerTitle.您要的商品);

            


            return View("ProductList", filterForm);
        }

        //---------------------------------------------------------------
        //未登入 => disabled 或 點下去提示需登入
        //已登入 => 點下去，不跳轉地執行後端程式，呼叫CartService的 Create方法
        //                  =>IsSuccessful == false => 提示已加入過            
        //                    IsSuccessful == true => 提示加入成功

        public ActionResult ProductToCart(string PID)
        {
            OperationResult result = new OperationResult();
            var cartService = new CartService();
            result = cartService.Create(PID);
            if (result.IsSuccessful)
            {
                //成功代表有寫入
                return RedirectToAction("ProductList");
            }
            else
            {
                //失敗代表本來就在車內
                return RedirectToAction("ProductList");
            }
        }

        //接收路由PID撈產品資料、取當前登入者，傳到View
        public ActionResult ProductDetail(string PID)
        {
            //var test = HttpContext.Current.User.Identity.Name;
            int? MID = null; 
            if (User.Identity.IsAuthenticated)
            {
                MID = Helper.ConvertAuthNameToMemberId(User.Identity.Name);
            }

            ProductDetailToCart VM = _service.GetProductDetail(PID, MID);

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
            int? MID = null;
            if (User.Identity.IsAuthenticated)
            {
                MID = Helper.ConvertAuthNameToMemberId(User.Identity.Name);
            }
            ProductDetailToCart VM = _service.GetProductDetail(PID, MID);
            VM.OperationSuccessful = isSuccessful;
            VM.OperationType = PostVM.IsExisted? "Update" : "Create";

            return View(VM);
        }
    }
}