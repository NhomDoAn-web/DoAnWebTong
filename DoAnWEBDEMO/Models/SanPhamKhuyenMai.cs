namespace DoAnWEBDEMO.Models
{
    public class SanPhamKhuyenMai
    {
        public int MaDanhMuc { get; set; }
        public string MoTa { get; set; }
        public int MaSP { get; set; }
        public string TEN_SP { get; set; }
        public decimal Gia { get; set; }
        public string HinhAnhSP { get; set; }
        public decimal GiaSauKhiGiam { get; set; }

        public string Slug { get; set; }
    }
}
