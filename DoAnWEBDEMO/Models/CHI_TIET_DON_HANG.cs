using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnWEBDEMO.Models
{
    public class CHI_TIET_DON_HANG
    {
        [Key]
        public int MA_DH { get; set; } // Khóa ngoại tới bảng DON_HANG

        [Key]
        public int MA_SP { get; set; } // Khóa ngoại tới bảng San_Pham (sẽ thêm sau)

        [Required(ErrorMessage = "Số lượng không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int SOLUONG { get; set; }

        [Required(ErrorMessage = "Tổng tiền từng sản phẩm không được để trống.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Tổng tiền từng sản phẩm phải là số dương.")]
        public decimal TONGTIENTUNGSANPHAM { get; set; }

        public DON_HANG? DonHang { get; set; }

        public San_Pham? SanPham { get; set; }
    }
}
