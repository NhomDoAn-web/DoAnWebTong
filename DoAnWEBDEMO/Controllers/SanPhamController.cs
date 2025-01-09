using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

        public IActionResult Details(int maSP)
        {
            var userId = GetLoggedInKhachHangId();

            var sanPham = _context.SanPham
                .Include(sp => sp.DanhMuc)
                .Include(sp => sp.NhaCungCap)
                .Include(sp => sp.ChiTietBinhLuans)
                    .ThenInclude(cl => cl.KhachHang) // Bao gồm thông tin khách hàng (giả sử có bảng KhachHang)
                .Include(sp => sp.MauSacs) // Bao gồm danh sách màu sắc của sản phẩm
                .Include(p => p.SanPhamYeuThichs)
                .FirstOrDefault(sp => sp.MaSP == maSP);

            if (sanPham == null)
            {
                return NotFound();
            }

            // Kiểm tra trạng thái đơn hàng
            bool donHang = _context.ChiTietDonHang
                .Include(ct => ct.DonHang)
                .Any(ct => ct.MA_SP == maSP && ct.DonHang.MaKH == userId && ct.DonHang.TrangThai == 3);

            // Lấy ảnh của màu đầu tiên (nếu có) từ bảng MauSac
            var anhDauTien = sanPham.MauSacs.FirstOrDefault()?.HinhAnhSP_MauSac;

            // Lấy danh sách sản phẩm nổi bật (giả sử các sản phẩm nổi bật có TrangThai = 1)
            var sanPhamNoiBat = _context.SanPham
                                        .Where(s => s.TrangThai == 1 && s.MaSP != maSP)
                                        .OrderByDescending(s => s.Gia)
                                        .Take(4) // Lấy 4 sản phẩm
                                        .ToList();

            // Kiểm tra trạng thái đăng nhập
            bool trangThaiDangNhap = userId != null;

            ViewBag.TrangThaiDangNhap = trangThaiDangNhap;

            var sanPhamYeuThich = _context.SanPhamYeuThich
                                  .Any(x => x.KhachHangId == userId && x.SanPhamId == maSP);

            // Tính trung bình số sao
            var trungBinhSoSao = sanPham.ChiTietBinhLuans?.Average(bl => bl.SO_SAO) ?? 0;

            // Lấy số lượt yêu thích
            var soLuotYeuThich = _context.SanPhamYeuThich.Count(sp => sp.SanPhamId == maSP);

            // Gửi sản phẩm và trung bình sao vào View
            ViewBag.TrungBinhSoSao = trungBinhSoSao;
            ViewBag.SanPhamNoiBat = sanPhamNoiBat;
            ViewBag.AnhDauTien = anhDauTien;
            ViewBag.SoLuotYeuThich = soLuotYeuThich;
            ViewBag.IsFavorite = sanPhamYeuThich;
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

            //if (userId == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

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

    }
}
