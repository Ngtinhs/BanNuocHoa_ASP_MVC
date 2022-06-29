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
    public class PaymentController : Controller
    {
        private readonly ShopContext db = new ShopContext();

        public ActionResult Index()
        {
            var user = Session["User"] as KhachHang;
            var order = db.GioHangs.FirstOrDefault(x => x.UserIdentityId == user.UserIdentityId && x.OrderStatus == false);
            var detail = db.ChiTietGios.Where(x => x.OrderId == order.OrderId).ToList();

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

        public ActionResult Pay(int quantity, int total)
        {
            var user = Session["User"] as KhachHang;
            var order = db.GioHangs.FirstOrDefault(x => x.UserIdentityId == user.UserIdentityId && x.OrderStatus == false);
            var detail = db.ChiTietGios.Where(x => x.OrderId == order.OrderId).ToList();

            order.OrderStatus = true;
            db.Entry(order).State = EntityState.Modified;

            foreach (var item in detail)
            {
                SanPham sanPham = db.SanPhams.Find(item.ProductId);
                sanPham.Sold += item.Quantity;
                db.Entry(sanPham).State = EntityState.Modified;
            }

            GioHang gioHang = new GioHang
            {
                UserIdentityId = user.UserIdentityId
            };

            ThanhToan thanhToan = new ThanhToan
            {
                Quantity = quantity,
                Total = total,
                OrderId = order.OrderId
            };

            db.GioHangs.Add(gioHang);
            db.ThanhToans.Add(thanhToan);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}