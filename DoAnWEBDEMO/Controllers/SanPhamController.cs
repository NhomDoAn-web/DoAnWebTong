using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnWEBDEMO.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ApplicationDb _context; 

        public SanPhamController (ApplicationDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products =  _context.SanPham.Include(p => p.DanhMuc).Include(p => p.NhaCungCap).ToList();
            return View(products);
        }

        public IActionResult Details(int maSP)
        {
            var sanPham = _context.SanPham
                .Include(sp => sp.DanhMuc)
                .Include(sp => sp.NhaCungCap)
                .Include(sp => sp.ChiTietBinhLuans)
                    .ThenInclude(cl => cl.KhachHang) // Bao gồm thông tin khách hàng (giả sử có bảng KhachHang)
                .Include(sp => sp.MauSacs) // Bao gồm danh sách màu sắc của sản phẩm
                .FirstOrDefault(sp => sp.MaSP == maSP);

            if (sanPham == null)
            {
                return NotFound();
            }

            // Lấy ảnh của màu đầu tiên (nếu có) từ bảng MauSac
            var anhDauTien = sanPham.MauSacs.FirstOrDefault()?.HinhAnhSP_MauSac;

            // Lấy danh sách sản phẩm nổi bật (giả sử các sản phẩm nổi bật có TrangThai = 1)
            var sanPhamNoiBat = _context.SanPham
                                        .Where(s => s.TrangThai == 1 && s.MaSP != maSP) // Tránh lặp lại sản phẩm hiện tại
                                        .OrderByDescending(s => s.Gia) // Sắp xếp theo giá giảm dần
                                        .Take(4) // Lấy 4 sản phẩm
                                        .ToList();

            // Tính trung bình số sao
            var trungBinhSoSao = sanPham.ChiTietBinhLuans?.Average(bl => bl.SO_SAO) ?? 0;

            // Gửi sản phẩm và trung bình sao vào View
            ViewBag.TrungBinhSoSao = trungBinhSoSao;

            ViewBag.SanPhamNoiBat = sanPhamNoiBat;

            ViewBag.AnhDauTien = anhDauTien;

<<<<<<< HEAD
            // Tăng lượt xem sản phẩm
            sanPham.LuotXem += 1;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.Update(sanPham);
            _context.SaveChanges();

=======
>>>>>>> MinhTu
            return View(sanPham);
        }

    }
}
