using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnWEBDEMO.Models
{
    public class ChiTietBinhLuan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_BinhLuan { get; set; }

        [Required]
        public int MA_SP { get; set; }

        [Required]
        public int MA_KH { get; set; }

        [Range(1, 5)]
        public int SO_SAO { get; set; }

        [StringLength(255)]
        public string? NOI_DUNG { get; set; }

        [Required]
        public DateTime NGAY { get; set; }

        public KhachHang? KhachHang { get; set; }
        public SanPham? SanPham { get; set; }

        // Thuộc tính trạng thái hiển thị
        [Required]
        public bool IsHidden { get; set; } = false; // Mặc định bình luận sẽ hiển thị
    }

}
