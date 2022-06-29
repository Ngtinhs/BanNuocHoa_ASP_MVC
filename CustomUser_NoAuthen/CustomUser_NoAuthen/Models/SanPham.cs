using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomUser_NoAuthen.Models
{
    public class SanPham
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public DateTime DateInsert { get; set; }
        public string Description { get; set; }
        public int Sold { get; set; }
        public string Thumnail { get; set; }

        public int CategoryId { get; set; }
        public TheLoai TheLoai { get; set; }
        public int TypeId { get; set; }
        public DanhMuc DanhMuc { get; set; }
        public int OriginId { get; set; }
        public XuatXu XuatXu { get; set; }
        public int SellId { get; set; }
        public NoiBan NoiBan { get; set; }
        public int MarkId { get; set; }
        public ThuongHieu ThuongHieu { get; set; }

        public virtual ICollection<ChiTietGio> ChiTietGios { get; set; }

        public SanPham()
        {
            DateInsert = DateTime.UtcNow.Date;
            Sold = 0;
        }
    }

    public class TheLoai
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class DanhMuc
    {
        [Key]
        public int TypeId { get; set; }
        public string Name { get; set; }
    }

    public class XuatXu
    {
        [Key]
        public int OriginId { get; set; }
        public string Name { get; set; }
    }

    public class NoiBan
    {
        [Key]
        public int SellId { get; set; }
        public string Name { get; set; }
    }

    public class ThuongHieu
    {
        [Key]
        public int MarkId { get; set; }
        public string Name { get; set; }
    }
}