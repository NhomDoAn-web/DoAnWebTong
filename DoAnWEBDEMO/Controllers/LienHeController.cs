using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using DoAnWEBDEMO.ApplicationDB;

namespace DoAnWEBDEMO.Controllers
{
    public class LienHeController : Controller
    {
        private readonly ApplicationDb _db;

        public LienHeController(ApplicationDb db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string HO_TEN, string EMAIL, string SDT, string NOI_DUNG)
        {
            if (ModelState.IsValid)
            {
                var lienHe = new LienHe
                {
                    HO_TEN = HO_TEN,
                    EMAIL = EMAIL,
                    SDT = SDT,
                    NOI_DUNG = NOI_DUNG,   
                    THOI_GIAN_GUI = DateTime.Now,
                    TRANG_THAI = 1 // Mặc định đang xử lý
                };

                _db.LienHe.Add(lienHe);
                _db.SaveChanges();

                // Lưu thông báo thành công vào TempData
                TempData["SuccessMessage"] = "Cảm ơn bạn đã liên hệ với chúng tôi!";
                return RedirectToAction("Index");
            }

            // Nếu dữ liệu không hợp lệ, hiển thị lại form
            return View();
        }

    }
}
