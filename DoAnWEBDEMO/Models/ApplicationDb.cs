using Microsoft.EntityFrameworkCore;

namespace DoAnWEBDEMO.Models
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options) { }

 
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<TaiKhoanNhanVien> TaiKhoanNhanVien { get; set; }

    }
}
