using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoAnWEBDEMO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("user");

            if (user != null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            else
                return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
