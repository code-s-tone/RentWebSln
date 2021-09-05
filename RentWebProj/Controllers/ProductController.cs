﻿using System;
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
        //未登入 => 提示需登入或一律disabled
        //已登入 => 有IsExisted屬性，=>true => 提示已加入            
        //                           false => 導到某action，給予參數：ProductID 以及 IsExisted:false (篩選狀況的JSON?)
        //                                    action執行：  以ProductDetailToCart型別  呼叫CartService.CreateOrUpdate加入購物車
        //                                    1.return重新導向原action，自然重篩
        //                                    3.return View，由於沒有重撈資料，必須透過某些手段改變ProductID搭配的IsExisted
        //              篩選狀況如何記錄起來傳遞?


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
        public ActionResult ProductDetail([Bind(Include = "isExisted,StartDate,ExpirationDate")] ProductDetailToCart PostVM, bool isCheckout , string PID) {
            //紀錄操作種類、成敗
            OperationResult result = new OperationResult();
            bool isSuccessful = false;
            //錯誤訊息
            //租借日期已被下訂、違法輸入
            if (ModelState.IsValid)
            {
                var cartService = new CartService();
                result = cartService.CreateOrUpdate(PostVM, PID);
                if (isCheckout)
                {
                    List<CartIndex> CList = new List<CartIndex>();

                    var c = new CartIndex()
                    {
                        MemberID = 1,
                        ProductID = PID,
                        //StartDate = PostVM.StartDate,
                        //ExpirationDate = PostVM.ExpirationDate,
                        //DailyRate = (decimal)p.DailyRate,
                        Qty = 1,//
                        Available = true,//
                    };

                    var dateDiff = (c.ExpirationDate - c.StartDate).Value.Days; //TotalDays帶小數
                    c.DateDiff = dateDiff;
                    c.Sub = c.DailyRate * dateDiff;

                    CList.Add(c);
                    TempData["directCheckout"] = CList;
                    return RedirectToAction("Checkout", "Carts");

                }

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