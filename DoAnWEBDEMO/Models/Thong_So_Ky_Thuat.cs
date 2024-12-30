using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class Thong_So_Ky_Thuat
    {
        [Key] 
        public int ID_THONG_SO { get; set; }

        [Required(ErrorMessage = "Mã sản phẩm không được để trống.")]
        public int MA_SP { get; set; }

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
    }
}
