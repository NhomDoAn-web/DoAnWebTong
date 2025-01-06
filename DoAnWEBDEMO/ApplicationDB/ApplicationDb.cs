
﻿using Microsoft.EntityFrameworkCore;
using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
namespace DoAnWEBDEMO.ApplicationDB
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options) { }

        public DbSet<DanhMuc> DanhMuc { get; set; }

        public DbSet<DonHang> DonHang { get; set; }
        public DbSet<KhachHang> KhachHang{ get; set; }
        public DbSet<LienHe> LienHe{ get; set; }
        public DbSet<NhaCungCap> NhaCungCap{ get; set; }
        public DbSet<NhanVien> NhanVien{ get; set; }
        public DbSet<SanPham> SanPham{ get; set; }
        public DbSet<SanPhamYeuThich> SanPhamYeuThich { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHang { get; set; }
        public DbSet<MauSac> MauSac { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
        public DbSet<ChiTietBinhLuan> ChiTietBinhLuan { get; set; }
        public DbSet<KhuyenMai> KhuyenMai { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChiTietDonHang>()
                .Property(c => c.TONGTIENTUNGSANPHAM)
                .HasColumnType("decimal(18,2)");  

            modelBuilder.Entity<DonHang>()
                .Property(d => d.TongTienDonHang)
                .HasColumnType("decimal(18,2)");  

            modelBuilder.Entity<SanPham>()
                .Property(s => s.Gia)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SanPhamYeuThich>()
               .HasKey(spyt => new { spyt.KhachHangId, spyt.SanPhamId });
            //Quan hệ: khách hàng x khuyến mãi
            modelBuilder.Entity<KhuyenMai>()
            .HasOne(d => d.SanPham)
            .WithMany(k => k.KhuyenMais)
            .HasForeignKey(d => d.SanPhamKhuyenMaiId)
            .OnDelete(DeleteBehavior.Cascade);

            //Quan hệ: khách hàng x Sản phẩm yêu thích 
            modelBuilder.Entity<SanPhamYeuThich>()
            .HasOne(d => d.KhachHang)
            .WithMany(k => k.SanPhamYeuThichs)
            .HasForeignKey(d => d.KhachHangId)
            .OnDelete(DeleteBehavior.Cascade);

            //Quan hệ: sản phẩm x Sản phẩm yêu thích 
            modelBuilder.Entity<SanPhamYeuThich>()
            .HasOne(d => d.SanPham)
            .WithMany(k => k.SanPhamYeuThichs)
            .HasForeignKey(d => d.SanPhamId)
            .OnDelete(DeleteBehavior.Cascade);

            //Quan hệ: Sản phẩm x Danh Mục
            modelBuilder.Entity<SanPham>()
                .HasOne(sp => sp.DanhMuc)
                .WithMany(dm => dm.SanPham)
                .HasForeignKey(p => p.MaDanhMuc) //Khóa ngoại...
                .OnDelete(DeleteBehavior.Cascade);

            //Quan hệ: Liên Hệ x Nhân Viên
            modelBuilder.Entity<LienHe>()
                .HasOne(lh => lh.NhanVien)
                .WithMany(nv => nv.LienHe)
                .HasForeignKey(p => p.MA_NVXL) //Khóa ngoại...
                .OnDelete(DeleteBehavior.Cascade);

            //Quan hệ: Sản phẩm x Nhà cung cấp
            modelBuilder.Entity<SanPham>()
                .HasOne(sp => sp.NhaCungCap)
                .WithMany(ncc => ncc.SanPham)
                .HasForeignKey(p => p.MaNCC) //Khóa ngoại...
                .OnDelete(DeleteBehavior.Cascade);

            //Quan hệ: Đơn hàng x Nhân Viên
            modelBuilder.Entity<DonHang>()
                .HasOne(dh => dh.NhanVien)
                .WithMany(nv => nv.DonHang)
                .HasForeignKey(p => p.MaNVXL) //Khóa ngoại...
                .OnDelete(DeleteBehavior.Cascade);

            //Quan hệ: Khách hàng x Đơn hàng
            modelBuilder.Entity<DonHang>()
                .HasOne(dh => dh.KhachHang)
                .WithMany(kh => kh.DonHang)
                .HasForeignKey(p => p.MaKH) //Khóa ngoại...
                .OnDelete(DeleteBehavior.Cascade);


            //========== Dương ============//
            // Cấu hình khóa chính composite cho bảng CHI_TIET_DON_HANG
            modelBuilder.Entity<ChiTietDonHang>()
                .HasKey(ctdh => new { ctdh.MA_DH, ctdh.MA_SP });

            // Thiết lập quan hệ 1-N giữa DonHang và ChiTietDonHang
            modelBuilder.Entity<ChiTietDonHang>()
                .HasOne(ctdh => ctdh.DonHang)
                .WithMany(dh => dh.ChiTietDonHangs)
                .HasForeignKey(ctdh => ctdh.MA_DH);

            // Thiết lập quan hệ 1-N giữa SanPham và ChiTietDonHang
            modelBuilder.Entity<ChiTietDonHang>()
                .HasOne(ctdh => ctdh.SanPham)
                .WithMany(sp => sp.ChiTietDonHangs)
                .HasForeignKey(ctdh => ctdh.MA_SP);


            // Cấu hình khóa chính composite cho bảng CHI_TIET_BINH_LUAN
            modelBuilder.Entity<ChiTietBinhLuan>()
                .HasKey(ctbl => new { ctbl.MA_KH, ctbl.MA_SP });

            // Thiết lập quan hệ 1-N giữa SanPham và ChiTietBinhLuan
            modelBuilder.Entity<ChiTietBinhLuan>()
                .HasOne(ctbl => ctbl.SanPham)
                .WithMany(dh => dh.ChiTietBinhLuans)
                .HasForeignKey(ctdh => ctdh.MA_SP);

            // Thiết lập quan hệ 1-N giữa KhachHang và ChiTietDonHang
            modelBuilder.Entity<ChiTietBinhLuan>()
                .HasOne(ctdh => ctdh.KhachHang)
                .WithMany(sp => sp.ChiTietBinhLuans)
                .HasForeignKey(ctdh => ctdh.MA_KH);
            // Cấu hình khóa chính composite cho bảng chi tiết giỏ hàng
            modelBuilder.Entity<ChiTietGioHang>()
                .HasKey(ctgh => new { ctgh.MaKH, ctgh.MaSP });
            //Quan hệ: Giỏ hàng x Khách hàng
            modelBuilder.Entity<ChiTietGioHang>()
                .HasOne(g => g.KhachHang)
                .WithMany(kh => kh.GioHang)
                .HasForeignKey(p => p.MaKH) //Khóa ngoại...
                .OnDelete(DeleteBehavior.Cascade);
            //Quan hệ: Giỏ hàng x Sản phẩm
            modelBuilder.Entity<ChiTietGioHang>()
                .HasOne(g => g.SanPham)
                .WithMany(kh => kh.GioHang)
                .HasForeignKey(p => p.MaSP) 
                .OnDelete(DeleteBehavior.Cascade);

            // Thiết lập quan hệ 1-N giữa SanPham và MauSac
            modelBuilder.Entity<MauSac>()
            .HasOne(c => c.SanPham)              
            .WithMany(p => p.MauSacs)          
            .HasForeignKey(c => c.MaSP);  

            // Thiết lập quan hệ 1-N giữa ChiTietGioHang và MauSac
            modelBuilder.Entity<ChiTietGioHang>()
            .HasOne(c => c.MauSac) 
            .WithMany(m => m.ChiTietGioHangs) 
            .HasForeignKey(c => c.MaMau);
        }
    }
}



