
﻿using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class SanPhamYeuThich
    {
        [Key]
        public int KhachHangId { get; set; }

        [Key]
        public int SanPhamId { get; set; }

        public KhachHang? KhachHang { get; set; }
        public SanPham? SanPham { get; set; }
    }
}

