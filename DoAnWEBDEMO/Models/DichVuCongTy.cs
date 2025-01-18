using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class DichVuCongTy
    {
        [Key]
        public int ID { get; set; }
        [Required]  
        public string? TieuDe { get; set; }
        [Required]
        public string? MoTa { get; set; }
        [Required]
        public string? HinhAnh { get; set; }
        [Required]
        public int? TrangThai { get; set; }
        
    }
}
