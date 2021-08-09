using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentWebProj.Models;

namespace RentWebProj.Controllers
{
    public class testMemberC : Controller
    {
        //!!!!!! 參考用 !!!  不要更動!!!!!!!!!

        private RentContext db = new RentContext();

        // GET: testMemberC
        public ActionResult Index()
        {
            var members = db.Members.Include(m => m.SignWay);
            return View(members.ToList());
        }

        // GET: testMemberC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: testMemberC/Create
        public ActionResult Create()
        {
            ViewBag.SignWayID = new SelectList(db.SignWays, "SignWayID", "Description");
            return View();
        }

        // POST: testMemberC/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,Account,PasswordHash,FullName,Email,Phone,Address,Birthday,SignWayID,active")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SignWayID = new SelectList(db.SignWays, "SignWayID", "Description", member.SignWayID);
            return View(member);
        }

        // GET: testMemberC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.SignWayID = new SelectList(db.SignWays, "SignWayID", "Description", member.SignWayID);
            return View(member);
        }

        // POST: testMemberC/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,Account,PasswordHash,FullName,Email,Phone,Address,Birthday,SignWayID,active")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SignWayID = new SelectList(db.SignWays, "SignWayID", "Description", member.SignWayID);
            return View(member);
        }

        // GET: testMemberC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: testMemberC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
