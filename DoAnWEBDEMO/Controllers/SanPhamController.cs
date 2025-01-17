using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DoAnWEBDEMO.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ApplicationDb _context;

        public SanPhamController(ApplicationDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.SanPham.Include(p => p.DanhMuc).Include(p => p.NhaCungCap).ToList();
            return View(products);
        }

        public int? GetLoggedInKhachHangId()
        {
            var khachHangJson = HttpContext.Session.GetString("user");

            if (string.IsNullOrEmpty(khachHangJson))
            {
                return null;
            }

            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(khachHangJson); //*

            if (thongTinkhachHang != null)
            {
                var customer = _context.KhachHang.FirstOrDefault(kh => kh.MaKH == thongTinkhachHang.MaKH);

                if (customer != null)
                {
                    return customer.MaKH;
                }
            }


            return null;
        }

        [HttpGet("san-pham/{slug}")]
        public IActionResult Details(string slug)
        {
            var userId = GetLoggedInKhachHangId();

            // Truy vấn sản phẩm theo slug
            var sanPham = _context.SanPham
                .Include(sp => sp.DanhMuc)
                .Include(sp => sp.NhaCungCap)
                .Include(sp => sp.ChiTietBinhLuans)
                    .ThenInclude(cl => cl.KhachHang)
                .Include(sp => sp.MauSacs)
                .Include(p => p.SanPhamYeuThichs)
                .FirstOrDefault(sp => sp.Slug == slug); // Tìm theo slug thay vì maSP

            if (sanPham == null)
            {
                return NotFound();
            }

            // Kiểm tra trạng thái đơn hàng
            bool donHang = _context.ChiTietDonHang
                .Include(ct => ct.DonHang)
                .Any(ct => ct.MA_SP == sanPham.MaSP && ct.DonHang.MaKH == userId && ct.DonHang.TrangThai == 4);

            var anhDauTien = sanPham.MauSacs.FirstOrDefault()?.HinhAnhSP_MauSac;

            var sanPhamLienQuan = _context.SanPham
                                          .Where(s => s.TrangThai == 1 && s.MaSP != sanPham.MaSP && s.MaDanhMuc == sanPham.MaDanhMuc)
                                          .OrderByDescending(s => s.Gia)
                                          .Take(4)
                                          .ToList();
            ViewBag.SanPhamLienQuan = sanPhamLienQuan;

            bool trangThaiDangNhap = userId != null;
            ViewBag.TrangThaiDangNhap = trangThaiDangNhap;

            var sanPhamYeuThich = _context.SanPhamYeuThich
                                  .Any(x => x.KhachHangId == userId && x.SanPhamId == sanPham.MaSP);
            ViewBag.IsFavorite = sanPhamYeuThich;

            var trungBinhSoSao = sanPham.ChiTietBinhLuans?.Average(bl => bl.SO_SAO) ?? 0;
            var soLuotYeuThich = _context.SanPhamYeuThich.Count(sp => sp.SanPhamId == sanPham.MaSP);

            ViewBag.TrungBinhSoSao = trungBinhSoSao;
            ViewBag.AnhDauTien = anhDauTien;
            ViewBag.SoLuotYeuThich = soLuotYeuThich;

            ViewBag.KiemTraTrangThaiDH = donHang;

            // Tăng lượt xem sản phẩm
            sanPham.LuotXem += 1;

            _context.Update(sanPham);
            _context.SaveChanges();

            return View(sanPham);
        }


        [HttpPost]
        public IActionResult BinhLuan(int maSP, string NoiDung, int Rating)
        {
            var userId = GetLoggedInKhachHangId();
            ViewBag.UserID = userId;
            // Thêm bình luận mới
            var binhLuan = new ChiTietBinhLuan
            {
                MA_KH = (int)userId,
                MA_SP = maSP,
                SO_SAO = Rating,
                NOI_DUNG = NoiDung,
                NGAY = DateTime.Now
            };

            _context.ChiTietBinhLuan.Add(binhLuan);
            _context.SaveChanges();

            TempData["Success"] = "Bình luận của bạn đã được gửi.";
            return RedirectToAction("Details", new { MaSP = maSP });
        }

        [HttpGet]
        public JsonResult GetProductStock(int idMauSac)
        {
            // Lấy số lượng tồn từ cơ sở dữ liệu dựa trên ID_MauSac
            var stock = _context.MauSac
                                .Where(ms => ms.ID_MauSac == idMauSac)
                                .Select(ms => ms.SoLuongTon_MS)
                                .FirstOrDefault();

            return Json(new { success = true, stock = stock });
        }

        public string CreateSlug(string title)
        {
            if (string.IsNullOrEmpty(title)) return string.Empty;

            // Chuyển thành chữ thường
            title = title.ToLower();

            // Thay thế các ký tự đặc biệt và khoảng trắng bằng dấu gạch ngang
            title = Regex.Replace(title, @"[^a-z0-9\s-]", "");
            title = Regex.Replace(title, @"\s+", " ").Trim();
            title = title.Replace(" ", "-");

            return title;
        }

    }
}
