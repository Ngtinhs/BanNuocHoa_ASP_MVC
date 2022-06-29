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
    public class VaiTroController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: VaiTro
        public ActionResult Index()
        {
            return View(db.VaiTros.ToList());
        }

        // GET: VaiTro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaiTro vaiTro = db.VaiTros.Find(id);
            if (vaiTro == null)
            {
                return HttpNotFound();
            }
            return View(vaiTro);
        }

        // GET: VaiTro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VaiTro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserRoleId,RoleName")] VaiTro vaiTro)
        {
            if (ModelState.IsValid)
            {
                db.VaiTros.Add(vaiTro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaiTro);
        }

        // GET: VaiTro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaiTro vaiTro = db.VaiTros.Find(id);
            if (vaiTro == null)
            {
                return HttpNotFound();
            }
            return View(vaiTro);
        }

        // POST: VaiTro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserRoleId,RoleName")] VaiTro vaiTro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaiTro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vaiTro);
        }

        // GET: VaiTro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaiTro vaiTro = db.VaiTros.Find(id);
            if (vaiTro == null)
            {
                return HttpNotFound();
            }
            return View(vaiTro);
        }

        // POST: VaiTro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VaiTro vaiTro = db.VaiTros.Find(id);
            db.VaiTros.Remove(vaiTro);
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
