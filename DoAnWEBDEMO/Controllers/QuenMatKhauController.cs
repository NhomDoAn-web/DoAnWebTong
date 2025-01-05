using DoAnWEBDEMO.ApplicationDB;
using DoAnWEBDEMO.Services;
using Microsoft.AspNetCore.Mvc;
using DoAnWEBDEMO.Models;
using System.Diagnostics;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;

namespace DoAnWEBDEMO.Controllers
{
    public class QuenMatKhauController : Controller
    {

        private readonly EmailService _email;
        private readonly ApplicationDb _db;
        private int dem_LayLaiMatKhau = 0;
        public QuenMatKhauController(EmailService email, ApplicationDb db)
        {
            _email = email;
            _db = db;
        }


        [HttpPost]
        public async Task<ActionResult> LayLaiMatKhau(string email)
        {
            if (dem_LayLaiMatKhau == 0)
            {
                dem_LayLaiMatKhau++;
                Debug.WriteLine("email: " + email);
                var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.Email == email);

                if (khachHang == null)
                {
                    TempData["ThatBai"] = "Thất bại";
                    Debug.WriteLine("Thất bại: ");
                    return View();
                }    

                var otp = new Random().Next(100000, 999999).ToString();

                HttpContext.Session.SetString("otp", otp);
                HttpContext.Session.SetString("email", email);
                await _email.GuiOTP(email, otp);

                TempData["ThanhCong"] = "Thành Công";
                
            }
            return View();
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("user");

            if(user != null)
            {
                return RedirectToAction("Index", "Home");
            }    
            else
                return View();
        }


        [HttpPost]
        public IActionResult DoiMatKhau(string OTP, string matKhau)
        {
            var email = HttpContext.Session.GetString("email");
            var otp_email = HttpContext.Session.GetString("otp");

            Debug.WriteLine("-----------MAT KHAU: " + matKhau);
            Debug.WriteLine("-----------: " + email);
            Debug.WriteLine("===========: " + otp_email);

            if(OTP != null && matKhau != null && OTP == otp_email)
            {
                var khachHang = _db.KhachHang.FirstOrDefault(kh => kh.Email == email);

                if(khachHang != null)
                {
                    string maHoaMatKhau = BCrypt.Net.BCrypt.HashPassword(matKhau);
                    khachHang.MATKHAU = maHoaMatKhau;
                    _db.SaveChanges();
                    TempData["DoiMatKhau"] = "DoiMatKhau";

                    HttpContext.Session.Remove("email");
                    HttpContext.Session.Remove("otp");

                    return View();
                }    
            }    
            return View();
        }

        public IActionResult LayLaiMatKhau()
        {
            var user = HttpContext.Session.GetString("user");

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return View();
        }
    }
}
