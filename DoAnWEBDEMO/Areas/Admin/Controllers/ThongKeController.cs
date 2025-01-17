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

        //public IActionResult ThongKeLuotMua()
        //{
        //    var thongKe = _context.ChiTietDonHang
        //        .Include(ct => ct.SanPham)
        //        .Include(ct => ct.DonHang)
        //        .Include(ct => ct.MauSac)
        //        .Where(ct => ct.DonHang.TrangThai == 4)
        //        .GroupBy(ct => new { ct.SanPham.TEN_SP, ct.MauSac.TenMauSac })
        //        .Select(g => new
        //        {
        //            TenSanPham = g.Key.TEN_SP,
        //            MauSac = g.Key.TenMauSac,
        //            TongSoLuongMua = g.Sum(ct => ct.SOLUONG),
        //            TongSoDonHang = g.Select(ct => ct.DonHang.MaDH).Distinct().Count()
        //        })
        //        .OrderBy(x => x.TenSanPham)
        //        .ThenByDescending(x => x.TongSoLuongMua)
        //        .ToList();

        //    return View(thongKe);
        //}

        public IActionResult ThongKeLuotMua(int page = 1, int pageSize = 10)
        {
            // ---------------------- Danh sách lượt mua -----------------------
            var danhSach = _context.ChiTietDonHang
                .Include(ct => ct.SanPham)
                .Include(ct => ct.DonHang)
                .Include(ct => ct.MauSac)
                .Where(ct => ct.DonHang.TrangThai == 4)
                .GroupBy(ct => new { ct.SanPham.TEN_SP, ct.MauSac.TenMauSac })
                .Select(g => new
                {
                    TenSanPham = g.Key.TEN_SP,
                    MauSac = g.Key.TenMauSac,
                    TongSoLuongMua = g.Sum(ct => ct.SOLUONG),
                    TongSoDonHang = g.Select(ct => ct.DonHang.MaDH).Distinct().Count()
                })
                .OrderBy(x => x.TenSanPham)
                .ThenByDescending(x => x.TongSoLuongMua)
                .ToList();

            // ---------------------- Phân trang -----------------------
            int totalItems = danhSach.Count;
            var pagedData = danhSach.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.PhanTrang = pagedData;

            var totalPages = (int)Math.Ceiling(danhSach.Count / (double)pageSize);
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            ViewBag.DanhSach = pagedData;
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // ---------------------- Biểu đồ -----------------------
            var thongKeForChart = _context.ChiTietDonHang
                .Include(ct => ct.SanPham)
                .Include(ct => ct.DonHang)
                .Where(ct => ct.DonHang.TrangThai == 4)
                .GroupBy(ct => ct.SanPham.TEN_SP)
                .Select(g => new
                {
                    TenSanPham = g.Key,
                    TongSoDonHang = g.Select(ct => ct.DonHang.MaDH).Distinct().Count()
                })
                .ToList();

            ViewBag.ThongKeForChart = thongKeForChart;

            return View();
        }

        //public IActionResult SanPhamBanChay(DateTime? fromDate, DateTime? toDate)
        //{
        //    fromDate ??= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Ngày đầu tháng
        //    toDate ??= DateTime.Now; // Hiện tại

        //    var thongKe = _context.ChiTietDonHang
        //        .Where(ct => ct.DonHang.NgayDatHang >= fromDate && ct.DonHang.NgayDatHang <= toDate)
        //        .GroupBy(ct => new
        //        {
        //            ct.SanPham.TEN_SP, // Nhóm theo tên sản phẩm
        //            NgayThangNam = ct.DonHang.NgayDatHang.Value.Date // Lấy ngày, bỏ phần giờ phút
        //        })
        //        .Select(g => new ThongKeSanPhamViewModel
        //        {
        //            TenSanPham = g.Key.TEN_SP,
        //            SoLuongBan = g.Sum(ct => ct.SOLUONG), // Tổng số lượng bán
        //            NgayThangNam = g.Key.NgayThangNam // Ngày tháng năm
        //        })
        //        .OrderByDescending(tk => tk.SoLuongBan)
        //        .ToList();

        //    // Truyền dữ liệu thống kê cho view
        //    ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
        //    ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

        //    return View(thongKe);
        //}

        public IActionResult SanPhamBanChay(DateTime? fromDate, DateTime? toDate)
        {
            fromDate ??= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Ngày đầu tháng
            toDate ??= DateTime.Now; // Hiện tại

            // Lấy dữ liệu thống kê lượt mua
            var thongKe = _context.ChiTietDonHang
                .Where(ct => ct.DonHang.NgayDatHang >= fromDate && ct.DonHang.NgayDatHang <= toDate)
                .GroupBy(ct => ct.SanPham.TEN_SP)  // Nhóm theo tên sản phẩm
                .Select(g => new ThongKeSanPhamViewModel
                {
                    TenSanPham = g.Key,  // Tên sản phẩm
                    LuotMua = g.Select(ct => ct.MA_DH).Distinct().Count()  // Đếm số lượt mua (đếm số lần sản phẩm xuất hiện trong các đơn hàng)
                })
                .OrderByDescending(tk => tk.LuotMua)  // Sắp xếp theo lượt mua từ cao đến thấp
                .ToList();

            // Truyền dữ liệu thống kê cho view
            ViewBag.FromDate = fromDate?.ToString("yyyy-MM-dd");
            ViewBag.ToDate = toDate?.ToString("yyyy-MM-dd");

            return View(thongKe);
        }




    }
}
