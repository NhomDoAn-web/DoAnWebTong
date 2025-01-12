using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using X.PagedList.Extensions;

namespace DoAnWEBDEMO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucController : Controller
    {   
        private readonly ApplicationDb _context;

        public DanhMucController(ApplicationDb context)
        {
            _context = context;
        }

        public IActionResult DanhSachDanhMuc(string page, string TenDahMucTimKiem, int pageSize = 2)
        { 
            var danhMuc = _context.DanhMuc.AsQueryable();
            // Nếu có từ khóa tìm kiếm, áp dụng mã hóa và tìm kiếm
            if (!string.IsNullOrEmpty(TenDahMucTimKiem))
            {
                string searchKeyword = MaHoaTenTimKiem(TenDahMucTimKiem).ToLower(); // Mã hóa và chuyển về chữ thường
                danhMuc = danhMuc.AsEnumerable() 
                .Where(dm => MaHoaTenTimKiem(dm.TenDM).ToLower().Contains(searchKeyword)) 
                .AsQueryable();
            }
            //Kiểm tra số trang
            // Kiểm tra nếu tham số page không hợp lệ
            int pageNumber;
            if (!int.TryParse(page, out pageNumber) || pageNumber < 1)
            {
                pageNumber = 1; // Mặc định về trang 1 nếu page không hợp lệ
            }


            var danhMucList = _context.DanhMuc.ToList();
            // Tính tổng số trang
            var totalItems = danhMucList.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Nếu pageNumber lớn hơn tổng số trang, chuyển về trang cuối
            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }
            //hết phàn kiểm tra số trang


            var pageList = danhMuc.ToPagedList(pageNumber, pageSize);

            ViewBag.TenTimKiemHienTai = TenDahMucTimKiem;

            return View(pageList);
        }
        public static string MaHoaTenTimKiem(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

    }
}
