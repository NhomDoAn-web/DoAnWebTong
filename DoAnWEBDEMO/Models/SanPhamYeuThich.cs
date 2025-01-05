<<<<<<< HEAD
﻿namespace DoAnWEBDEMO.Models
{
    public class SanPhamYeuThich
    {
        public int Id { get; set; }

        public int KhachHangId { get; set; }
        public int SanPhamId { get; set; }

        public KhachHang? KhachHang { get; set; }
        public SanPham? SanPham { get; set; }
    }
}
=======
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
>>>>>>> manh
