using DoAnWEBDEMO.Models;
using Microsoft.EntityFrameworkCore;

namespace DoAnWEBDEMO.Access
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<KHACH_HANG> kHACH_HANGs { get; set; }
        public DbSet<Tai_Khoan_Khach_Hang> Tai_Khoan_Khach_Hangs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



        }
    }
}
