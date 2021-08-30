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
        private IndexService _service;
        private CartService _cartService;
        // GET: Carts

        public CartsController()
        {
            _service = new IndexService();
            _cartService = new CartService();
        }

        public ActionResult Checkout()
        {

            var sb = TempData["DATA"];


            return View(sb);
  

        }
        [HttpPost]
        public ActionResult Checkout(IEnumerable<CartIndex> VM)
        {   
            //判斷日期是否可通過

            //造訂單、寫入庫
            //參數可能要調整
            new OrderService().Create(VM);

            return RedirectToAction("MemberCenter", "Member");
        }

        public ActionResult Index()
        {
            var carts = _cartService.GetCart(1);
            ViewBag.Total = _cartService.GetCartTotal(1);

            return View(carts);
        }
        [HttpPost]
        public ActionResult Index(OrderDoubleCheck VM)
        {

            //for (int i = 0, i < VM.ListChecked.Count();)

            //判斷Modefied
            //sevice 負責VM=>DM  (  , ,  , ,)
            //if (VM.IsExisted)
            //{//更新購物車
            //    Cart entity = new Cart()
            //    {
            //        MemberID = 1,
            //        ProductID = PID,
            //        StartDate = Convert.ToDateTime(VM.StartDate),//空字串能否轉?
            //        ExpirationDate = DateTime.Parse(VM.ExpirationDate)
            //    };

            //    _repository.Update(entity);//猜測會用PK去找到原有的資料
            //}

            //自己傳資料到View
            //return RedirectToAction("checkout", "carts");
            return View("checkout");// , model: _service.getCartsData(lstStuModel)
        }

        public ActionResult Delete(int MemberID, string ProductID)
        {
             _cartService.DeleteCart(MemberID, ProductID);
            return RedirectToAction("Index");
        }

        //public ActionResult Index()
        //{
        //    var carts = db.Carts.Include(c => c.Member).Include(c => c.Product);
        //    return View(carts.ToList());
        //}

        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Account");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            return View();
        }

        // POST: Carts/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,ProductID,StartDate,ExpirationDate")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Account", cart.MemberID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", cart.ProductID);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Account", cart.MemberID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", cart.ProductID);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,ProductID,StartDate,ExpirationDate")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.Members, "MemberID", "Account", cart.MemberID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", cart.ProductID);
            return View(cart);
        }

        //// GET: Carts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cart cart = db.Carts.Find(id);
        //    if (cart == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cart);
        //}

        //// POST: Carts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Cart cart = db.Carts.Find(id);
        //    db.Carts.Remove(cart);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
