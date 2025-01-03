using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;

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
       /* public JsonResult getDanhMuc()
        {
            var categories = _db.DanhMuc.Select(c => c.TenDM).ToList();
            return Json(categories);
        }*/

        public IActionResult Index()
        {
            return View();
        }
    }
}
