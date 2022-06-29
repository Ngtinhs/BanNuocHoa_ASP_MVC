using CustomUser_NoAuthen.DAL;
using CustomUser_NoAuthen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomUser_NoAuthen.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShopContext db = new ShopContext();

        public ActionResult Index()
        {
            var user = Session["User"] as KhachHang;
            if (user == null) return RedirectToAction("Login", "Authen");

            var order = db.GioHangs.FirstOrDefault(x => x.UserIdentityId == user.UserIdentityId && x.OrderStatus == false);
            var detail = db.ChiTietGios.Where(x => x.OrderId == order.OrderId).ToList();

            if (detail == null) return View();

            List<ChiTietGioSanPhamViewModel> chiTietGios = new List<ChiTietGioSanPhamViewModel>();
            foreach (var item in detail)
            {
                SanPham sanPham = db.SanPhams.Find(item.ProductId);
                chiTietGios.Add(new ChiTietGioSanPhamViewModel
                {
                    ProductId = sanPham.ProductId,
                    ProductName = sanPham.Name,
                    ProductPrice = sanPham.Price,
                    ProductDiscount = sanPham.Discount,
                    ProductThumbnail = sanPham.Thumnail,
                    ProductQuantity = item.Quantity,
                    ProductTotalEach = item.TotalEach
                });
            }

            var _quantity = 0;
            var _totalPrice = 0;
            foreach (var item in chiTietGios)
            {
                _quantity += item.ProductQuantity;
                _totalPrice += item.ProductTotalEach;
            }

            GioHangViewModel gioHang = new GioHangViewModel
            {
                UserName = user.Username,
                UserPhone = user.Phone,
                UserAddress = user.Address,
                Quantity = _quantity,
                TotalPrice = _totalPrice,
                chiTiets = chiTietGios
            };

            return View(gioHang);
        }

        public ActionResult Insert(string returnUrl, int productId)
        {
            KhachHang user = Session["User"] as KhachHang;
            if (user == null) return RedirectToAction("Login", "Authen");

            var order = db.GioHangs.FirstOrDefault(x => x.UserIdentityId == user.UserIdentityId && x.OrderStatus == false);
            var detail = db.ChiTietGios.FirstOrDefault(x => x.ProductId == productId && x.OrderId == order.OrderId);
            var product = db.SanPhams.FirstOrDefault(x => x.ProductId == productId);

            var price = product.Price;
            if (product.Discount > 0)
            {
                price -= price * product.Discount / 100;
            }

            if (detail == null)
            {
                var _quantity = Convert.ToInt32(Session["Order"]);
                _quantity++;
                Session["Order"] = _quantity;

                ChiTietGio chiTietGio = new ChiTietGio
                {
                    ProductId = productId,
                    OrderId = order.OrderId,
                    Quantity = 1,
                    TotalEach = price
                };

                db.ChiTietGios.Add(chiTietGio);
            }
            else
            {
                detail.Quantity++;
                detail.TotalEach += price;
                db.Entry(detail).State = EntityState.Modified;
            }

            db.SaveChanges();
            return Redirect(returnUrl);
            //return null;
        }

        public ActionResult Delete(int productId)
        {
            var user = Session["User"] as KhachHang;
            var order = db.GioHangs.FirstOrDefault(x => x.UserIdentityId == user.UserIdentityId && x.OrderStatus == false);
            var detail = db.ChiTietGios.SingleOrDefault(x => x.OrderId == order.OrderId && x.ProductId == productId);

            db.ChiTietGios.Remove(detail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditTotal(int productId, int altTotal)
        {
            var user = Session["User"] as KhachHang;
            var order = db.GioHangs.FirstOrDefault(x => x.UserIdentityId == user.UserIdentityId && x.OrderStatus == false);

            var chiTietGio = db.ChiTietGios.FirstOrDefault(x => x.ProductId == productId && x.OrderId == order.OrderId);
            if (chiTietGio.Quantity + altTotal >= 0)
            {
                var sanPham = db.SanPhams.Find(productId);
                var total = sanPham.Price;
                if (sanPham.Discount > 0) 
                    total = sanPham.Price - sanPham.Price * sanPham.Discount / 100;

                chiTietGio.Quantity += altTotal;
                chiTietGio.TotalEach += total * altTotal;
            }

            db.Entry(chiTietGio).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}