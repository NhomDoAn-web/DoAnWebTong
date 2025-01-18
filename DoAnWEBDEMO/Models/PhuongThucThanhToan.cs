using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class PhuongThucThanhToan
    {
        [Key]
        public int MaPT { get; set; }
        [Required]
        public string TenPT { get; set; }
        [Required]
        public string HinhPT { get; set; }

    }
}
