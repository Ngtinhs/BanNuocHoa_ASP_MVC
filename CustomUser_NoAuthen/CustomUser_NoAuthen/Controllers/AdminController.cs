using CustomUser_NoAuthen.DAL;
using CustomUser_NoAuthen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomUser_NoAuthen.Controllers
{
    public class AdminController : Controller
    {
        private readonly ShopContext db = new ShopContext();

        public ActionResult Index()
        {
            var payment = db.ThanhToans.ToList();
            if (payment == null) return View();

            var last = payment.Last();
            List<ThanhToanNamViewModel> thanhToans = new List<ThanhToanNamViewModel>();
            foreach (var item in payment)
            {
                var year = item.DateInsert.Year;
                var month = item.DateInsert.Month;

                var _year = thanhToans.Count != 0 ? thanhToans.Count - 1 : 0;
                if (!thanhToans.Exists(x => x.YearPay == year))
                {
                    if (thanhToans.Count != 0)
                    {
                        foreach (var work in thanhToans[_year].months)
                        {
                            thanhToans[_year].YearQuantity += work.MonthQuantity;
                            thanhToans[_year].YearTotal += work.MonthTotal;
                        }
                    }

                    thanhToans.Add(new ThanhToanNamViewModel
                    {
                        YearPay = year,
                        months = new List<ThanhToanThangViewModel>()
                    });
                }

                if (!thanhToans[_year].months.Exists(x => x.MonthPay == month))
                {
                    thanhToans[_year].months.Add(new ThanhToanThangViewModel
                    {
                        MonthPay = month
                    });
                }

                var _month = thanhToans[_year].months.Count != 0 ? thanhToans[_year].months.Count - 1 : 0;
                thanhToans[_year].months[_month].MonthQuantity = item.Quantity;
                thanhToans[_year].months[_month].MonthTotal = item.Total;

                if (item.Equals(last))
                {
                    foreach (var work in thanhToans[_year].months)
                    {
                        thanhToans[_year].YearQuantity += work.MonthQuantity;
                        thanhToans[_year].YearTotal += work.MonthTotal;
                    }
                }
            }
            return View(thanhToans);
        }
    }
}