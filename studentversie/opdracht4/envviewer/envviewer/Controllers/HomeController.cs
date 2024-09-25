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
            HomeModel model = new HomeModel();
            model.Title = Environment.GetEnvironmentVariable("Title");
            model.SubTitle = Environment.GetEnvironmentVariable("SubTitle");
            model.ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");

            return View(model);
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
