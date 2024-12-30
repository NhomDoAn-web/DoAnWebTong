using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnWEBDEMO.Models
{
    public class TaiKhoanNhanVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ma_TKNV { get; set; } // Mã tài khoản nhân viên, khóa chính

        [ForeignKey("NhanVien")]
        public int Ma_NV { get; set; } // Mã nhân viên (khóa ngoại)

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(255, ErrorMessage = "Tên đăng nhập không được vượt quá 255 ký tự")]
        public string TenDangNhap { get; set; } // Tên đăng nhập

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(255, ErrorMessage = "Mật khẩu không được vượt quá 255 ký tự")]
        public string MatKhau { get; set; } // Mật khẩu
    }
}
