using Microsoft.AspNetCore.Mvc;
using DoAnWEBDEMO.Models;  // Thêm namespace của các model và DbContext của bạn
using X.PagedList;
using System.Linq;
using System;
using DoAnWEBDEMO.ApplicationDB;
using X.PagedList.Extensions;
using Microsoft.EntityFrameworkCore; // Thêm dòng này
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Diagnostics;
using System.Net.Http;


namespace DoAnWEBDEMO.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLySanPhamController : Controller
    {
        private readonly ApplicationDb _context;
        private readonly IWebHostEnvironment _hostingEnvironment;//xử lý ảnh

        public bool checkDangNhap()
        {
            var admin = HttpContext.Session.GetString("admin");

            if (admin != null)
            {
                return true;
            }
            return false;
        }

        public QuanLySanPhamController(ApplicationDb context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        private readonly ILogger<QuanLySanPhamController> _logger;

        // Phương thức Index với các tham số tìm kiếm và phân trang
        public async Task<IActionResult> Index(string searchString, int? categoryId, int page = 1)
        {
            if (!checkDangNhap())
            {
                return  Redirect("https://localhost:7075/Admin/Admin/AdminDangNhap");
            }
            var pageSize = 10; 

            // Đảm bảo rằng page không nhỏ hơn 1
            if (page < 1)
            {
                page = 1;
            }

            var sanPhams = from s in _context.SanPham
                           select s;

            // Tìm kiếm theo tên sản phẩm
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(s => s.TEN_SP.Contains(searchString));
            }

            // Tìm kiếm theo danh mục
            if (categoryId.HasValue)
            {
                sanPhams = sanPhams.Where(s => s.MaDanhMuc == categoryId);
            }

            // Lấy danh sách các danh mục để hiển thị trong dropdown
            ViewBag.DanhMuc = await _context.DanhMuc.ToListAsync();  

            // Lấy tổng số sản phẩm
            var totalCount = await sanPhams.CountAsync();

            // Lấy sản phẩm theo trang
            var products = await sanPhams
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            // Tính toán số trang
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            return View(products);
        }

  

        [HttpPost]
        public IActionResult CapNhatTrangThaiSanPham(string action, List<int> SelectedProducts)
        {
            if (SelectedProducts != null && SelectedProducts.Any())
            {
                // Lấy danh sách sản phẩm dựa trên trạng thái và hành động
                var products = _context.SanPham.Where(p => SelectedProducts.Contains(p.MaSP)).ToList();

                if (action == "hide")
                {
                    foreach (var product in products.Where(p => p.TrangThai == 1)) // Chỉ ẩn sản phẩm đang bán
                    {
                        product.TrangThai = 0; // Ẩn sản phẩm
                    }

                    TempData["SuccessMessage"] = "Sản phẩm đã được ẩn thành công!";
                }
                else if (action == "show")
                {
                    foreach (var product in products.Where(p => p.TrangThai == 0)) // Chỉ mở lại sản phẩm bị ẩn
                    {
                        product.TrangThai = 1; // Mở lại sản phẩm
                    }

                    TempData["SuccessMessage"] = "Sản phẩm đã được mở lại thành công!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Hành động không hợp lệ!";
                    return RedirectToAction("Index", "QuanLySanPham", new { area = "Admin" });
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }
            else
            {
                TempData["ErrorMessage"] = "Không có sản phẩm nào được chọn!";
            }

            return RedirectToAction("Index", "QuanLySanPham", new { area = "Admin" });
        }

       
        [HttpGet]

        public IActionResult Edit(int id)
        {
            if (!checkDangNhap())
            {
                return Redirect("https://localhost:7075/Admin/Admin/AdminDangNhap");
            }
            var product = _context.SanPham.Find(id); // Lấy sản phẩm theo ID
            if (product == null)
            {
                return NotFound();
            }

            
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SanPham model, IFormFile? Image, List<int> SelectedColors)
        {
            ViewBag.Colors = _context.MauSac.ToList(); // Truyền danh sách màu sắc vào ViewBag trước
          

            var product = _context.SanPham.Include(p => p.MauSacs).FirstOrDefault(p => p.MaSP == model.MaSP);
            if (product != null)
            {
                // Cập nhật thông tin sản phẩm
                product.TEN_SP = model.TEN_SP;
                product.Gia = model.Gia;
                product.MoTa = model.MoTa;
                product.SlideShow = model.SlideShow;
                product.KichThuoc_ManHinh = model.KichThuoc_ManHinh;
                product.CameraSau = model.CameraSau;
                product.Camera_Truoc = model.Camera_Truoc;
                product.Chipset = model.Chipset;
                product.Gpu = model.Gpu;
                product.Dung_Luong_Ram = model.Dung_Luong_Ram;
                product.Bo_Nho_Trong = model.Bo_Nho_Trong;
                product.Pin = model.Pin;
                product.HeDieuHanh = model.HeDieuHanh;
                product.TanSoQuet = model.TanSoQuet;
                product.TGianBaoHanh = model.TGianBaoHanh;
                product.SoLuongTon = model.SoLuongTon;
                product.MaDanhMuc = model.MaDanhMuc; // Cập nhật mã danh mục

                // Cập nhật màu sắc
                if (SelectedColors != null)
                {
                    product.MauSacs.Clear();
                    foreach (var colorId in SelectedColors)
                    {
                        var color = _context.MauSac.FirstOrDefault(m => m.ID_MauSac == colorId);
                        if (color != null)
                        {
                            product.MauSacs.Add(color);
                        }
                    }
                }

                // Xử lý ảnh nếu có
                if (Image != null && Image.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/image", Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    product.HinhAnhSP = Image.FileName;
                }

                product.Slug = GenerateSlug(model.TEN_SP);

                _context.Update(product);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.DanhMucs = _context.DanhMuc.ToList(); // Lấy danh mục từ database
            return View(model);
        }



        //
        public string GenerateSlug(string input)
        {
            return input.ToLower()
                        .Replace(" ", "-")
                        .Replace("--", "-")
                        .Replace("đ", "d")
                        .Replace("à", "a")
                        .Replace("á", "a")
                        .Replace("ả", "a")
                        .Replace("ã", "a")
                        .Replace("ạ", "a");
            // Thêm các thay đổi khác tùy theo yêu cầu của bạn
        }
        //
        [HttpGet]
        public IActionResult UploadExcel()
        {
            if (!checkDangNhap())
            {
                return Redirect("https://localhost:7075/Admin/Admin/AdminDangNhap");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                TempData["ErrorMessage"] = "File không hợp lệ!";
                return View();
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "image", "products");
                    Directory.CreateDirectory(uploadsFolder);

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var sanPham = new SanPham
                        {
                            MaNCC = ParseInt(worksheet.Cells[row, 1].Text),
                            MaDanhMuc = ParseInt(worksheet.Cells[row, 2].Text),
                            Gia = ParseDecimal(worksheet.Cells[row, 3].Text),
                            TEN_SP = worksheet.Cells[row, 4].Text,
                            HinhAnhSP = worksheet.Cells[row, 5].Text,
                            SlideShow = worksheet.Cells[row, 6].Text,
                            MoTa = worksheet.Cells[row, 7].Text,
                            TGianBaoHanh = ParseInt(worksheet.Cells[row, 8].Text),
                            NgayRaMat = ParseDateTime(worksheet.Cells[row, 9].Text),
                            TrangThai = ParseInt(worksheet.Cells[row, 10].Text),
                            Slug = worksheet.Cells[row, 11].Text,
                            KichThuoc_ManHinh = worksheet.Cells[row, 12].Text,
                            CameraSau = worksheet.Cells[row, 13].Text,
                            Camera_Truoc = worksheet.Cells[row, 14].Text,
                            Chipset = worksheet.Cells[row, 15].Text,
                            Gpu = worksheet.Cells[row, 16].Text,
                            Dung_Luong_Ram = worksheet.Cells[row, 17].Text,
                            Bo_Nho_Trong = worksheet.Cells[row, 18].Text,
                            Pin = worksheet.Cells[row, 19].Text,
                            HeDieuHanh = worksheet.Cells[row, 20].Text,
                            TanSoQuet = worksheet.Cells[row, 21].Text,
                            SoLuongTon = ParseInt(worksheet.Cells[row, 22].Text),
                        };

                        sanPham.HinhAnhSP = await ProcessImage(sanPham.HinhAnhSP, uploadsFolder);

                        var existingProduct = _context.SanPham
                            .FirstOrDefault(sp => sp.TEN_SP == sanPham.TEN_SP && sp.Slug == sanPham.Slug);

                        if (existingProduct == null)
                        {
                            _context.SanPham.Add(sanPham);
                            await _context.SaveChangesAsync();

                            int maSP = sanPham.MaSP;
                            await ProcessMauSac(worksheet.Cells[row, 23].Text, maSP);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = $"Sản phẩm '{sanPham.TEN_SP}' đã tồn tại!";
                        }
                    }

                    TempData["SuccessMessage"] = "Tải dữ liệu từ Excel thành công!";
                }
            }

            return RedirectToAction("Index");
        }

        private int ParseInt(string value)
        {
            return int.TryParse(value, out int result) ? result : 0;
            //Nếu chuỗi không thể chuyển đổi (ví dụ: chuỗi rỗng, chữ cái, ký tự đặc biệt...), giá trị mặc định 0 sẽ được trả về.
        }

        private decimal ParseDecimal(string value)
        {
            return decimal.TryParse(value, out decimal result) ? result : 0.0m;
            //Nếu chuỗi không thể chuyển đổi, giá trị mặc định 0.0m (decimal) sẽ được trả về.
        }

        private DateTime ParseDateTime(string value)
        {
            return DateTime.TryParse(value, out DateTime result) ? result : DateTime.MinValue;
            //Nếu chuỗi không thể chuyển đổi, giá trị mặc định DateTime.MinValue (01/01/0001 00:00:00) sẽ được trả về.
        }

        private async Task<string> ProcessImage(string imageUrl, string uploadsFolder)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return "loi-404.jpg";

            if (Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageUrl)}";
                string filePath = Path.Combine(uploadsFolder, fileName);

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);
                        await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                    }

                    return fileName;
                }
                catch
                {
                    return "loi-404.jpg";
                }
            }

            return imageUrl;
        }

        private async Task ProcessMauSac(string tenMauSac, int maSP)
        {
            if (string.IsNullOrEmpty(tenMauSac)) return;

            var mauSac = _context.MauSac.FirstOrDefault(ms => ms.TenMauSac == tenMauSac);
            if (mauSac != null)
            {
                mauSac.MaSP = maSP;
                _context.MauSac.Update(mauSac);
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["ErrorMessage"] = $"Màu sắc '{tenMauSac}' không tồn tại trong hệ thống!";
            }
        }



    }
}
