using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class DanhMuc
    {
        [Key]
        public int Ma_DM { get; set; } // Mã danh mục, khóa chính

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(255, ErrorMessage = "Tên danh mục không được vượt quá 255 ký tự")]
        public string TenDM { get; set; } // Tên danh mục

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        [StringLength(50, ErrorMessage = "Trạng thái không được vượt quá 50 ký tự")]
        public string Trang_Thai { get; set; } // Trạng thái: Hiển thị/Ẩn
    }
}
