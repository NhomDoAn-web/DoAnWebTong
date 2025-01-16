using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Text;
using X.PagedList.Extensions;

namespace DoAnWEBDEMO.Controllers
{
    public class BaiVietController : Controller
    {
        private readonly ApplicationDb _context;

        public BaiVietController(ApplicationDb context)
        {
            _context = context;
        }

        public IActionResult DanhSachBaiViet(string search,int page = 1, int PageSize = 5)
        {
            var danhSachBaiViet = _context.BaiViet.Skip(1).ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                // Mã hóa tên tìm kiếm để so sánh dễ dàng
                var searchQuery = MaHoaTenTimKiem(search.ToLower());
                danhSachBaiViet = danhSachBaiViet.AsEnumerable()
                    .Where(bv => MaHoaTenTimKiem(bv.TieuDe.ToLower()).Contains(searchQuery)).ToList();
            }
            var PageList = danhSachBaiViet.ToPagedList(page, PageSize);
            ViewBag.CurrentBlog = search;
            return View(PageList);
        }
        public IActionResult ChiTietBaiViet(int id)
        {
            var baiViet = _context.BaiViet.FirstOrDefault(bv => bv.MaBV == id);
            if (baiViet == null)
            {
                return NotFound();
            }

            return View(baiViet);
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
