using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class NhanVien
    {
        [Key]
        public int MA_NV { get; set; }  

        [Required]
        [StringLength(100)]
        public string TENNV { get; set; } 

        [StringLength(15)]
        public string SDT { get; set; }  

        [StringLength(100)]
        [EmailAddress]
        public string EMAIL { get; set; } 

        public int TRANGTHAI { get; set; }
    }
}
