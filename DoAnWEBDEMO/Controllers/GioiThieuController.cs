using DoAnWEBDEMO.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace DoAnWEBDEMO.Controllers
{
    public class GioiThieuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
