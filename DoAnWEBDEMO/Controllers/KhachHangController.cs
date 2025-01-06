
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
                return Json(new { showLoginModal = true });
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
                return Json(new { showLoginModal = true });
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
        // Phương thức POST để cập nhật thông tin tài khoản
        [HttpPost]
        public IActionResult EditProfile(string hoKH, string tenKH, string gioiTinh, string email, string sdt, string diaChi, string tenNguoiDung)
        {
            try
            {
                var khachHangJson = HttpContext.Session.GetString("user");

                if (string.IsNullOrEmpty(khachHangJson))
                {
                    return RedirectToAction("Login", "Account");
                }

                var thongTinkhachHang = JsonSerializer.Deserialize<KhachHang>(khachHangJson);

                if (thongTinkhachHang != null)
                {
                    var customer = _db.KhachHang.FirstOrDefault(c => c.MaKH == thongTinkhachHang.MaKH);

                    if (customer != null)
                    {
                        var existingAccount = _db.KhachHang.FirstOrDefault(kh => (kh.Email == email || kh.TENNGUOIDUNG == tenNguoiDung) && kh.MaKH != thongTinkhachHang.MaKH);
                        if (existingAccount != null)
                        {
                            TempData["ErrorMessage"] = "Email hoặc tên người dùng đã tồn tại.";
                            return RedirectToAction("EditProfile");
                        }

                        // Cập nhật thông tin khách hàng
                        customer.HoKH = hoKH;
                        customer.TenKH = tenKH;
                        customer.GioiTinh = gioiTinh;
                        customer.Email = email;
                        customer.SDT = sdt;
                        customer.DiaChi = diaChi;
                        customer.TENNGUOIDUNG = tenNguoiDung;

                        _db.KhachHang.Update(customer);
                        _db.SaveChanges();

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
        

        //[HttpPost]
        //public JsonResult EditProfile(string hoKH, string tenKH, string gioiTinh, string email, string sdt, string diaChi, string tenNguoiDung)
        //{
        //    try
        //    {
        //        var userName = HttpContext.Session.GetString("user");
        //        Debug.WriteLine("Tên đăng nhập: " + userName);

        //        if (string.IsNullOrEmpty(userName))
        //        {
        //            return Json(new { value = false, message = "Vui lòng đăng nhập để chỉnh sửa thông tin." });
        //        }

        //        var customer = _db.KhachHang.FirstOrDefault(c => c.TenKH == userName);
        //        if (customer == null)
        //        {
        //            return Json(new { value = false, message = "Không tìm thấy thông tin người dùng." });
        //        }

        //        // Kiểm tra email hoặc tên người dùng trùng lặp
        //        var existingAccount = _db.KhachHang.FirstOrDefault(kh => (kh.Email == email || kh.TENNGUOIDUNG == tenNguoiDung) && kh.TenKH != userName);
        //        if (existingAccount != null)
        //        {
        //            return Json(new { value = false, message = "Email hoặc tên người dùng đã tồn tại." });
        //        }

        //        // Cập nhật thông tin
        //        customer.HoKH = hoKH;
        //        customer.TenKH = tenKH;
        //        customer.GioiTinh = gioiTinh;
        //        customer.Email = email;
        //        customer.SDT = sdt;
        //        customer.DiaChi = diaChi;
        //        customer.TENNGUOIDUNG = tenNguoiDung;

        //        _db.KhachHang.Update(customer);
        //        _db.SaveChanges();

        //        Debug.WriteLine("Cập nhật thành công!");

        //        return Json(new { value = true, message = "Cập nhật thông tin thành công!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Lỗi: " + ex.Message);
        //        return Json(new { value = false, message = "Có lỗi xảy ra: " + ex.Message });
        //    }
        //}



        //

           
       

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
        public async Task<IActionResult> SanPhamYeuThich()
        {
            // Lấy ID khách hàng từ session
            var maKH = GetLoggedInKhachHangId();

            // Kiểm tra nếu maKH là null, có nghĩa là người dùng chưa đăng nhập
            if (!maKH.HasValue)
            {
                ViewBag.Message = "Vui lòng đăng nhập để xem sản phẩm yêu thích.";
                return View(); // Trả về view hiện tại mà không chuyển hướng
            }

            // Lấy danh sách sản phẩm yêu thích của khách hàng
            var sanPhamYeuThich = await _db.SanPhamYeuThich
                .Include(d => d.KhachHang)  // Liên kết với bảng KhachHang
                .Include(d => d.SanPham)    // Liên kết với bảng SanPham
                .Where(d => d.KhachHangId == maKH.Value)  // Lọc theo KhachHangId
                .ToListAsync();

            // Trả về View và gửi thông báo nếu không có dữ liệu
            if (sanPhamYeuThich == null || !sanPhamYeuThich.Any())
            {
                ViewBag.Message = "Không có sản phẩm yêu thích.";
            }

            return View(sanPhamYeuThich); // Trả về view và danh sách sản phẩm yêu thích
        }



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
        public IActionResult Index()
        {
            return View();
        }
    }
}

