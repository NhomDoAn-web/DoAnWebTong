using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
namespace DoAnWEBDEMO.Controllers
{
    public class KhachHangController : Controller
    {

        private readonly ApplicationDb _db;

        public KhachHangController(ApplicationDb db)
        {
            _db = db;
        }


        //Khách hàng đăng nhập - Json
        public JsonResult getKhachHangDangNhap(string taiKhoan, string matKhau)
        {
            var checkTaiKhoan = _db.KhachHang.FirstOrDefault(tk => tk.Email == taiKhoan || tk.TENNGUOIDUNG == taiKhoan);
            Debug.WriteLine("Tài khoản: " + taiKhoan);
            Debug.WriteLine("Mật khẩu: " + matKhau);
            if(checkTaiKhoan != null)
            {
                if(checkTaiKhoan.MATKHAU == matKhau)
                {
                    HttpContext.Session.SetString("user",checkTaiKhoan.TenKH);
                    return Json(new {value  = true});
                }    
            }    
            return Json(new {value = false});
        }

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
