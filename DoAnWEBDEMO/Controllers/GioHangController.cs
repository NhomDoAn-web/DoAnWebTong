
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
                var result = from ctgh in _db.ChiTietGioHang
                             join sp in _db.SanPham on ctgh.MaSP equals sp.MaSP
                             join m in _db.MauSac on ctgh.MaMau equals m.ID_MauSac
                             where ctgh.MaKH == Convert.ToInt32(thongTinkhachHang.MaKH)
                             select new
                             {
                                 MaSP = sp.MaSP,
                                 TenSP = sp.TEN_SP,
                                 Gia = sp.Gia,
                                 HinhAnh = sp.HinhAnhSP,
                                 SoLuong = ctgh.Soluong,
                                 Mau = m.TenMauSac,
                                 TongTien = sp.Gia * ctgh.Soluong
                             };
                Debug.WriteLine("==================="+result.Count());
                if (result.Count() > 0)
                {
                    ViewBag.TongTien = result.Sum(e => e.TongTien);
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
                var result = from ctgh in _db.ChiTietGioHang
                             join sp in _db.SanPham on ctgh.MaSP equals sp.MaSP
                             join m in _db.MauSac on ctgh.MaMau equals m.ID_MauSac
                             where ctgh.MaKH == Convert.ToInt32(thongTinkhachHang.MaKH)
                             select new
                             {
                                 MaSP = sp.MaSP,
                                 TenSP = sp.TEN_SP,
                                 Gia = sp.Gia,
                                 HinhAnh = m.HinhAnhSP_MauSac,
                                 SoLuong = ctgh.Soluong,
                                 Mau = m.TenMauSac,
                                 TongTien = sp.Gia * ctgh.Soluong
                             };
                ViewBag.Total = result.Sum(e => e.TongTien);
                ViewBag.ThanhToan = result;
            }
            return View();
        }
        public int? GetLoggedInKhachHangId()
        {
            var userName = HttpContext.Session.GetString("user");
            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(userName);

            if (thongTinkhachHang.MaKH == null)
            {
                return null;
            }
            var customer = _db.KhachHang.FirstOrDefault(kh => kh.MaKH == thongTinkhachHang.MaKH);

            if (customer == null)
            {
                return null;
            }

            return customer.MaKH;
        }
        [HttpPost]
        public IActionResult TangSoLuong(int maSP)
        {
            var sanpham = _db.ChiTietGioHang.FirstOrDefault(sp => sp.MaSP == maSP);
            if (sanpham != null)
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
        public IActionResult ThemVaoGioHang(int maSP, int soluong, int mau)
        {
            var userId = GetLoggedInKhachHangId(); // Hàm lấy ID của khách hàng đăng nhập
            if (userId == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập!" });
            }

            // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng với cùng mã sản phẩm và mã màu
            var existingItem = _db.ChiTietGioHang.FirstOrDefault(x => x.MaKH == userId && x.MaSP == maSP && x.MaMau == mau);

            if (existingItem != null)
            {
                // Nếu sản phẩm đã tồn tại, thông báo rằng sản phẩm đã có trong giỏ hàng
                return Json(new { success = false, message = "Sản phẩm đã có trong giỏ hàng!" });
            }
            else
            {
                // Nếu sản phẩm chưa tồn tại với màu này, thêm sản phẩm mới
                var newWishlistItem = new ChiTietGioHang
                {
                    MaKH = userId.Value,
                    MaSP = maSP,
                    MaMau = mau,
                    Soluong = soluong
                };

                _db.ChiTietGioHang.Add(newWishlistItem);
                _db.SaveChanges();

                return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng!" });
            }
        }
        [HttpPost]
        public IActionResult XoaToanBoGioHang()
        {
            var KH_JSON = HttpContext.Session.GetString("user");
            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(KH_JSON);
            if (thongTinkhachHang.MaKH != null)
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

