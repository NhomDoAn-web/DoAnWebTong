using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;


namespace DoAnWEBDEMO.Controllers
{
    public class DonHangController : Controller
    {
        private readonly ApplicationDb _context;

        public DonHangController(ApplicationDb context)
        {
            _context = context;
        }
        public int? GetLoggedInKhachHangId()
        {
            var khachHangJson = HttpContext.Session.GetString("user");

            if (string.IsNullOrEmpty(khachHangJson))
            {
                return null;
            }

            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(khachHangJson);

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

        // Hiển thị danh sách đơn hàng
        public async Task<IActionResult> Index()
        {
            // Lấy ID của khách hàng đã đăng nhập
            var khachHangId = GetLoggedInKhachHangId();

            // Nếu khách hàng chưa đăng nhập, chuyển hướng đến trang đăng nhập hoặc hiển thị thông báo
            if (!khachHangId.HasValue)
            {
                return RedirectToAction("Login", "Account"); // Hoặc bạn có thể hiển thị thông báo
            }

            // Lọc đơn hàng theo khách hàng đã đăng nhập
            var donHangs = await _context.DonHang
                .Include(d => d.KhachHang)
                .Include(d => d.NhanVien)
                .Include(d => d.ChiTietDonHangs) // Bao gồm ChiTietDonHangs
                    .ThenInclude(c => c.SanPham)  // Bao gồm sản phẩm liên quan đến chi tiết đơn hàng
                .Where(d => d.MaKH == khachHangId.Value)
                .ToListAsync();

            return View(donHangs);
        }


        //hiển thị chi tiết đơn hàng
        public async Task<IActionResult> DetailsDonHang(int id)
        {
            // Lấy ID của khách hàng đã đăng nhập
            var khachHangId = GetLoggedInKhachHangId();

            // Nếu khách hàng chưa đăng nhập, chuyển hướng đến trang đăng nhập hoặc hiển thị thông báo
            if (!khachHangId.HasValue)
            {
                return RedirectToAction("Login", "Account"); // Hoặc bạn có thể hiển thị thông báo
            }

            // Tìm kiếm đơn hàng theo id và khách hàng hiện tại
            var donHang = await _context.DonHang
                .Include(d => d.KhachHang)
                .Include(d => d.NhanVien)
                .Include (d => d.ChiTietDonHangs)
                .FirstOrDefaultAsync(d => d.MaDH == id && d.MaKH == khachHangId.Value);

            // Kiểm tra xem đơn hàng có tồn tại không
            if (donHang == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy đơn hàng
            }

            return View(donHang); // Trả về view hiển thị chi tiết đơn hàng
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
            return RedirectToAction(nameof(DetailsDonHang), new { id = donHang.MaDH });
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
            return RedirectToAction(nameof(DetailsDonHang), new { id = donHang.MaDH });
        }


        
    }
}
