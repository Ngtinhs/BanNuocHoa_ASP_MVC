using CustomUser_NoAuthen.DAL;
using CustomUser_NoAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomUser_NoAuthen.Controllers
{
    public class AuthenController : Controller
    {
        private readonly ShopContext db = new ShopContext();

        public ActionResult Login()
        {
            string _admin_user = "Admin";
            string _admin_pass = "Admin";

            NguoiDung user = db.NguoiDungs
                .Where(s => s.Username == _admin_user && s.Password == _admin_pass)
                .FirstOrDefault();

            if (user == null)
            {
                VaiTro vaiTro = new VaiTro
                {
                    RoleName = "Admin"
                };

                NguoiDung admin = new NguoiDung
                {
                    Email = "a@gmail.com",
                    Username = _admin_user,
                    Password = _admin_pass,
                    UserRoleId = vaiTro.UserRoleId
                };

                db.VaiTros.Add(vaiTro);
                db.NguoiDungs.Add(admin);
                db.SaveChanges();
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["User"] = null;
            Session["Userrole"] = null;
            Session["Username"] = null;
            Session["Order"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LoginSubmit(string username, string password)
        {
            NguoiDung user = db.NguoiDungs
                .Where(s => s.Username == username && s.Password == password)
                .FirstOrDefault();

            if (user != null && ModelState.IsValid)
            {
                var role = db.VaiTros.SingleOrDefault(x => x.UserRoleId == user.UserRoleId);

                FormsAuthentication.SetAuthCookie(user.Username, false);
                Session["User"] = user;
                Session["Userrole"] = role.RoleName;
                Session["Username"] = user.Username;

                if (role.RoleName == "Admin")
                    return RedirectToAction("Index", "Admin");

                var order = db.GioHangs.FirstOrDefault(x => x.UserIdentityId == user.UserIdentityId && x.OrderStatus == false);
                var detail = db.ChiTietGios.Where(x => x.OrderId == order.OrderId).ToList();

                Session["Order"] = detail.Count;
                return RedirectToAction("Index", "Home");
            }
            return View("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string email, string username, string password, string phone, string address)
        {
            var _role = db.VaiTros.SingleOrDefault(x => x.RoleName == "User");
            if (_role == null)
            {
                VaiTro vaiTro = new VaiTro
                {
                    RoleName = "User"
                };

                db.VaiTros.Add(vaiTro);
                db.SaveChanges();
            }

            if (ModelState.IsValid)
            {
                var role = db.VaiTros.SingleOrDefault(x => x.RoleName == "User");

                KhachHang khachHang = new KhachHang
                {
                    Email = email,
                    Username = username,
                    Password = password,
                    Phone = phone,
                    Address = address,
                    UserRoleId = role.UserRoleId

                };

                GioHang gioHang = new GioHang
                {
                    UserIdentityId = khachHang.UserIdentityId
                };

                db.NguoiDungs.Add(khachHang);
                db.GioHangs.Add(gioHang);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}