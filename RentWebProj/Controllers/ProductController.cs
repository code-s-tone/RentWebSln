using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentWebProj.Services;
using RentWebProj.ViewModels;
using RentWebProj.Models;
using RentWebProj.Helpers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace RentWebProj.Controllers
{
    public class ProductController : Controller
    {
        private ProductService _service;
        private RouteHelper _rhelper;
        public ProductController()
        {
            _service = new ProductService();
            _rhelper = new RouteHelper();
        }

        public ActionResult GeneralCategories()
        {
            ViewBag.Page = nameof(Pages.CategoriesCardsPage);
            ViewBag.Container = nameof(Container.CategoriesCardsContainer);
            ViewBag.ContainerTitle = nameof(ContainerTitle.種類列表);
            ViewBag.CategoryOptions = _service.GetCategoryData();
            return View("ProductList"); 
        }


        //取得商品資料 1.如果有帶categoryID就顯示該主類商品 2.沒帶或打all就顯示所有商品不分類
        public ActionResult ProductList(string categoryID) 
        {
            //1. 若使用者是打種類的單字(大於三碼) 先判斷種類代號
            if (categoryID!=null && categoryID.Length > 3)
            {
                categoryID=_rhelper.SwitchToCategoryID(categoryID);
            }
            
            //2. 沒指定或是打ALL 都秀出所有商品
            if (string.IsNullOrEmpty(categoryID) || categoryID.Trim().ToUpper() == "ALL")
            {
                ViewBag.ContainerTitle = nameof(ContainerTitle.所有商品);
            }
            else if (_service.GetCategoryName(categoryID)==null) //網址category後如果亂打  
            {
                ViewBag.ContainerTitle = nameof(ContainerTitle.很抱歉找不到您要的商品);
            }
            else
            {
                ViewBag.ContainerTitle="所有" + _service.GetCategoryName(categoryID);
            }
            
            ViewBag.Page = nameof(Pages.ProductCardsPage);
            ViewBag.Container = nameof(Container.ProductCardsContainer);
            ViewBag.CategoryOptions = _service.GetCategoryData();
            ViewBag.selectedProductList = _service.GetSelectedProductData(categoryID);
            return View();
        }

        //前端選了主類選項 出現副類
        [HttpGet]
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


        [HttpPost] //庭安卡片加購物車用 
        public ActionResult ProductToCart(string PID)
        {
            OperationResult result = new OperationResult();
            var cartService = new CartService();
            result = cartService.Create(PID);
            return Json(result.IsSuccessful); 
      
        }

        //接收路由PID撈產品資料、取當前登入者，傳到View
        public ActionResult ProductDetail(string PID)
        {
            ProductDetailToCart VM = _service.GetProductDetail(PID);
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

                    TempData["directCheckout"] = _service.ProductToCheckout(PID, PostVM.StartDate, PostVM.ExpirationDate);
                    return RedirectToAction("Checkout", "Carts");
                }
                var cartService = new CartService();
                result = cartService.CreateOrUpdate(PostVM, PID);
                isSuccessful = result.IsSuccessful;
            }

            //購物車可能已變動/違法輸入，需重撈
            ProductDetailToCart VM = _service.GetProductDetail(PID);
            VM.OperationSuccessful = isSuccessful;
            VM.OperationType = PostVM.IsExisted? "Update" : "Create";

            return View(VM);
        }

        //偉軒寫庫用
        //public async Task<ActionResult> LoL()
        //{
        //    string imgeSrc = "";

        //    var repository = new RentWebProj.Repositories.CommonRepository(new RentContext());
        //    var Mservice = new MemberService();
        //    //取資料
        //    var LoLList =
        //        (from p in repository.GetAll<Product>()
        //         where p.Discontinuation == true
        //         orderby p.ProductName
        //         select p).ToList();
        //    int i = 1;
        //    HttpClient client = new HttpClient();

        //    foreach (Product x in LoLList)
        //    {
        //        var championName = x.ProductName;
        //        var PID = "PplFt" + i.ToString().PadLeft(3, '0');

        //        repository.Delete(x);
        //        repository.SaveChanges();
        //        //寫庫
        //        Product entity = new Product
        //        {
        //            ProductID = PID,
        //            ProductName = x.ProductID, //保留中文名字
        //            Description = x.Description,
        //            DailyRate = (decimal)Math.Pow((double)x.DailyRate, 3),
        //            LaunchDate = x.LaunchDate,
        //            WithdrawalDate = x.WithdrawalDate,
        //            Discontinuation = false,
        //            UpdateTime = x.UpdateTime
        //        };

        //        repository.Create(entity);
        //        repository.SaveChanges();
        //        for (int j = 0; ; j++)
        //        {
        //            imgeSrc = $"https://ddragon.leagueoflegends.com/cdn/img/champion/splash/{championName}_{j}.jpg";
        //            //imgeSrc = $"https://cdngarenanow-a.akamaihd.net/webmain/static/pss/lol/items_splash/{championName}_{j}.jpg";
        //            try
        //            {
        //                HttpResponseMessage response = await client.GetAsync(imgeSrc);
        //                response.EnsureSuccessStatusCode();
        //            }
        //            catch (Exception ex)
        //            {
        //                break;
        //            }
        //            //上圖，寫庫
        //            Mservice.FileUploadProductImage(PID, j + 1, imgeSrc);
        //        }
        //        i++;
        //    }
        //    return Content("完成");
        //}
    }
}