namespace DoAnWEBDEMO.ViewModels
{
    public class ThongKeSanPhamViewModel
    {
        public string TenSanPham { get; set; }
        public int LuotMua { get; set; }
        public List<DateTime>? NgayThangNam { get; set; } // Thêm ngày tháng
    }

}
