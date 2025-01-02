using Microsoft.EntityFrameworkCore;
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

        public DbSet<CHI_TIET_DON_HANG> CHI_TIET_DON_HANG { get; set; }
        public DbSet<CHI_TIET_BINH_LUAN> CHI_TIET_BINH_LUAN { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


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


            // Cấu hình khóa chính composite cho bảng CHI_TIET_DON_HANG
            modelBuilder.Entity<CHI_TIET_DON_HANG>()
                .HasKey(ctdh => new { ctdh.MA_DH, ctdh.MA_SP });

            // Thiết lập quan hệ 1-N giữa DonHang và ChiTietDonHang
            modelBuilder.Entity<CHI_TIET_DON_HANG>()
                .HasOne(ctdh => ctdh.DonHang)
                .WithMany(dh => dh.ChiTietDonHangs)
                .HasForeignKey(ctdh => ctdh.MA_DH);

            // Thiết lập quan hệ 1-N giữa SanPham và ChiTietDonHang
            modelBuilder.Entity<CHI_TIET_DON_HANG>()
                .HasOne(ctdh => ctdh.SanPham)
                .WithMany(sp => sp.ChiTietDonHangs)
                .HasForeignKey(ctdh => ctdh.MA_SP);


            // Cấu hình khóa chính composite cho bảng CHI_TIET_BINH_LUAN
            modelBuilder.Entity<CHI_TIET_BINH_LUAN>()
                .HasKey(ctbl => new { ctbl.MA_KH, ctbl.MA_SP });

            // Thiết lập quan hệ 1-N giữa SanPham và ChiTietBinhLuan
            modelBuilder.Entity<CHI_TIET_BINH_LUAN>()
                .HasOne(ctbl => ctbl.SanPham)
                .WithMany(dh => dh.ChiTietBinhLuans)
                .HasForeignKey(ctdh => ctdh.MA_SP);

            // Thiết lập quan hệ 1-N giữa KhachHang và ChiTietDonHang
            modelBuilder.Entity<CHI_TIET_BINH_LUAN>()
                .HasOne(ctdh => ctdh.KhachHang)
                .WithMany(sp => sp.ChiTietBinhLuans)
                .HasForeignKey(ctdh => ctdh.MA_KH);

        }


    }
}
