using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class DanhMuc
    {
        [Key]
        public int Ma_DM { get; set; } 

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(255, ErrorMessage = "Tên danh mục không được vượt quá 255 ký tự")]
        public string TenDM { get; set; } 

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public int Trang_Thai { get; set; }

        public virtual ICollection<SanPham>? SanPham{ get; set; }
    }
}
