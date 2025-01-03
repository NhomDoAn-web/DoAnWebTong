using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using DoAnWEBDEMO.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

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
                // Kiểm tra xem tài khoản đã tồn tại hay chưa
                var existingAccount = _db.KhachHang.FirstOrDefault(kh => kh.Email == email || kh.TENNGUOIDUNG == tenNguoiDung);
                if (existingAccount != null)
                {
                    return Json(new { value = false, message = "Email hoặc tên người dùng đã tồn tại." });
                }

                // Mã hóa mật khẩu trước khi lưu
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(matKhau);

                // Tạo đối tượng KhachHang mới
                var newCustomer = new KhachHang
                {
                    HoKH = hoKH,
                    TenKH = tenKH,
                    GioiTinh = gioiTinh,
                    Email = email,
                    SDT = sdt,
                    DiaChi = diaChi,
                    TENNGUOIDUNG = tenNguoiDung,
                    MATKHAU = hashedPassword
                };

                // Thêm vào cơ sở dữ liệu và lưu thay đổi
                _db.KhachHang.Add(newCustomer);
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
                // Sử dụng BCrypt để xác minh mật khẩu
                if (BCrypt.Net.BCrypt.Verify(matKhau, checkTaiKhoan.MATKHAU))
                {
                    HttpContext.Session.SetString("user", checkTaiKhoan.TenKH);
                    return Json(new { value = true });
                }
            }

            return Json(new { value = false });
        }
        //Thông tin tài khoản khách hàng
        public IActionResult Profile()
        {
            var userName = HttpContext.Session.GetString("user");

            if (string.IsNullOrEmpty(userName))
            {
                // Nếu không có thông tin đăng nhập, trả về thông tin yêu cầu mở modal đăng nhập
                return Json(new { showLoginModal = true });
            }

            var customer = _db.KhachHang.FirstOrDefault(c => c.TenKH == userName);

            if (customer == null)
            {
                // Nếu không tìm thấy thông tin khách hàng, trả về yêu cầu mở modal đăng nhập
                return Json(new { showLoginModal = true });
            }

            // Nếu tìm thấy khách hàng, trả về thông tin khách hàng
            return View(customer);
        }

        // Sửa thông tin tài khoản 
        
        public IActionResult EditProfile()
        {
            // Phương thức GET để hiển thị thông tin tài khoản
            var userName = HttpContext.Session.GetString("user");

            if (string.IsNullOrEmpty(userName))
            {
                // Nếu không có thông tin đăng nhập, yêu cầu mở modal đăng nhập
                return Json(new { showLoginModal = true });
            }

            var customer = _db.KhachHang.FirstOrDefault(c => c.TenKH == userName);

            if (customer == null)
            {
                // Nếu không tìm thấy thông tin khách hàng, yêu cầu mở modal đăng nhập
                return Json(new { showLoginModal = true });
            }

            // Trả về view với thông tin khách hàng
            return View(customer);
        }
        // Phương thức POST để cập nhật thông tin tài khoản
        [HttpPost]
        public JsonResult EditProfile(string hoKH, string tenKH, string gioiTinh, string email, string sdt, string diaChi, string tenNguoiDung)
        {
            try
            {
                var userName = HttpContext.Session.GetString("user");

                if (string.IsNullOrEmpty(userName))
                {
                    return Json(new { value = false, message = "Vui lòng đăng nhập để chỉnh sửa thông tin." });
                }

                var customer = _db.KhachHang.FirstOrDefault(c => c.TenKH == userName);

                if (customer == null)
                {
                    return Json(new { value = false, message = "Không tìm thấy thông tin người dùng." });
                }

                // Kiểm tra xem email hoặc tên người dùng đã tồn tại chưa
                var existingAccount = _db.KhachHang.FirstOrDefault(kh => (kh.Email == email || kh.TENNGUOIDUNG == tenNguoiDung) && kh.TenKH != userName);
                if (existingAccount != null)
                {
                    return Json(new { value = false, message = "Email hoặc tên người dùng đã tồn tại." });
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

                // Trả về JSON thông báo thành công và URL trang Profile
                return Json(new { value = true, message = "Cập nhật thông tin thành công!", redirectUrl = Url.Action("Profile", "KhachHang") });
            }
            catch (Exception ex)
            {
                return Json(new { value = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }
        //đang sai chuyển hướng khi cập nhật thành công 


        //

        [HttpPost]
        public JsonResult khachHangDangXuat()
        {
            HttpContext.Session.Clear();
            return Json(new { value = true });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
