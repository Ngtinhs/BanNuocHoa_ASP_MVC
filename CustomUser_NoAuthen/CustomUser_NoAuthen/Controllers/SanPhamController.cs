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
    public class SanPhamController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: SanPham
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc).Include(s => s.NoiBan).Include(s => s.TheLoai).Include(s => s.ThuongHieu).Include(s => s.XuatXu);
            return View(sanPhams.ToList());
        }

        // GET: SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPham/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.DanhMucs, "TypeId", "Name");
            ViewBag.SellId = new SelectList(db.NoiBans, "SellId", "Name");
            ViewBag.CategoryId = new SelectList(db.TheLoais, "CategoryId", "Name");
            ViewBag.MarkId = new SelectList(db.ThuongHieus, "MarkId", "Name");
            ViewBag.OriginId = new SelectList(db.XuatXus, "OriginId", "Name");
            return View();
        }

        // POST: SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Price,Discount,DateInsert,Description,Sold,Thumnail,CategoryId,TypeId,OriginId,SellId,MarkId")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.DanhMucs, "TypeId", "Name", sanPham.TypeId);
            ViewBag.SellId = new SelectList(db.NoiBans, "SellId", "Name", sanPham.SellId);
            ViewBag.CategoryId = new SelectList(db.TheLoais, "CategoryId", "Name", sanPham.CategoryId);
            ViewBag.MarkId = new SelectList(db.ThuongHieus, "MarkId", "Name", sanPham.MarkId);
            ViewBag.OriginId = new SelectList(db.XuatXus, "OriginId", "Name", sanPham.OriginId);
            return View(sanPham);
        }

        // GET: SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.DanhMucs, "TypeId", "Name", sanPham.TypeId);
            ViewBag.SellId = new SelectList(db.NoiBans, "SellId", "Name", sanPham.SellId);
            ViewBag.CategoryId = new SelectList(db.TheLoais, "CategoryId", "Name", sanPham.CategoryId);
            ViewBag.MarkId = new SelectList(db.ThuongHieus, "MarkId", "Name", sanPham.MarkId);
            ViewBag.OriginId = new SelectList(db.XuatXus, "OriginId", "Name", sanPham.OriginId);
            return View(sanPham);
        }

        // POST: SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Price,Discount,DateInsert,Description,Sold,Thumnail,CategoryId,TypeId,OriginId,SellId,MarkId")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.DanhMucs, "TypeId", "Name", sanPham.TypeId);
            ViewBag.SellId = new SelectList(db.NoiBans, "SellId", "Name", sanPham.SellId);
            ViewBag.CategoryId = new SelectList(db.TheLoais, "CategoryId", "Name", sanPham.CategoryId);
            ViewBag.MarkId = new SelectList(db.ThuongHieus, "MarkId", "Name", sanPham.MarkId);
            ViewBag.OriginId = new SelectList(db.XuatXus, "OriginId", "Name", sanPham.OriginId);
            return View(sanPham);
        }

        // GET: SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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

        public ActionResult DanhSach()
        {
            DanhSachSanPhamViewModel list = new DanhSachSanPhamViewModel
            {
                Products = db.SanPhams.ToList(),
                Types = db.DanhMucs.ToList(),
                SellPlaces = db.NoiBans.ToList(),
                Origins = db.XuatXus.ToList(),
                Trademarks = db.ThuongHieus.ToList()
            };

            return View(list);
        }

        [HttpPost]
        public ActionResult DanhSach(IEnumerable<string> array)
        {
            if (array.ElementAtOrDefault(array.Count() - 1) == "000")
                return PartialView("_DanhSach", db.SanPhams.ToList());

            var sanpham = db.SanPhams.ToList();
            foreach (string item in array)
            {
                List<string> split = item.Split('_').ToList();
                string property = split.First();
                split.RemoveAt(0);

                switch (property)
                {
                    case "SellPlaces":
                        //sanpham = db.SanPhams.Where(x => split.Contains(x.SellId.ToString())).ToList();
                        sanpham = sanpham.Where(x => split.Contains(x.SellId.ToString())).ToList();
                        break;
                    case "TradeMarks":
                        //sanpham = db.SanPhams.Where(x => split.Contains(x.MarkId.ToString())).ToList();
                        sanpham = sanpham.Where(x => split.Contains(x.MarkId.ToString())).ToList();
                        break;
                    case "Origins":
                        //sanpham = db.SanPhams.Where(x => split.Contains(x.OriginId.ToString())).ToList();
                        sanpham = sanpham.Where(x => split.Contains(x.OriginId.ToString())).ToList();
                        break;
                }
            }

            return PartialView("_DanhSach", sanpham);
        }

        public ActionResult ChiTiet(int? id)
        {
            Session["SearchBar"] = null;
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            SanPham sANPHAM = db.SanPhams.Find(id);
            if (sANPHAM == null)
                return HttpNotFound();

            var item = from p in db.SanPhams
                       join o in db.XuatXus on p.OriginId equals o.OriginId
                       join m in db.ThuongHieus on p.MarkId equals m.MarkId
                       where p.ProductId == id
                       select new ChiTietSanPhamViewModel()
                       {
                           ProductId = p.ProductId,
                           ProductName = p.Name,
                           ProductPrice = p.Price,
                           ProductDiscount = p.Discount,
                           ProductSold = p.Sold,
                           ProductDescription = p.Description,
                           ProductThumbnail = p.Thumnail,
                           ProductOri = o.Name,
                           ProductMark = m.Name
                       };

            return View(item.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult ThanhLoc(string text)
        {
            if (text == "")
                Session["SearchBar"] = null;
            else
                Session["SearchBar"] = db.SanPhams.Where(x => x.Name.Contains(text)).Take(5).ToList();
            return PartialView("_SearchBar");
        }
    }
}
