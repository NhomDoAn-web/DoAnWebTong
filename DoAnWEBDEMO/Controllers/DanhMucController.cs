using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoAnWEBDEMO.Controllers
{
    public class DanhMucController : Controller
    {

        private readonly ApplicationDb _db;

        public DanhMucController(ApplicationDb db)
        {
            _db = db;
        }


        //Dữ liệu Danh mục
        public JsonResult getDanhMuc()
        {
            var categories = _db.DanhMuc.Select(c => new { c.Ma_DM, c.TenDM }).ToList();
            Debug.WriteLine(categories);
            return Json(categories);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

