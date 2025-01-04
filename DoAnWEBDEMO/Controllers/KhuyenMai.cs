using DoAnWEBDEMO.Models;

namespace DoAnWEBDEMO.Controllers
{
    public class KhuyenMai
    {
        public int Id { get; set; }
        public int SanPhamKhuyenMaiId { get; set; } 
        public decimal MucGiamGia { get; set; } 
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public  SanPham SanPham { get; set; }
    }
}
