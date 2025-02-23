using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class KhachHang
    {
        [Key]
        [Required(ErrorMessage = "Mã khách hàng không được để trống.")]
        public int MaKH { get; set; }

        [Required(ErrorMessage = "Họ khách hàng không được để trống.")]
        [StringLength(50, ErrorMessage = "Họ khách hàng không được vượt quá 50 ký tự.")]
        public string HoKH { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
        [StringLength(50, ErrorMessage = "Tên khách hàng không được vượt quá 50 ký tự.")]
        public string TenKH { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống.")]
        [StringLength(10, ErrorMessage = "Giới tính không được vượt quá 10 ký tự.")]
        [RegularExpression("^(Nam|Nữ|Khác)$", ErrorMessage = "Giới tính phải là 'Nam', 'Nữ', hoặc 'Khác'.")]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(10, ErrorMessage = "Số điện thoại không được vượt quá 10 ký tự.")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        [StringLength(255, ErrorMessage = "Địa chỉ không được vượt quá 255 ký tự.")]
        public string DiaChi { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Tên người dùng không được để trống.")]
        public string? TENNGUOIDUNG { get; set; }

        [StringLength(255)]
        [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Mật khẩu phải chứa ít nhất 8 ký tự, bao gồm chữ thường, chữ hoa, ký số, và ký tự đặc biệt.")]
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string? MATKHAU { get; set; }

        public virtual ICollection<DonHang>? DonHang { get; set; }

        public ICollection<ChiTietBinhLuan>? ChiTietBinhLuans { get; set; }
        public virtual ICollection<SanPhamYeuThich>? SanPhamYeuThichs { get; set; }

        public ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

    }
}