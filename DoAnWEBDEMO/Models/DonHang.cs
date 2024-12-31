using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class DonHang
    {
        [Key]
        [Required(ErrorMessage = "Mã đơn hàng không được để trống.")]
        public int MaDH { get; set; }

        [Required(ErrorMessage = "Mã khách hàng không được để trống.")]
        public int MaKH { get; set; }

        [Required(ErrorMessage = "Mã nhân viên không được để trống.")]
        public int MaNVXL { get; set; }

        [Required(ErrorMessage = "Ngày đặt hàng không được để trống.")]
        [StringLength(255, ErrorMessage = "Ngày đặt hàng không được vượt quá 255 ký tự.")]
        public string NgayDatHang { get; set; }

        [Required(ErrorMessage = "Tổng tiền không được để trống.")]
        [Range(1, double.MaxValue, ErrorMessage = "Tổng tiền phải là số dương.")]
        public decimal TongTienDonHang { get; set; }

        [Required(ErrorMessage = "Trạng thái đơn hàng không được để trống.")]
        [Range(1, 4, ErrorMessage = "Trạng thái phải nằm trong khoảng từ 1 đến 4.")]
        public int TrangThai { get; set; }

        [Required(ErrorMessage = "Địa chỉ nhận hàng không được để trống.")]
        [StringLength(500, ErrorMessage = "Địa chỉ nhận hàng không được vượt quá 500 ký tự.")]
        public string DiaChiNhanHang { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự.")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string SoDienThoai { get; set; }

        public virtual NhanVien? NhanVien{ get; set; }
        public virtual KhachHang? KhachHang { get; set; }

        public ICollection<CHI_TIET_DON_HANG>? ChiTietDonHangs { get; set; }


    }
}
