using Microsoft.AspNetCore.Mvc;

namespace OnTapLan1_Admin.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {

        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
