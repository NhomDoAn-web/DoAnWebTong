using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class BaiViet
    {
        [Key] 
        public int MaBV { get; set; }

        [Required]
        [StringLength(100)] 
        public string? TieuDe { get; set; }
        [Required]
        public string HinhAnh { get; set; }
        [Required]
        public string? NoiDung { get; set; } 

        [Required]
        public DateTime NgayDang { get; set; }
    }
}
