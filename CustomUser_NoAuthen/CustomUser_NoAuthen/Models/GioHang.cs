using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CustomUser_NoAuthen.Models
{
    public class GioHang
    {
        [Key]
        public int OrderId { get; set; }
        public bool OrderStatus { get; set; }

        public int UserIdentityId { get; set; }
        public KhachHang KhachHang { get; set; }

        public virtual ICollection<ChiTietGio> ChiTietGios { get; set; }

        public GioHang()
        {
            OrderStatus = false;
        }
    }

    public class ChiTietGio
    {
        [Key, Column(Order = 1)]
        public int OrderId { get; set; }
        public virtual GioHang GioHang { get; set; }

        [Key, Column(Order = 2)]
        public int ProductId { get; set; }
        public virtual SanPham SanPham { get; set; }

        public int Quantity { get; set; }
        public int TotalEach { get; set; }
    }

    public class ThanhToan
    {
        [Key]
        public int PaymentId { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public DateTime DateInsert { get; set; }

        public int OrderId { get; set; }
        public virtual GioHang GioHang { get; set; }

        public ThanhToan()
        {
            DateInsert = DateTime.UtcNow.Date;
        }
    }
}