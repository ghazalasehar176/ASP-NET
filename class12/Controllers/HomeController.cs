using System.Diagnostics;
using class12.Models;
using Microsoft.AspNetCore.Mvc;

namespace class12.Controllers
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

        public IActionResult Dashboard()
        {
            string username = HttpContext.Session.GetString("username");

            if (username == null )
            {
                return RedirectToAction("Index" , "AccountController1");
            }

            ViewBag.username = username;
            return View();
        }

    }
}
