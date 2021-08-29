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
            return View("ProductCardsList"); 
        }
        public ActionResult ProductCardsList(string categoryID) //路由先暫時用categoryID 至於搜尋待考慮是否改為productID
        {
            ViewBag.Page = nameof(Pages.ProductCardsPage);
            ViewBag.Container = nameof(Container.ProductCardsContainer);
            ViewBag.CategoryOptions = _service.GetCategoryData();
            var selectedCtProductList = _service.GetProductData(categoryID);
            ViewBag.ContainerTitle = "所有"+_service.GetCategoryName(categoryID);
            return View(selectedCtProductList);
        }
     
        [HttpPost] //前端選了主類選項 出現副類
        public ActionResult GetSubCategoryOptions(string categoryID)
        {
            return Json(_service.GetSubCategoryOptions(categoryID), JsonRequestBehavior.AllowGet);
        }

        [HttpPost] //前端搜尋篩選
        public ActionResult SearchProductCards(FormCollection filterForm)
        {
            var selectedCtProductList = _service.SearchProductCards(filterForm);
                //keywordInput, categoryOptions, subCategoryOptions, orderByOptions, dailyRateBudget);
            ViewBag.Page = nameof(Pages.ProductCardsPage);
            ViewBag.Container = nameof(Container.ProductCardsContainer);
            ViewBag.CategoryOptions = _service.GetCategoryData();
            if (selectedCtProductList.Count() == 0)
            {
                ViewBag.ContainerTitle = nameof(ContainerTitle.您要的商品);
            }
            
            ViewBag.FilterForm = filterForm;
            return View("ProductCardsList", selectedCtProductList);
        }


        //---------------------------------------------------------------


        public ActionResult ProductDetail(string PID)
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
        public ActionResult ProductDetail([Bind(Include = "isExisted,StartDate,ExpirationDate")] ProductDetailToCart PostVM , string PID) {
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