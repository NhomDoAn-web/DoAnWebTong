using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DoAnWEBDEMO.Controllers
{
    [Area("Admin")]
    public class ChiTietBinhLuanController : Controller
    {
        private readonly ApplicationDb _context;

        public ChiTietBinhLuanController(ApplicationDb context)
        {
            _context = context;
        }
        public bool checkDangNhap()
        {
            var admin = HttpContext.Session.GetString("admin");

            if (admin != null)
            {
                return true;
            }
            return false;
        }
        // Phương thức HttpGet để hiển thị danh sách bình luận
        [HttpGet]
        public IActionResult Index(int? page, string searchTerm, bool? isHidden)
        {
            if (!checkDangNhap())
            {
                return Redirect("https://localhost:7075/Admin/Admin/AdminDangNhap");
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1); 

            // Lấy danh sách các bình luận từ cơ sở dữ liệu và kết hợp với bảng KhachHang và SanPham
            var binhLuansQuery = _context.ChiTietBinhLuan
                                         .Include(b => b.KhachHang)
                                         .Include(b => b.SanPham)
                                         .OrderByDescending(b => b.NGAY)
                                         .AsQueryable();

            // Tìm kiếm theo tên khách hàng hoặc tên sản phẩm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                binhLuansQuery = binhLuansQuery.Where(b => b.KhachHang.TenKH.Contains(searchTerm) || b.SanPham.TEN_SP.Contains(searchTerm));
            }

            // Tìm kiếm theo trạng thái (hiển thị/ẩn)
            if (isHidden.HasValue)
            {
                binhLuansQuery = binhLuansQuery.Where(b => b.IsHidden == isHidden.Value);
            }

            // Lấy dữ liệu sau khi lọc
            var binhLuans = binhLuansQuery.ToList();

            // Phân trang dữ liệu
            var pagedBinhLuans = binhLuans.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tính toán tổng số trang
            int totalItems = binhLuans.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Truyền dữ liệu phân trang và thông tin tìm kiếm vào View
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.SearchTerm = searchTerm;  
            ViewBag.IsHidden = isHidden;      
            return View(pagedBinhLuans);
        }



        // Phương thức ẩn/hủy ẩn bình luận
        public async Task<IActionResult> ToggleCommentVisibility(int id)
        {
            // Tìm bình luận theo Id
            var binhLuan = await _context.ChiTietBinhLuan
                .FirstOrDefaultAsync(bl => bl.Id_BinhLuan == id);

            if (binhLuan == null)
            {
                return NotFound();
            }

            // Đảo ngược trạng thái của IsHidden
            binhLuan.IsHidden = !binhLuan.IsHidden;
           
            // Lưu thay đổi vào cơ sở dữ liệu
            _context.Update(binhLuan);
            await _context.SaveChangesAsync();
            // Thêm thông báo vào TempData
            if (binhLuan.IsHidden)
            {
                TempData["SuccessMessage"] = "Bình luận đã được ẩn.";
            }
            else
            {
                TempData["SuccessMessage"] = "Bình luận đã được hiển thị.";
            }
            // Chuyển hướng về trang hiển thị danh sách bình luận
            return RedirectToAction(nameof(Index));
        }
    }
}
