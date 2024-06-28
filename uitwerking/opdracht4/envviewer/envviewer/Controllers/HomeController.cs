using envviewer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace envviewer.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            ViewBag.Title = Environment.GetEnvironmentVariable("Title");
            ViewBag.SubTitle = Environment.GetEnvironmentVariable("SubTitle");
            ViewBag.ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");

            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
