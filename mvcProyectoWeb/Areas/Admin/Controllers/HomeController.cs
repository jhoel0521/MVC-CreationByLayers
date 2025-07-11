using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvcProyectoWeb.Models;

namespace mvcProyectoWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
