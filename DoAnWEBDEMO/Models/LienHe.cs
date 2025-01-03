using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class LienHe
    {
        [Key]
        public int MA_LH { get; set; }  

        public int? MA_NVXL { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(255, ErrorMessage = "Họ tên không được vượt quá 255 ký tự")]
        public string HO_TEN { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [StringLength(255, ErrorMessage = "Email không được vượt quá 255 ký tự")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(\d{10}|\d{11})$", ErrorMessage = "Số điện thoại phải gồm 10 hoặc 11 chữ số")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        [StringLength(255)]
        public string NOI_DUNG { get; set; }  

        [DataType(DataType.DateTime)]
        public DateTime THOI_GIAN_GUI { get; set; }

        public int TRANG_THAI { get; set; } = 0;

        public virtual NhanVien? NhanVien{ get; set; }
    }
}
