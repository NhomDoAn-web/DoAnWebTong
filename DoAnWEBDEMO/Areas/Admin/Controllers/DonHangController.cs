using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnWEBDEMO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonHangController : Controller
    {
        private readonly ApplicationDb _context;

        public DonHangController(ApplicationDb context)
        {
            _context = context;
        }

        public IActionResult Index(int? trangThai, int page = 1)
        {
            int pageSize = 12;
            var donHangsQuery = _context.DonHang
                .Include(d => d.KhachHang)
                .Include(d => d.ChiTietDonHangs)
                    .ThenInclude(ct => ct.SanPham)
                    .Where(d => d.TrangThai != 6)
                .AsQueryable();

            if (trangThai.HasValue)
            {
                donHangsQuery = donHangsQuery.Where(d => d.TrangThai == trangThai);
            }
            var totalItems = _context.DonHang
                 .Where(d => d.TrangThai != 6)
                 .Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var donHangs = donHangsQuery
                .OrderBy(d => d.TrangThai) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["TrangThai"] = trangThai;
            return View(donHangs);
        }

        public IActionResult DonHangDaXoa(int page = 1, int pageSize = 10)
        {
            int totalItems = _context.DonHang.Count(dh => dh.TrangThai == 6);

            var danhSachDonHangDaXoa = _context.DonHang
                .Where(dh => dh.TrangThai == 6)
                .Include(dh => dh.KhachHang)
                .Include(dh => dh.ChiTietDonHangs)
                .ThenInclude(ct => ct.SanPham)
                .OrderByDescending(dh => dh.NgayDatHang) 
                .Skip((page - 1) * pageSize) 
                .Take(pageSize) 
                .ToList();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(danhSachDonHangDaXoa);
        }



        [HttpPost]
        [Route("Admin/DonHang/CapNhatDonHang")]
        public IActionResult CapNhatDonHang(int maDH, int trangThai)
        {
            try
            {
                var donHang = _context.DonHang.Find(maDH);
                if (donHang == null)
                {
                    return Json(new { success = false, message = "Đơn hàng không tồn tại." });
                }

                donHang.TrangThai = trangThai;
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("Admin/DonHang/XoaDonHang")]
        public JsonResult XoaDonHang(int maDH)
        {
            try
            {
                // Lấy đơn hàng từ cơ sở dữ liệu
                var donHang = _context.DonHang.FirstOrDefault(d => d.MaDH == maDH);

                if (donHang == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy đơn hàng này." });
                }

                if (donHang.TrangThai != 3) // Chỉ có thể xóa khi trạng thái là "Hủy đơn hàng"
                {
                    return Json(new { success = false, message = "Đơn hàng không có trạng thái 'Hủy đơn hàng'." });
                }

                // Cập nhật trạng thái của đơn hàng
                donHang.TrangThai = 6; // Giả sử 6 là trạng thái "Đã xóa"
                _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                return Json(new { success = true, message = "Đơn hàng đã được xóa thành công." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }


    }

}
