using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class Header
    {
        [Key]
        public int ID { get; set; }  
        public string? TieuDe { get; set; }
        public string? DuongLienKet { get; set; }
        public string? Icon { get; set; }
        public int? ViTriHienThi { get; set; }

    }
}
