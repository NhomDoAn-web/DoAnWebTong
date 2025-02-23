﻿using System.ComponentModel.DataAnnotations;

namespace DoAnWEBDEMO.Models
{
    public class ChiTietGioHang
    {

        [Key]
        public int MaKH {  get; set; }
        [Key]
        public int? MaSP { get; set; }
        [Key]
        public int? MaMau{ get; set; }

        public int? Soluong { get; set;}
        public KhachHang? KhachHang { get; set; }
        public SanPham? SanPham{ get; set; }
        public MauSac? MauSac { get; set; }
    }
}
