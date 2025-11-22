using System.Diagnostics;
using class5.Models;
using Microsoft.AspNetCore.Mvc;

namespace class5.Controllers
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

        [HttpPost]
        public IActionResult Privacy(string name , string email , string gender , string city , string password)
        {

            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.Password = password;
            ViewBag.Gender = gender;
            ViewBag.City = city;


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
