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