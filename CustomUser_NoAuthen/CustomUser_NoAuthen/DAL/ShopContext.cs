using CustomUser_NoAuthen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustomUser_NoAuthen.DAL
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("name=ShopDbConnection")
        {
            
        }

        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<XuatXu> XuatXus { get; set; }
        public DbSet<NoiBan> NoiBans { get; set; }
        public DbSet<ThuongHieu> ThuongHieus { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<ChiTietGio> ChiTietGios { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }

        public System.Data.Entity.DbSet<CustomUser_NoAuthen.Models.SanPham> SanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChiTietGio>()
                .HasKey(c => new { c.OrderId, c.ProductId });

            modelBuilder.Entity<GioHang>()
                .HasMany(c => c.ChiTietGios)
                .WithRequired()
                .HasForeignKey(c => c.OrderId);

            modelBuilder.Entity<SanPham>()
                .HasMany(c => c.ChiTietGios)
                .WithRequired()
                .HasForeignKey(c => c.ProductId);
        }
    }
}