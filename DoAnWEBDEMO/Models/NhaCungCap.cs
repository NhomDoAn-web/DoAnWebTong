using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class NhaCungCap
    {
        
        [Key]
        public int MaNCC { get; set; }
        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên nhà cung cấp không được vượt quá 255 ký tự.")]
        public string? TenNCC { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        [RegularExpression(@"^\+?[0-9]*$", ErrorMessage = "Số điện thoại chỉ được chứa số và dấu + (nếu có).")]
        public string? SDT { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        [StringLength(255, ErrorMessage = "Email không được vượt quá 255 ký tự.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string? Email { get; set; }

        public virtual ICollection<SanPham>? SanPham{ get; set; }

    }
}
