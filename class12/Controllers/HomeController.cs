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

        //Ye code Dashboard page access control aur display handle kar raha hai.
        public IActionResult Dashboard()
        {
            //Pehle check kar raha hai ki user login hai ya nahi
            string username = HttpContext.Session.GetString("username");

            //Agar session me username null hai ? user login nahi ? login page (Index) pe bhej do
            if (username == null) 
            {
                return RedirectToAction("Index" , "Account");
            }

            //User login hai ? View me show
            ViewBag.username = username;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
