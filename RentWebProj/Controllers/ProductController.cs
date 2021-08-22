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

            return View(_service.getCategoryData()); 
        }
        public ActionResult ProductCards()
        {
            var selectedProductList = _service.getCategoryData();
            return View();
        }

        public ActionResult ProductCategory()
        {
            //先不刪 名駿說要留著研究
            return View();
        }

        public ActionResult ProductSubCategory()
        { //先不刪 名駿說要留著研究
            return View();
        }

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
        }
    }
}