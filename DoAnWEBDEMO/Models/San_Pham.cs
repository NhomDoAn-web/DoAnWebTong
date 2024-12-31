using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class San_Pham
    {
        [Key]
        public int MaSP { get; set; } 

        [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc.")]
        [ForeignKey("NhaCungCap")]
        public int MaNCC { get; set; } 

        [Required(ErrorMessage = "Mã danh mục là bắt buộc.")]
        public int MaDanhMuc { get; set; } 

        [StringLength(int.MaxValue)]
        public string? HinhAnhSP { get; set; } 

        [StringLength(int.MaxValue)]
        public string? MoTa { get; set; } 

        [StringLength(50, ErrorMessage = "Màu sản phẩm không được vượt quá 50 ký tự.")]
        public string? MauSP { get; set; } 
        [Required(ErrorMessage = "Thời gian bảo hành là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Thời gian bảo hành phải lớn hơn hoặc bằng 0.")]
        public int TGianBaoHanh { get; set; } 

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        [Range(0, 1, ErrorMessage = "Trạng thái phải là 0 (Không hoạt động) hoặc 1 (Hoạt động).")]
        public int TrangThai { get; set; } 

        [Required(ErrorMessage = "Slug không được để trống.")]
        [StringLength(255, ErrorMessage = "Slug không được vượt quá 255 ký tự.")]
        public string? Slug { get; set; }

        public ICollection<CHI_TIET_DON_HANG>? ChiTietDonHangs { get; set; }
        public ICollection<CHI_TIET_BINH_LUAN>? ChiTietBinhLuans { get; set; }


    }
}
