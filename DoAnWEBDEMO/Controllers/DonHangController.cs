using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnWEBDEMO.Controllers
{
    public class DonHangController : Controller
    {
        private readonly ApplicationDb _context;

        public DonHangController(ApplicationDb context)
        {
            _context = context;
        }


        // Hiển thị danh sách đơn hàng
        public async Task<IActionResult> Index()
        {
            var donHangs = await _context.DonHang
                .Include(d => d.KhachHang)
                .Include(d => d.NhanVien)
                .ToListAsync();

            return View(donHangs);
        }
        // Hủy đơn hàng
        public async Task<IActionResult> CancelOrder(int id)
        {
            var donHang = await _context.DonHang
                .FirstOrDefaultAsync(d => d.MaDH == id);

            if (donHang == null)
            {
                return NotFound(); // Nếu không tìm thấy đơn hàng
            }

            // Cập nhật trạng thái đơn hàng thành "Đã hủy" (Trạng thái 3)
            donHang.TrangThai = 3;

            await _context.SaveChangesAsync();

            // Trả về thông tin trạng thái của đơn hàng (có thể hiển thị thêm thông báo hoặc icon "Mua lại")
            return RedirectToAction(nameof(Index), new { id = donHang.MaDH });
        }
        //
        public async Task<IActionResult> RepurchaseOrder(int id)
        {
            var donHang = await _context.DonHang
                .FirstOrDefaultAsync(d => d.MaDH == id);

            if (donHang == null)
            {
                return NotFound(); // Nếu không tìm thấy đơn hàng
            }

            // Cập nhật trạng thái đơn hàng thành "Đang xử lý" (Trạng thái 1)
            donHang.TrangThai = 1;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Chuyển hướng về trang danh sách đơn hàng hoặc trang chi tiết đơn hàng
            return RedirectToAction(nameof(Index));
        }



    }
}
