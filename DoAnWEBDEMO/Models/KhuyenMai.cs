<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class KhuyenMai
    {
        [Key]
        public int Id { get; set; }
        public int SanPhamKhuyenMaiId { get; set; }
        public int MucGiamGia { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public SanPham SanPham { get; set; }
    }
}
=======
﻿using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class KhuyenMai
    {
        [Key]
        public int Id { get; set; }
        public int SanPhamKhuyenMaiId { get; set; }
        public int MucGiamGia { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }

        public SanPham SanPham { get; set; }
    }
}
>>>>>>> manh
