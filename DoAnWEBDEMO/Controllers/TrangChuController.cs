using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAnWEBDEMO.ApplicationDB;
using X.PagedList;
using X.PagedList.Extensions;
using DoAnWEBDEMO.Models;
namespace DoAnWEBDEMO.Controllers
{
    public class TrangChuController : Controller
    {
        private readonly ApplicationDb _context;

        public TrangChuController(ApplicationDb context)
        {
            _context = context ;
        }
        public IActionResult TimKiemSanPham(string? TenTimKiem, int page = 1, int pageSize = 2)
        {

            var query = _context.SanPham.AsQueryable();

            if (!string.IsNullOrEmpty(TenTimKiem))
            {
                query = query.Where(sp => sp.TEN_SP.Contains(TenTimKiem));
            }
            var pagedList = query.OrderBy(sp => sp.TEN_SP).ToPagedList(page, pageSize);
            ViewBag.CurrentSearchTerm = TenTimKiem;
            return View(pagedList);
        }
        [HttpPost]
        public IActionResult ThemSanPhamYeuThich(int productId)
        {
            var userId = GetLoggedInKhachHangId(); // Hàm lấy ID của khách hàng đăng nhập
            if (userId == null)
            {
                return Json(new { success = false, message = "Bạn cần đăng nhập để thêm vào danh sách yêu thích." });
            }

            var existingItem = _context.SanPhamYeuThich.FirstOrDefault(x => x.KhachHangId == userId && x.SanPhamId == productId);

            if (existingItem != null)
            {
                return Json(new { success = false, message = "Sản phẩm đã có trong danh sách yêu thích của bạn." });
            }

            var newWishlistItem = new SanPhamYeuThich
            {
                KhachHangId = userId.Value,
                SanPhamId = productId
            };

            _context.SanPhamYeuThich.Add(newWishlistItem);
            _context.SaveChanges();

            return Json(new { success = true, message = "Sản phẩm đã được thêm vào danh sách yêu thích!" });
        }

        public int? GetLoggedInKhachHangId()
        {
            var userName = HttpContext.Session.GetString("user");

            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }
            var customer = _context.KhachHang.FirstOrDefault(kh => kh.TenKH == userName);

            if (customer == null)
            {
                return null; 
            }

            return customer.MaKH; 
        }


    }
}
