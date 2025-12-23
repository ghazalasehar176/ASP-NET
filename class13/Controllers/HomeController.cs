// Install package : BCrypt.Net-Next

using System.Diagnostics;
using class13.Models;
using Microsoft.AspNetCore.Mvc;

//Ye BCrypt library ko use karne ke liye namespace include karta hai
using BCrypt.Net;

namespace class13.Controllers
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

            //Ye user ka original password hai
            string password = "abc123@#";
            //Ye password secure form (hash) me convert kar raha hai
            string hased = BCrypt.Net.BCrypt.HashPassword(password);

            //Ye check karta hai ki jo password user ne enter kiya ("abc1223@#"), kya wo hash ke original password ke match karta hai
            bool verifyPass = BCrypt.Net.BCrypt.Verify("abc1223@#" , hased);


            if (verifyPass)
            {
                ViewBag.result = "Password is correct";
            }
            else
            {
                ViewBag.result = "Incorrect password";
            }

                 ViewBag.hased = hased;
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
