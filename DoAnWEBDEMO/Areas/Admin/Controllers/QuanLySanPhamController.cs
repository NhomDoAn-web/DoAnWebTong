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

        public QuanLySanPhamController(ApplicationDb context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        private readonly ILogger<QuanLySanPhamController> _logger;

        // Phương thức Index với các tham số tìm kiếm và phân trang
        public async Task<IActionResult> Index(string searchString, int? categoryId, int page = 1)
        {
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
            var product = _context.SanPham.Find(id); // Lấy sản phẩm theo ID
            if (product == null)
            {
                return NotFound();
            }

            
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SanPham model, IFormFile? Image)
        {
            var product = _context.SanPham.FirstOrDefault(p => p.MaSP == model.MaSP);
            if (product != null)
            {
                // Cập nhật các trường theo yêu cầu
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

                // Kiểm tra nếu người dùng có chọn ảnh mới
                if (Image != null && Image.Length > 0)
                {
                    // Xử lý tải ảnh mới lên
                    var filePath = Path.Combine("wwwroot/image", Image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    product.HinhAnhSP = Image.FileName;
                }
                else
                {
                    // Nếu không có ảnh mới, giữ lại ảnh cũ
                    product.HinhAnhSP = product.HinhAnhSP; // không thay đổi
                }

                // Cập nhật slug (giả sử bạn có hàm GenerateSlug)
                product.Slug = GenerateSlug(model.TEN_SP);

                // Lưu lại sản phẩm đã cập nhật
                _context.Update(product);
                _context.SaveChanges();

                // Thêm thông báo cập nhật thành công
                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";

                return RedirectToAction(nameof(Index)); // Sau khi lưu, chuyển hướng về trang Index
            }

            // Nếu có lỗi hoặc không thành công, quay lại trang Edit
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];  // Lấy sheet đầu tiên
                        var rowCount = worksheet.Dimension.Rows;  // Lấy số lượng dòng

                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "image");
                        Directory.CreateDirectory(uploadsFolder); // Đảm bảo thư mục tồn tại

                        for (int row = 2; row <= rowCount; row++)  // Bắt đầu từ dòng 2 (dòng 1 là tiêu đề)
                        {
                            var sanPham = new SanPham
                            {
                                MaNCC = int.TryParse(worksheet.Cells[row, 1].Text, out int maNCC) ? maNCC : 0,
                                MaDanhMuc = int.TryParse(worksheet.Cells[row, 2].Text, out int maDanhMuc) ? maDanhMuc : 0,
                                Gia = decimal.TryParse(worksheet.Cells[row, 3].Text, out decimal gia) ? gia : 0.0m,
                                TEN_SP = worksheet.Cells[row, 4].Text,
                                HinhAnhSP = worksheet.Cells[row, 5].Text,
                                SlideShow = worksheet.Cells[row, 6].Text,
                                MoTa = worksheet.Cells[row, 7].Text,
                                TGianBaoHanh = int.TryParse(worksheet.Cells[row, 8].Text, out int tgianBaoHanh) ? tgianBaoHanh : 0,
                                NgayRaMat = DateTime.TryParse(worksheet.Cells[row, 9].Text, out DateTime ngayRaMat) ? ngayRaMat : DateTime.MinValue,
                                TrangThai = int.TryParse(worksheet.Cells[row, 10].Text, out int trangThai) ? trangThai : 0,
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
                                SoLuongTon = int.TryParse(worksheet.Cells[row, 22].Text, out int soLuongTon) ? soLuongTon : 0,
                            };

                            // Xử lý ảnh
                            if (!string.IsNullOrEmpty(sanPham.HinhAnhSP))
                            {
                                string productUploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "image", "products");

                                if (Uri.IsWellFormedUriString(sanPham.HinhAnhSP, UriKind.Absolute))
                                {
                                    // Nếu là URL, tải ảnh về
                                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(sanPham.HinhAnhSP)}";
                                    string filePath = Path.Combine(productUploadsFolder, fileName);

                                    try
                                    {
                                        using (HttpClient client = new HttpClient())
                                        {
                                            byte[] imageBytes = await client.GetByteArrayAsync(sanPham.HinhAnhSP);
                                            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                                        }

                                        // Lưu tên ảnh vào cơ sở dữ liệu (không cần lưu đường dẫn gốc)
                                        sanPham.HinhAnhSP = fileName;
                                    }
                                    catch (Exception ex)
                                    {
                                        TempData["ErrorMessage"] = $"Không thể tải ảnh từ URL: {sanPham.HinhAnhSP}. Lỗi: {ex.Message}";
                                        continue; // Bỏ qua sản phẩm nếu không thể tải ảnh
                                    }
                                }
                                else
                                {
                                    // Nếu là tên file, kiểm tra và xử lý
                                    string sourcePath = Path.Combine(_hostingEnvironment.WebRootPath, "image", "products", sanPham.HinhAnhSP);
                                    if (System.IO.File.Exists(sourcePath))
                                    {
                                        // Đổi tên và di chuyển ảnh nếu cần thiết
                                        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(sanPham.HinhAnhSP)}";
                                        string destPath = Path.Combine(productUploadsFolder, fileName);

                                        try
                                        {
                                            // Copy ảnh đến thư mục mới với tên mới
                                            System.IO.File.Copy(sourcePath, destPath, overwrite: true);
                                            sanPham.HinhAnhSP = fileName; // Lưu tên ảnh
                                        }
                                        catch (Exception ex)
                                        {
                                            TempData["ErrorMessage"] = $"Không thể xử lý ảnh {sanPham.HinhAnhSP}. Lỗi: {ex.Message}";
                                            continue; // Bỏ qua sản phẩm nếu không thể di chuyển ảnh
                                        }
                                    }
                                    else
                                    {
                                        TempData["ErrorMessage"] = $"Ảnh {sanPham.HinhAnhSP} không tồn tại!";
                                        continue; // Bỏ qua sản phẩm nếu ảnh không tồn tại
                                    }
                                }
                            }
                            else
                            {
                                sanPham.HinhAnhSP = "loi-404.jpg"; // Gán ảnh mặc định nếu không có ảnh
                            }




                            // Kiểm tra xem sản phẩm đã tồn tại trong cơ sở dữ liệu chưa
                            var existingProduct = _context.SanPham
                                .FirstOrDefault(sp => sp.TEN_SP == sanPham.TEN_SP && sp.Slug == sanPham.Slug);

                            if (existingProduct == null)  // Nếu sản phẩm chưa tồn tại
                            {
                                _context.SanPham.Add(sanPham);  // Thêm sản phẩm mới
                                TempData["SuccessMessage"] = "Tải dữ liệu từ Excel thành công!";
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "Sản phẩm đã tồn tại, không thêm!";
                            }
                        }

                        await _context.SaveChangesAsync();  // Lưu tất cả sản phẩm vào DB
                      
                    }
                }

                return RedirectToAction("Index");  // Chuyển đến trang danh sách sản phẩm
            }

            TempData["ErrorMessage"] = "File không hợp lệ!";
            return View();
        }



    }
}
