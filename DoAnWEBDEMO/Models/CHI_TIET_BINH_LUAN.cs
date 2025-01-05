using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class CHI_TIET_BINH_LUAN
    {

        [Key] // Đảm bảo trường này không được để trống
        public int MA_SP { get; set; } // Khóa ngoại tới bảng Sản phẩm

        [Key] // Đảm bảo trường này không được để trống
        public int MA_KH { get; set; } // Khóa ngoại tới bảng Khách hàng

        [Range(1, 5)] // Hạn chế giá trị từ 1 đến 5 cho số sao
        public int SO_SAO { get; set; } // Số sao đánh giá

        [StringLength(255)] // Giới hạn độ dài của nội dung đánh giá
        public string? NOI_DUNG { get; set; } // Nội dung đánh giá

        [Required] // Trường này không được để trống
        public DateTime NGAY { get; set; } // Ngày đánh giá

        public KhachHang? KhachHang { get; set; }
        public SanPham? SanPham { get; set; }
    }
}
