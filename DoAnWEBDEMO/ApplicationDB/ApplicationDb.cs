
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
        public DbSet<BaiViet> BaiViet { get; set; }
        public DbSet<PhuongThucThanhToan> PhuongThucThanhToan { get; set; }
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
                 .HasKey(c => new { c.MA_DH, c.MA_SP, c.MA_MAU }); // Thiết lập khóa chính

            modelBuilder.Entity<ChiTietDonHang>()
                .HasOne(c => c.MauSac)
                .WithMany(m => m.ChiTietDonHangs)
                .HasForeignKey(c => c.MA_MAU) // Thiết lập khóa ngoại
                .OnDelete(DeleteBehavior.Restrict); // Không xóa liên kết khi xóa màu sắc

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
                .HasKey(ctbl => new { ctbl.MA_KH, ctbl.MA_SP, ctbl.Id_BinhLuan });

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

            // Cấu hình khóa chính composite cho bảng ChiTietGioHang
            modelBuilder.Entity<ChiTietGioHang>()
                .HasKey(ctgh => new { ctgh.MaKH, ctgh.MaSP, ctgh.MaMau }); // Gồm cả MaMau nếu cần thiết

            // Quan hệ: ChiTietGioHang x KhachHang (1-N)
            modelBuilder.Entity<ChiTietGioHang>()
                .HasOne(ctgh => ctgh.KhachHang)
                .WithMany(kh => kh.ChiTietGioHangs)
                .HasForeignKey(ctgh => ctgh.MaKH)
                .OnDelete(DeleteBehavior.Cascade); // Xóa khách hàng sẽ xóa giỏ hàng liên quan

            // Quan hệ: ChiTietGioHang x SanPham (1-N)
            modelBuilder.Entity<ChiTietGioHang>()
                .HasOne(ctgh => ctgh.SanPham)
                .WithMany(sp => sp.ChiTietGioHangs)
                .HasForeignKey(ctgh => ctgh.MaSP)
                .OnDelete(DeleteBehavior.Cascade); // Xóa sản phẩm sẽ xóa giỏ hàng liên quan

            // Quan hệ: ChiTietGioHang x MauSac (1-N)
            modelBuilder.Entity<ChiTietGioHang>()
                .HasOne(ctgh => ctgh.MauSac)
                .WithMany(ms => ms.ChiTietGioHangs)
                .HasForeignKey(ctgh => ctgh.MaMau)
                .OnDelete(DeleteBehavior.Restrict); // Không cho phép xóa màu nếu có giỏ hàng tham chiếu

            // Thiết lập quan hệ 1-N giữa SanPham và MauSac
            modelBuilder.Entity<MauSac>()
            .HasOne(c => c.SanPham)
            .WithMany(p => p.MauSacs)
            .HasForeignKey(c => c.MaSP);

        }
    }
}



