using DoAnWEBDEMO.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnWEBDEMO.Access
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<DON_HANG> DON_HANGs { get; set; }
        public DbSet<San_Pham> San_Phams { get; set; }

        public DbSet<CHI_TIET_DON_HANG> CHI_TIET_DON_HANGs { get; set; }
        public DbSet<KHACH_HANG> kHACH_HANGs { get; set; }
        public DbSet<CHI_TIET_BINH_LUAN> CHI_TIET_BINH_LUANs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
