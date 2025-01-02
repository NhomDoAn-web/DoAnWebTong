using DoAnWEBDEMO.ApplicationDB;
using Microsoft.AspNetCore.Mvc;

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


        public IActionResult Index()
        {
            return View();
        }
    }
}
