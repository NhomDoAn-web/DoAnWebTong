namespace DoAnWEBDEMO.Models
{
    public class SanPhamYeuThich
    {
        public int Id { get; set; }

        public int KhachHangId { get; set; }
        public int SanPhamId { get; set; }

        public KhachHang? KhachHang { get; set; }
        public SanPham? SanPham { get; set; }
    }
}
