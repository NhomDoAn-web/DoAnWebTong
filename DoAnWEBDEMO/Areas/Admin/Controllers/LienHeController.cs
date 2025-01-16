using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAnWEBDEMO.ApplicationDB;
using X.PagedList.Extensions;


namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LienHeController : Controller
    {
        private readonly ApplicationDb _db;
        public LienHeController(ApplicationDb db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var dslh = _db.LienHe
                    .Include(lh => lh.NhanVien)
                    .Where(lh=>lh.TRANG_THAI!=3)
                    .ToList();
            ViewBag.PagedList = dslh;
            return View();
        }
        // Action Xóa Liên Hệ
        [HttpPost]
        public IActionResult XoaLienHe(int MaLH)
        {
            var item = _db.LienHe.FirstOrDefault(x => x.MA_LH == MaLH);

            if (item == null)
            {
                return Json(new { success = false, message = "Không tìm thấy mục" });
            }

            // Xóa mục khỏi DbContext
            _db.LienHe.Remove(item);

            // Lưu thay đổi vào cơ sở dữ liệu
            _db.SaveChanges();

            return Json(new { success = true, message = "Đã xóa thành công!" });
        }

        // Action Cập nhật Trạng Thái
        [HttpPost]
        public IActionResult CapNhatTrangThai(int MaLH)
        {
            var item = _db.LienHe.FirstOrDefault(x => x.MA_LH == MaLH);
            if (item == null)
            {
                return Json(new { success = false, message = "Không tìm thấy mục" });
            }

            item.MA_NVXL = 1; 
            item.TRANG_THAI = item.TRANG_THAI == 0 ? 1 : 0; 
            _db.SaveChanges();

            return Json(new { success = true, newStatus = item.TRANG_THAI, message = "Cập nhật trạng thái thành công!" });
        }

    }
}
