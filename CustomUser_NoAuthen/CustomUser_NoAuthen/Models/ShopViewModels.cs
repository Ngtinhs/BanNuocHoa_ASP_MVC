using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomUser_NoAuthen.Models
{
    public class DanhSachSanPhamViewModel
    {
        public List<SanPham> Products { get; set; }
        public List<DanhMuc> Types { get; set; }
        public List<NoiBan> SellPlaces { get; set; }
        public List<XuatXu> Origins { get; set; }
        public List<ThuongHieu> Trademarks { get; set; }
    }

    public class ChiTietSanPhamViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductDiscount { get; set; }
        public int ProductSold { get; set; }
        public string ProductDescription { get; set; }
        public string ProductThumbnail { get; set; }
        public string ProductOri { get; set; }
        public string ProductMark { get; set; }
    }

    public class ChiTietGioSanPhamViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductDiscount { get; set; }
        public string ProductThumbnail { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductTotalEach { get; set; }
    }

    public class GioHangViewModel
    {
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public List<ChiTietGioSanPhamViewModel> chiTiets { get; set; }
    }

    public class ThanhToanNamViewModel
    {
        public int YearPay { get; set; }
        public int YearQuantity { get; set; }
        public int YearTotal { get; set; }
        public List<ThanhToanThangViewModel> months { get; set; }

        public ThanhToanNamViewModel()
        {
            YearQuantity = 0;
            YearTotal = 0;
        }
    }

    public class ThanhToanThangViewModel
    {
        public int MonthPay { get; set; }
        public int MonthQuantity { get; set; }
        public int MonthTotal { get; set; }

        public ThanhToanThangViewModel()
        {
            MonthQuantity = 0;
            MonthTotal = 0;
        }
    }
}