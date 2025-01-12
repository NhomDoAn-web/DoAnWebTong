using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class MauSac
    {
        [Key]
        public int ID_MauSac { get; set; } // Khóa chính

        [Required(ErrorMessage = "Tên màu không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên màu không được vượt quá 50 ký tự.")]
        public string TenMauSac { get; set; }

        [Required(ErrorMessage = "URL hình ảnh không được để trống.")]
        [Url(ErrorMessage = "URL hình ảnh không hợp lệ.")]
        public string HinhAnhSP_MauSac { get; set; }

        // Khóa ngoại
        [Required(ErrorMessage = "Mã sản phẩm không được để trống.")]
        public int MaSP { get; set; }

        // Thêm trường SoLuongTon_MS
        [Required(ErrorMessage = "Số lượng tồn không được để trống.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0.")]
        public int SoLuongTon_MS { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        [Range(0, 1, ErrorMessage = "Trạng thái phải là 0 (Không hoạt động) hoặc 1 (Hoạt động).")]
        public int TrangThai { get; set; }

        public SanPham? SanPham { get; set; }
        public ICollection<ChiTietGioHang>? ChiTietGioHangs { get; set; }
        public ICollection<ChiTietDonHang>? ChiTietDonHangs { get; set; }
    }
}
