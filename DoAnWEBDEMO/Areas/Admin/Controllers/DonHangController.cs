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

        public IActionResult Index()
        {
            var donHangs = _context.DonHang.Include(d => d.KhachHang).ToList();
            return View(donHangs);
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

    }

}
