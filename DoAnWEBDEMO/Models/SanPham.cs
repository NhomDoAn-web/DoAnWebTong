using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }

        [Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc.")]
        [ForeignKey("NhaCungCap")]
        public int MaNCC { get; set; }

        [Required(ErrorMessage = "Mã danh mục là bắt buộc.")]
        public int MaDanhMuc { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        public string TEN_SP { get; set; }

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

        [Required(ErrorMessage = "Kích thước màn hình không được để trống.")]
        [StringLength(50, ErrorMessage = "Kích thước màn hình không được vượt quá 50 ký tự.")]
        public string? KichThuoc_ManHinh { get; set; }

        [Required(ErrorMessage = "Thông tin camera sau không được để trống.")]
        [StringLength(255, ErrorMessage = "Thông tin camera sau không được vượt quá 255 ký tự.")]
        public string? CameraSau { get; set; }

        [Required(ErrorMessage = "Thông tin camera trước không được để trống.")]
        [StringLength(255, ErrorMessage = "Thông tin camera trước không được vượt quá 255 ký tự.")]
        public string? Camera_Truoc { get; set; }

        [Required(ErrorMessage = "Chipset không được để trống.")]
        [StringLength(255, ErrorMessage = "Thông tin chipset không được vượt quá 255 ký tự.")]
        public string? Chipset { get; set; }

        [Required(ErrorMessage = "GPU không được để trống.")]
        [StringLength(255, ErrorMessage = "Thông tin GPU không được vượt quá 255 ký tự.")]
        public string? Gpu { get; set; }

        [Required(ErrorMessage = "Dung lượng RAM không được để trống.")]
        [StringLength(50, ErrorMessage = "Dung lượng RAM không được vượt quá 50 ký tự.")]
        public string? Dung_Luong_Ram { get; set; }

        [Required(ErrorMessage = "Bộ nhớ trong không được để trống.")]
        [StringLength(50, ErrorMessage = "Bộ nhớ trong không được vượt quá 50 ký tự.")]
        public string? Bo_Nho_Trong { get; set; }

        [Required(ErrorMessage = "Dung lượng pin không được để trống.")]
        [StringLength(50, ErrorMessage = "Dung lượng pin không được vượt quá 50 ký tự.")]
        public string? Pin { get; set; }

        [Required(ErrorMessage = "Hệ điều hành không được để trống.")]
        [StringLength(255, ErrorMessage = "Hệ điều hành không được vượt quá 255 ký tự.")]
        public string? HeDieuHanh { get; set; }

        [Required(ErrorMessage = "Tần số quét màn hình không được để trống.")]
        [StringLength(50, ErrorMessage = "Tần số quét màn hình không được vượt quá 50 ký tự.")]
        public string? TanSoQuet { get; set; }

        public virtual DanhMuc? DanhMuc{ get; set; }
        public virtual NhaCungCap? NhaCungCap{ get; set; }

        public ICollection<CHI_TIET_DON_HANG>? ChiTietDonHangs { get; set; }
        public ICollection<CHI_TIET_BINH_LUAN>? ChiTietBinhLuans { get; set; }

    }
}
