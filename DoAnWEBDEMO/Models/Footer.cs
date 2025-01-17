using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class Footer
    {
        [Key]
        public int ID { get; set; }
        public string? TieuDe { get; set; }
        public string? MoTa { get; set; }
        public string? HinhAnh { get; set; }
        public string? DuongLienKet { get; set; }
        [Required]
        public int? TrangThaiHienThi { get; set; }

    }
}
