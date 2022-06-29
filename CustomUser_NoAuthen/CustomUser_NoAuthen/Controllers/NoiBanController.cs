using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomUser_NoAuthen.DAL;
using CustomUser_NoAuthen.Models;

namespace CustomUser_NoAuthen.Controllers
{
    public class NoiBanController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: NoiBan
        public ActionResult Index()
        {
            return View(db.NoiBans.ToList());
        }

        // GET: NoiBan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoiBan noiBan = db.NoiBans.Find(id);
            if (noiBan == null)
            {
                return HttpNotFound();
            }
            return View(noiBan);
        }

        // GET: NoiBan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NoiBan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SellId,Name")] NoiBan noiBan)
        {
            if (ModelState.IsValid)
            {
                db.NoiBans.Add(noiBan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noiBan);
        }

        // GET: NoiBan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoiBan noiBan = db.NoiBans.Find(id);
            if (noiBan == null)
            {
                return HttpNotFound();
            }
            return View(noiBan);
        }

        // POST: NoiBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SellId,Name")] NoiBan noiBan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noiBan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noiBan);
        }

        // GET: NoiBan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoiBan noiBan = db.NoiBans.Find(id);
            if (noiBan == null)
            {
                return HttpNotFound();
            }
            return View(noiBan);
        }

        // POST: NoiBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NoiBan noiBan = db.NoiBans.Find(id);
            db.NoiBans.Remove(noiBan);
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
