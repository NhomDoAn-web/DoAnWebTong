using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class Tai_Khoan_Khach_Hang
    {
        [Key] 
        public int MA_TK { get; set; }

        [Required] 
        public int MA_KH { get; set; }

        [StringLength(50)] 
        [Required(ErrorMessage = "Tên người dùng không được để trống.")]
        public string? TENNGUOIDUNG { get; set; }

        [StringLength(255)] 
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string? MATKHAU { get; set; }
    }
}
