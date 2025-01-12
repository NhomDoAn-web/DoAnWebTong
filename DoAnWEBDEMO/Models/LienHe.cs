using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class LienHe
    {
        [Key]
        public int MA_LH { get; set; }

        public int? MA_NVXL { get; set; }

        [Required]
        [StringLength(255)]
        public string HO_TEN { get; set; } 

        [EmailAddress]
        [StringLength(255)]
        public string EMAIL { get; set; } 

        [StringLength(11)]
        public string SDT { get; set; }  

        [StringLength(255)]
        public string NOI_DUNG { get; set; }  

        [DataType(DataType.DateTime)]
        public DateTime THOI_GIAN_GUI { get; set; } 

        public int TRANG_THAI { get; set; }

        public virtual NhanVien? NhanVien{ get; set; }
    }
}
