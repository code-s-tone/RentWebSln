using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentWebProj.Models;
using RentWebProj.Repositories;
using RentWebProj.Services;
using RentWebProj.ViewModels;

namespace RentWebProj.Controllers
{
    public class CartsController : Controller
    {
        private RentContext db = new RentContext();
        private CartService _cartService;
        // GET: Carts

        public CartsController()
        {
            _cartService = new CartService();
        }

        public ActionResult Checkout()
        {
            if(TempData["directCheckout"]!=null)
                return View();
            else
                return RedirectToAction("Index", "Carts");
        }
        [HttpPost]
        public ActionResult Checkout(CreateOrder PostVM)//IEnumerable<CartIndex> VM
        {
            //判斷日期是否可通過
            //造訂單、寫入庫
            //參數可能要調整
            
            //軒：我的直接結帳，有可能產品不在購物車中，故刪除時可考慮一下
            new OrderService().Create(PostVM);

            //刪除購物車要用郭懿的方法喔，在下面
            foreach (var PID in PostVM.ListProductID)
            {
                new CartService().DeleteCart(Int32.Parse(User.Identity.Name), PID);
                //new CartService().DeleteCart(1, PID);
            }


            return RedirectToAction("MemberCenter", "Member");
        }
        [Authorize]
        public ActionResult Index()
        {
            //要登帳號
            var carts = _cartService.GetCart(Int32.Parse(User.Identity.Name));
            ViewBag.Total = _cartService.GetCartTotal(Int32.Parse(User.Identity.Name));

            //免登預設1
            //var carts = _cartService.GetCart(1);            
            //ViewBag.Total = _cartService.GetCartTotal(1);

            return View(carts);
        }
        [HttpPost]
        public ActionResult Index(OrderDoubleCheck VM)
        {
            List<CartIndex> CList = new List<CartIndex>();

            //可能未考慮日期null
            for (int i = 0; i < VM.ListChecked.Count(); i++)
            {
                //只有有勾選 且有更動日期的 才弄
                if (VM.ListChecked[i] && VM.ListModified[i])
                {
                    //紀錄操作種類、成敗
                    OperationResult result = new OperationResult();
                    if (ModelState.IsValid)
                    {
                        //更新購物車時間
                        var CartVM = new ProductDetailToCart()
                        {
                            IsExisted = true,
                            StartDate = VM.ListStartDate[i],
                            ExpirationDate = VM.ListExpirationDate[i]
                        };
                        result = new CartService().CreateOrUpdate(CartVM, VM.ListProductID[i]);
                    }
                }
                if (VM.ListChecked[i])
                {
                    //CList.Add(_cartService.CheckCart(VM.ListProductID[i],1));
                    CList.Add(_cartService.CheckCart(VM.ListProductID[i], Int32.Parse(User.Identity.Name)));
                }
            }

            //自己傳資料到View
            return View("checkout", model: CList);
        }

        public ActionResult Delete(int MemberID, string ProductID)
        {
            _cartService.DeleteCart(MemberID, ProductID);
            return RedirectToAction("Index");
        }

    }
}
