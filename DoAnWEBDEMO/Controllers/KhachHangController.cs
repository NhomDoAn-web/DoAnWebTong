
﻿using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using DoAnWEBDEMO.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using MimeKit.Encodings;

namespace DoAnWEBDEMO.Controllers
{
    public class KhachHangController : Controller
    {

        private readonly ApplicationDb _db;

        public KhachHangController(ApplicationDb db)
        {
            _db = db;
        }

        
        //khách hàng đăng ký

        public IActionResult Register()
        {
            return View(); // trả về view đăng ký
        }
        [HttpPost]
        public JsonResult Register(string hoKH, string tenKH, string gioiTinh, string email, string sdt, string diaChi, string tenNguoiDung, string matKhau)
        {
            try
            {
                var existingAccount = _db.KhachHang.FirstOrDefault(kh => kh.Email == email || kh.TENNGUOIDUNG == tenNguoiDung);
                if (existingAccount != null)
                {
                    return Json(new { value = false, message = "Email hoặc tên người dùng đã tồn tại." });
                }

                var khachHang = new KhachHang
                {
                    HoKH = hoKH,
                    TenKH = tenKH,
                    GioiTinh = gioiTinh,
                    Email = email,
                    SDT = sdt,
                    DiaChi = diaChi,
                    TENNGUOIDUNG = tenNguoiDung,
                    MATKHAU = matKhau
                };

                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(khachHang, null, null);
                if (!Validator.TryValidateObject(khachHang, validationContext, validationResults, true))
                {
                    string errorMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
                    return Json(new { value = false, message = "Dữ liệu không hợp lệ: " + errorMessage });
                }

                // Mã hóa mật khẩu trước khi lưu
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(matKhau);
                khachHang.MATKHAU = hashedPassword;

                // Thêm vào cơ sở dữ liệu và lưu thay đổi
                _db.KhachHang.Add(khachHang);
                _db.SaveChanges();

                return Json(new { value = true, message = "Đăng ký thành công!" });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo lỗi
                return Json(new { value = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }


        //Thông tin Giỏ hàng của Khách hàng
        public void getThongTinGioHang()
        {
            HttpContext.Session.Remove("tongTien");
            HttpContext.Session.Remove("soLuong");
            var khachHangJson = HttpContext.Session.GetString("user");
            
            if(khachHangJson != null)
            {
                var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(khachHangJson);
            
                if(thongTinkhachHang != null)
                {
                    var result = (from ctgh in _db.ChiTietGioHang
                                  join sp in _db.SanPham on ctgh.MaSP equals sp.MaSP
                                  select new
                                  {
                                      TongTien = ctgh.Soluong * sp.Gia,
                                      SoLuong = ctgh.Soluong
                                  });
            
                    if (result != null)
                    {
                        var duLieu = new
                        {
                            TongTien = result.Sum(x => x.TongTien),
                            SoLuong = result.Sum(e => e.SoLuong)
                        };
                        if(duLieu != null)
                        {
                            HttpContext.Session.SetInt32("tongTien", (int)duLieu.TongTien);
                            HttpContext.Session.SetInt32("soLuong", (int)duLieu.SoLuong);
                        }    
                    }    
                    else
                    {
                        HttpContext.Session.SetInt32("tongTien", 0);
                        HttpContext.Session.SetInt32("soLuong", 0);
            
                    }
            
                }    
            }    
            
        }

        //Khách hàng đăng nhập - Json
        public JsonResult getKhachHangDangNhap(string taiKhoan, string matKhau)
        {
            var checkTaiKhoan = _db.KhachHang.FirstOrDefault(tk => tk.Email == taiKhoan || tk.TENNGUOIDUNG == taiKhoan);
            Debug.WriteLine("Tài khoản: " + taiKhoan);
            Debug.WriteLine("Mật khẩu: " + matKhau);
            if (checkTaiKhoan != null)
            {
                if (BCrypt.Net.BCrypt.Verify(matKhau, checkTaiKhoan.MATKHAU))
                {
                    HttpContext.Session.SetString("user", JsonSerializer.Serialize(checkTaiKhoan));
                    //TenKH
                    //String - JSON
                    getThongTinGioHang();
                    return Json(new { value = true });
                }
            }
            return Json(new { value = false });
        }


        [HttpPost]
        public JsonResult khachHangDangXuat()
        {
            HttpContext.Session.Clear();
            return Json(new { value = true });
        }

        //Thông tin tài khoản khách hàng
        public IActionResult Profile()
        {
            var khachHangJson = HttpContext.Session.GetString("user");

            if (string.IsNullOrEmpty(khachHangJson))
            {
                return RedirectToAction("Index", "TrangChu");
            }

            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(khachHangJson);

            if (thongTinkhachHang != null)
            {
                var customer = _db.KhachHang.FirstOrDefault(c => c.MaKH == thongTinkhachHang.MaKH);
                Debug.WriteLine("========: " + customer);

                if (customer != null)
                {
                    return View(customer);
                }
            }

            return View();
        }


        // Sửa thông tin tài khoản 

        public IActionResult EditProfile()
        {
            var khachHangJson = HttpContext.Session.GetString("user");

            if (string.IsNullOrEmpty(khachHangJson))
            {
                return RedirectToAction("Index", "TrangChu");
            }

            var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(khachHangJson);

            if(thongTinkhachHang != null)
            {
                var customer = _db.KhachHang.FirstOrDefault(c => c.MaKH == thongTinkhachHang.MaKH);

                if(customer != null)
                {
                    return View(customer);
                }    
            }    


            return View();
        }

        // Sửa thông tin tài khoản 
        // Phương thức POST để cập nhật thông tin tài khoản
        [HttpPost]
        public IActionResult EditProfile(string hoKH, string tenKH, string gioiTinh, string email, string sdt, string diaChi)
        {
            try
            {
                var khachHangJson = HttpContext.Session.GetString("user");

                if (string.IsNullOrEmpty(khachHangJson))
                {
                    return RedirectToAction("Index", "TrangChu");
                }

                var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(khachHangJson);

                if (thongTinkhachHang != null)
                {
                    var customer = _db.KhachHang.FirstOrDefault(c => c.MaKH == thongTinkhachHang.MaKH);

                    if (customer != null)
                    {
                        // Kiểm tra email hoặc tên người dùng đã tồn tại
                        var existingAccount = _db.KhachHang.FirstOrDefault(kh => kh.Email == email && kh.MaKH != thongTinkhachHang.MaKH);
                        if (existingAccount != null)
                        {
                            TempData["ErrorMessage"] = "Email đã tồn tại.";
                            return RedirectToAction("EditProfile");
                        }

                        // Cập nhật thông tin khách hàng (Không thay đổi tên người dùng)
                        customer.HoKH = hoKH;
                        customer.TenKH = tenKH;
                        customer.GioiTinh = gioiTinh;
                        customer.Email = email;
                        customer.SDT = sdt;
                        customer.DiaChi = diaChi;

                        _db.KhachHang.Update(customer);
                        _db.SaveChanges();
                        HttpContext.Session.SetString("user", JsonSerializer.Serialize(customer));
                        TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
                        return RedirectToAction("Profile");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction("EditProfile");
            }

            return View();
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
                var customer = _db.KhachHang.FirstOrDefault(kh => kh.MaKH == thongTinkhachHang.MaKH);

                if (customer != null)
                {
                    return customer.MaKH;
                }
            }

            return null;
        }
        //public async Task<IActionResult> SanPhamYeuThich()
        //{
        //    // Lấy ID khách hàng từ session
        //    var maKH = GetLoggedInKhachHangId();

        //    // Kiểm tra nếu maKH là null, có nghĩa là người dùng chưa đăng nhập
        //    if (!maKH.HasValue)
        //    {
        //        ViewBag.Message = "Vui lòng đăng nhập để xem sản phẩm yêu thích.";
        //        return View(); // Trả về view hiện tại mà không chuyển hướng
        //    }

        //    // Lấy danh sách sản phẩm yêu thích của khách hàng
        //    var sanPhamYeuThich = await _db.SanPhamYeuThich
        //        .Include(d => d.KhachHang)  // Liên kết với bảng KhachHang
        //        .Include(d => d.SanPham)    // Liên kết với bảng SanPham
        //        .Where(d => d.KhachHangId == maKH.Value)  // Lọc theo KhachHangId
        //        .ToListAsync();

        //    // Trả về View và gửi thông báo nếu không có dữ liệu
        //    if (sanPhamYeuThich == null || !sanPhamYeuThich.Any())
        //    {
        //        ViewBag.Message = "Không có sản phẩm yêu thích.";
        //    }

        //    return View(sanPhamYeuThich); // Trả về view và danh sách sản phẩm yêu thích
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken] // Bảo vệ chống CSRF
        //public async Task<IActionResult> XoaSanPhamYeuThich(int sanPhamId)
        //{
        //    // Lấy ID khách hàng từ session
        //    var maKH = GetLoggedInKhachHangId();

        //    // Kiểm tra nếu maKH là null, có nghĩa là người dùng chưa đăng nhập
        //    if (!maKH.HasValue)
        //    {
        //        ViewBag.Message = "Vui lòng đăng nhập để thực hiện thao tác này.";
        //        return RedirectToAction("Index", "TrangChu"); // Điều hướng đến trang chủ nếu người dùng chưa đăng nhập
        //    }

        //    // Tìm sản phẩm yêu thích của khách hàng với ID sản phẩm và Khách hàng ID
        //    var sanPhamYeuThich = await _db.SanPhamYeuThich
        //        .FirstOrDefaultAsync(sp => sp.KhachHangId == maKH.Value && sp.SanPhamId == sanPhamId);

        //    // Kiểm tra xem sản phẩm có trong danh sách yêu thích của khách hàng hay không
        //    if (sanPhamYeuThich == null)
        //    {
        //        ViewBag.Message = "Sản phẩm không có trong danh sách yêu thích.";
        //        return RedirectToAction("SanPhamYeuThich"); // Điều hướng lại đến danh sách sản phẩm yêu thích
        //    }

        //    // Xóa sản phẩm yêu thích khỏi danh sách
        //    _db.SanPhamYeuThich.Remove(sanPhamYeuThich);
        //    await _db.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

        //    // Trả về thông báo thành công và điều hướng lại đến danh sách sản phẩm yêu thích
        //    ViewBag.Message = "Đã xóa sản phẩm khỏi danh sách yêu thích.";
        //    return RedirectToAction("SanPhamYeuThich");
        //}

        //
        public async Task<IActionResult> SanPhamChamDiem()
        {
            // Lấy ID khách hàng từ session
            var maKH = GetLoggedInKhachHangId();

            // Kiểm tra nếu maKH là null, có nghĩa là người dùng chưa đăng nhập
            if (!maKH.HasValue)
            {
                ViewBag.Message = "Vui lòng đăng nhập để xem sản phẩm chấm điểm.";
                return View(); // Trả về view hiện tại mà không chuyển hướng
            }

            // Lấy danh sách sản phẩm đã được khách hàng chấm điểm
            var sanPhamChamDiem = await _db.ChiTietBinhLuan
                .Include(d => d.KhachHang)  // Liên kết với bảng KhachHang
                .Include(d => d.SanPham)    // Liên kết với bảng SanPham
                .Where(d => d.MA_KH == maKH.Value)  // Lọc theo MaKH
                .ToListAsync();

            // Trả về View và gửi thông báo nếu không có dữ liệu
            if (sanPhamChamDiem == null || !sanPhamChamDiem.Any())
            {
                ViewBag.Message = "Không có sản phẩm chấm điểm.";
            }

            return View(sanPhamChamDiem); // Trả về view và danh sách sản phẩm đã chấm điểm
        }
        //sửa chấm điểm 
        public async Task<IActionResult> SuaChamDiem(int sanPhamId)
        {
            var maKH = GetLoggedInKhachHangId();

            // Kiểm tra nếu khách hàng chưa đăng nhập
            if (!maKH.HasValue)
            {
                TempData["Message"] = "Vui lòng đăng nhập để sửa chấm điểm.";
                return RedirectToAction("Index", "TrangChu");
            }

            // Lấy chi tiết bình luận/chấm điểm của sản phẩm từ khách hàng đã đăng nhập
            var chiTietBinhLuan = await _db.ChiTietBinhLuan
                .FirstOrDefaultAsync(c => c.MA_KH == maKH.Value && c.MA_SP == sanPhamId);
            // Trả về view và truyền chi tiết bình luận/chấm điểm của sản phẩm
            return View(chiTietBinhLuan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaChamDiem(int sanPhamId, int rating, string comment)
        {
            var maKH = GetLoggedInKhachHangId();

            // Kiểm tra nếu khách hàng chưa đăng nhập
            if (!maKH.HasValue)
            {
                TempData["Message"] = "Vui lòng đăng nhập để sửa chấm điểm.";
                return RedirectToAction("Index", "TrangChu");
            }

            // Lấy chi tiết bình luận/chấm điểm của sản phẩm từ khách hàng đã đăng nhập
            var chiTietBinhLuan = await _db.ChiTietBinhLuan
                .FirstOrDefaultAsync(c => c.MA_KH == maKH.Value && c.MA_SP == sanPhamId);
            // Cập nhật chấm điểm và bình luận
            chiTietBinhLuan.SO_SAO = rating; // Cập nhật điểm đánh giá
            chiTietBinhLuan.NOI_DUNG = comment; // Cập nhật bình luận

            // Cập nhật vào cơ sở dữ liệu
            _db.ChiTietBinhLuan.Update(chiTietBinhLuan);
            await _db.SaveChangesAsync();

            // Cập nhật thông báo thành công
            TempData["Message"] = "Cập nhật chấm điểm thành công!";

            // Trả về cùng trang hiện tại để hiển thị thông báo
            return View(chiTietBinhLuan);
        }


        public IActionResult Index()
        {
            return View();
        }


    }
}

