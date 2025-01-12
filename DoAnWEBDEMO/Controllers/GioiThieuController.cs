using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace DoAnWEBDEMO.Controllers
{
    public class GioiThieuController : Controller
    {
        private readonly ApplicationDb _context;
        public GioiThieuController(ApplicationDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Lấy bài viết giới thiệu
            var baiVietGioiThieu = _context.BaiViet
                .FirstOrDefault(bv => bv.TieuDe.Contains("Giới thiệu về TechLand"));

            // Lấy danh sách các bài viết khác (2 bài gần nhất)
            var danhSachBaiViet = _context.BaiViet
                .Where(bv => !bv.TieuDe.Contains("Giới thiệu"))
                .OrderByDescending(bv => bv.NgayDang)
                .Take(4)
                .ToList();

            // Gửi dữ liệu ra view
            ViewBag.GioiThieu = baiVietGioiThieu;
            ViewBag.DanhSachBaiViet = danhSachBaiViet;

            return View();
        }
    }
}
