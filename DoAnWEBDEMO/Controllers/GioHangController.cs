using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics;
using System.Text.Json;

namespace DoAnWEBDEMO.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ApplicationDb _db;

        public GioHangController(ApplicationDb db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var KH_JSON = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(KH_JSON))
            {
                var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
                Debug.WriteLine(thongTinkhachHang);     
                var result = from p in _db.ChiTietGioHang
                             join c in _db.SanPham on p.MaSP equals c.MaSP
                             where p.MaKH==Convert.ToInt32(thongTinkhachHang.MaKH)
                             select new
                         {
                             MaSP = c.MaSP,
                             ProductName = c.TEN_SP,
                             Price = c.Gia,
                             Image = c.HinhAnhSP,
                             Quantity = p.Soluong,
                             TotalPrice = c.Gia * p.Soluong
                             };
                if(result.Count() >0 ) {
                    ViewBag.Total = result.Sum(e => e.TotalPrice);
                    ViewBag.ChiTietGioHang = result;
                }
                else
                {
                    ViewBag.ChiTietGioHang = null;
                }
            }
            return View();
        }
        public IActionResult ThanhToan()
        {
            var KH_JSON = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(KH_JSON))
            {
                var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
                Debug.WriteLine(thongTinkhachHang);
                var result = from p in _db.ChiTietGioHang
                             join c in _db.SanPham on p.MaSP equals c.MaSP
                             where p.MaKH == Convert.ToInt32(thongTinkhachHang.MaKH)
                             select new
                             {
                                 MaSP = c.MaSP,
                                 ProductName = c.TEN_SP,
                                 Price = c.Gia,
                                 Image = c.HinhAnhSP,
                                 Quantity = p.Soluong,
                                 TotalPrice = c.Gia * p.Soluong
                             };
                ViewBag.Total = result.Sum(e => e.TotalPrice);
                ViewBag.GioHang = result;
            }
            return View();
        }
        [HttpPost]
        public IActionResult TangSoLuong(int maSP)
        {
            var sanpham = _db.ChiTietGioHang.FirstOrDefault(sp=>sp.MaSP==maSP);
            if(sanpham!=null)
            {
                sanpham.Soluong += 1;
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult GiamSoLuong(int maSP)
        {
            var sanpham = _db.ChiTietGioHang.FirstOrDefault(sp => sp.MaSP == maSP);
            if (sanpham != null)
            {
                sanpham.Soluong -= 1;
                if (sanpham.Soluong <= 0)
                {
                    _db.ChiTietGioHang.Remove(sanpham); 
                }
                else
                {
                    _db.ChiTietGioHang.Update(sanpham);
                }
                _db.SaveChanges();
                return Json(new { success = true });
                
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult XoaKhoiGioHang(int maSP)
        {             
            var sanpham = _db.ChiTietGioHang.FirstOrDefault(sp => sp.MaSP == maSP);
                if (sanpham != null)
                {
                    Debug.WriteLine("Xoa san pham khoi gio hang" + sanpham.MaKH);
                    _db.Remove(sanpham);
                    _db.SaveChanges();
                    return Json(new { success = true });
                }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ThemVaoGioHang(int maSP)
        {
            var sanPham = _db.SanPham.FirstOrDefault(p => p.MaSP == maSP);
            if (sanPham != null)
            {
                var KH_JSON = HttpContext.Session.GetString("user");
                var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
                var sanpham = _db.ChiTietGioHang.FirstOrDefault(p => p.MaSP == maSP && p.MaKH == thongTinkhachHang.MaKH);

                if (sanpham == null)
                {
                    Debug.WriteLine("San pham da duoc them vao gio");
/*
                    sanpham = new ChiTietGioHang
                    {
                        MaSP = maSP,
                        MaKH = thongTinkhachHang.MaKH,
                        Soluong = 1
                    };
                    _db.ChiTietGioHang.Add(sanpham);*/
                }
               

               // _db.SaveChanges();

                return Json(new { success = true});
            }

            return Json(new { success = false });
        }
        [HttpPost]
        public IActionResult XoaToanBoGioHang()
        {
            var KH_JSON = HttpContext.Session.GetString("user");
            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
            if (thongTinkhachHang.MaKH!=null)
            {

                var gioHangItems = _db.ChiTietGioHang.Where(p => p.MaKH == thongTinkhachHang.MaKH);
                _db.ChiTietGioHang.RemoveRange(gioHangItems);
                _db.SaveChanges();

                return Json(new { success = true, message = "Đã xóa toàn bộ giỏ hàng." });
            }

            return Json(new { success = false, message = "Không thể xóa giỏ hàng." });
        }
    }
}
