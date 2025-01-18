using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using X.PagedList.Extensions;
using ClosedXML.Excel;
using System.IO;

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

        public bool checkDangNhap()
        {
            var admin = HttpContext.Session.GetString("admin");

            if (admin != null)
            {
                return true;
            }
            return false;
        }
        public IActionResult DanhSachDanhMuc(string page, string TenDanhMucTimKiem, int pageSize = 6)
        {
            if (!checkDangNhap())
            {
                return Redirect("https://localhost:7075/Admin/Admin/AdminDangNhap");
            }
            var danhMuc = _context.DanhMuc.AsQueryable();
            // Nếu có từ khóa tìm kiếm, áp dụng mã hóa và tìm kiếm
            if (!string.IsNullOrWhiteSpace(TenDanhMucTimKiem))
            {
                string searchKeyword = MaHoaTenTimKiem(TenDanhMucTimKiem).ToLower(); // Mã hóa và chuyển về chữ thường
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
            //hết phần kiểm tra số trang


            var pageList = danhMuc.ToPagedList(pageNumber, pageSize);

            ViewBag.TenTimKiemHienTai = TenDanhMucTimKiem;

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
        [HttpPost]
        public IActionResult ThemDanhMuc(string TenDanhMuc, int TrangThai)
        {
                string tenDanhMucCanThem = MaHoaTenTimKiem(TenDanhMuc).ToLower();

                var danhMucList = _context.DanhMuc.ToList();
                bool danhMucTonTai = danhMucList.Any(dm => MaHoaTenTimKiem(dm.TenDM).ToLower() == tenDanhMucCanThem);

                if (danhMucTonTai)
                {
                    TempData["ThongBaoLoi"] = "Tên danh mục đã tồn tại!";
                    return RedirectToAction("DanhSachDanhMuc");
                }
                // Tạo đối tượng danh mục mới
                var danhMuc = new DanhMuc
                {
                    TenDM = TenDanhMuc,
                    Trang_Thai = TrangThai
                };
                // Lưu vào cơ sở dữ liệu
                _context.DanhMuc.Add(danhMuc);
                _context.SaveChanges();

                // Thông báo thành công
                TempData["ThongBaoThanhCong"] = "Thêm danh mục thành công!";
                return RedirectToAction("DanhSachDanhMuc");
        }

        [HttpPost]
        public IActionResult ThayDoiTrangThai(int id)
        {
            var item = _context.DanhMuc.Include(dm => dm.SanPham)
                                       .FirstOrDefault(dm => dm.Ma_DM == id);
            if (item != null)
            {
                // Thay đổi trạng thái của danh mục
                item.Trang_Thai = item.Trang_Thai == 1 ? 0 : 1;             
                // Cập nhật trạng thái của tất cả các sản phẩm liên quan
                foreach (var sanPham in item.SanPham)
                {
                    sanPham.TrangThai = item.Trang_Thai; // Cập nhật trạng thái sản phẩm theo trạng thái danh mục
                }
                TempData["ThongBaoThanhCong"] = "Thay đổi trạng thái danh mục thành công!";
                _context.SaveChanges(); // Lưu thay đổi
            }

            return Json(new { trangThai = item?.Trang_Thai });
        }
        public IActionResult ChinhSuaDanhMuc(int id)
        {
            var danhMuc = _context.DanhMuc.FirstOrDefault(dm => dm.Ma_DM == id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            return Json(danhMuc); // Trả về dữ liệu dưới dạng JSON
        }
        [HttpPost]
        public IActionResult SuaDanhMuc(int MaDanhMuc, string TenDanhMuc, int TrangThai)
        {
            // Tìm danh mục cần sửa theo ID
            var danhMucCanSua = _context.DanhMuc.FirstOrDefault(dm => dm.Ma_DM == MaDanhMuc);

            if (danhMucCanSua == null)
            {
                TempData["ThongBaoLoi"] = "Danh mục không tồn tại!";
                return RedirectToAction("DanhSachDanhMuc");
            }
            // Kiểm tra tên danh mục đã tồn tại trong danh sách
            string tenDanhMucCanThem = MaHoaTenTimKiem(TenDanhMuc).ToLower();
            var danhMucList = _context.DanhMuc.Where(dm => dm.Ma_DM != MaDanhMuc).ToList();
            bool danhMucTonTai = danhMucList.Any(dm => MaHoaTenTimKiem(dm.TenDM).ToLower() == tenDanhMucCanThem);

            if (danhMucTonTai)
            {
                TempData["ThongBaoLoi"] = "Tên danh mục đã tồn tại, vui lòng chọn tên khác!";
                return RedirectToAction("DanhSachDanhMuc");
            }

            // Cập nhật thông tin danh mục
            danhMucCanSua.TenDM = TenDanhMuc;

            var item = _context.DanhMuc.Include(dm => dm.SanPham)
                                       .FirstOrDefault(dm => dm.Ma_DM == MaDanhMuc);
            danhMucCanSua.Trang_Thai = TrangThai;

            foreach (var sanPham in item.SanPham)
            {
                sanPham.TrangThai = TrangThai; // Cập nhật trạng thái sản phẩm theo trạng thái danh mục
            }

            _context.SaveChanges();

            TempData["ThongBaoThanhCong"] = "Danh mục đã được sửa thành công!";
            return RedirectToAction("DanhSachDanhMuc");
        }
        [HttpGet]
        public IActionResult XuatExcel()
        {
            // Lấy danh sách danh mục
            var danhMucList = _context.DanhMuc
                .Select(dm => new
                {
                    Ma_DM = dm.Ma_DM,
                    TenDM = dm.TenDM,
                    TrangThai = dm.Trang_Thai == 1 ? "Hoạt động" : "Ngừng hoạt động",
                    SoLuongSanPham = _context.SanPham.Count(sp => sp.MaDanhMuc == dm.Ma_DM) // Đếm số lượng sản phẩm
                })
                .ToList();

            // Tạo workbook
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Danh Mục");

                // Thêm tiêu đề cột
                worksheet.Cell(1, 1).Value = "Mã danh mục";
                worksheet.Cell(1, 2).Value = "Tên danh mục";
                worksheet.Cell(1, 3).Value = "Trạng thái";
                worksheet.Cell(1, 4).Value = "Số lượng sản phẩm";

                // Định dạng tiêu đề cột
                var headerRange = worksheet.Range(1, 1, 1, 4);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Chuyển đổi chữ cái đầu tiên của tiêu đề thành hoa
                for (int i = 1; i <= 4; i++)
                {
                    var headerCell = worksheet.Cell(1, i);
                    headerCell.Value = headerCell.Value.ToString().ToUpper(); // Chuyển thành chữ hoa
                }

                // Điền dữ liệu
                for (int i = 0; i < danhMucList.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = danhMucList[i].Ma_DM;
                    worksheet.Cell(i + 2, 2).Value = danhMucList[i].TenDM;
                    worksheet.Cell(i + 2, 3).Value = danhMucList[i].TrangThai;
                    worksheet.Cell(i + 2, 4).Value = danhMucList[i].SoLuongSanPham;
                }

                // Định dạng nội dung
                var dataRange = worksheet.Range(2, 1, danhMucList.Count + 1, 4);
                dataRange.Style.Font.Bold = false; // Không in đậm phần nội dung
                dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                // Định dạng bảng
                var range = worksheet.Range(1, 1, danhMucList.Count + 1, 4);
                range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // Ghi file ra stream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhMucCacSanPham-TechLand.xlsx");
                }
            }
        }


    }
}