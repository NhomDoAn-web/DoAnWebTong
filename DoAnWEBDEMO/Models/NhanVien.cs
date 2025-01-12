
﻿ using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class NhanVien
    {
        [Key]
        public int MA_NV { get; set; }  

        [Required]
        [StringLength(100)]
        public string? TENNV { get; set; } 

        [StringLength(15)]
        public string? SDT { get; set; }  

        [StringLength(100)]
        [EmailAddress]
        public string? EMAIL { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(255, ErrorMessage = "Tên đăng nhập không được vượt quá 255 ký tự")]
        public string? TENDANGNHAP { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất 8 ký tự, bao gồm chữ thường, chữ hoa, ký số, và ký tự đặc biệt.")]
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string? MATKHAU { get; set; }

        public int TRANGTHAI { get; set; }

        public virtual ICollection<LienHe>? LienHe { get; set; }
        public virtual ICollection<DonHang>? DonHang{ get; set; }

    }
}

