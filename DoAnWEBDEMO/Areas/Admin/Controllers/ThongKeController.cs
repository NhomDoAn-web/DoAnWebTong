using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DoAnWEBDEMO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {

        private readonly ApplicationDb _context;

        public ThongKeController(ApplicationDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ThongKeLuotMua(int page = 1, int? categoryId = null, int? month = null)
        {
            int pageSize = 10;
            var categories = _context.DanhMuc.ToList();
            var thongKeQuery = _context.ChiTietDonHang
                .Where(ct => ct.DonHang.TrangThai == 4)
                .AsQueryable();
            if (categoryId.HasValue)
            {
                thongKeQuery = thongKeQuery.Where(ct => ct.SanPham.MaDanhMuc == categoryId);
            }
            if (month.HasValue)
            {
                thongKeQuery = thongKeQuery.Where(ct => ct.DonHang.NgayDatHang.HasValue && ct.DonHang.NgayDatHang.Value.Month == month);
            }
            int totalProducts = thongKeQuery
                .GroupBy(ct => ct.SanPham.TEN_SP)
                .Count();
            var thongKe = thongKeQuery
                .GroupBy(ct => ct.SanPham.TEN_SP)
                .Select(g => new ThongKeSanPhamViewModel
                {
                    TenSanPham = g.Key,
                    LuotMua = g.Select(ct => ct.MA_DH).Distinct().Count() // Đếm số đơn hàng khác nhau
                })
                .OrderByDescending(tk => tk.LuotMua)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.Categories = categories;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SelectedCategory = categoryId;
            ViewBag.SelectedMonth = month;
            return View(thongKe);
        }


    }
}
