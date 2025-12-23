using System.Diagnostics;
using class11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace class11.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //get
        public IActionResult Index()
        {
            //user ki information temporary memory (session) me save kar raha hai taake har page me use ki ja sake.
            HttpContext.Session.SetString("username", "biya");
            HttpContext.Session.SetInt32("userId" , 12);
            HttpContext.Session.SetString("email", "biya123@gmail.com");
            return View();
        }


        public IActionResult About()
        {
            //Remove: Sirf "username" key ko session se delete karta hai. Baaki keys safe rahengi.
            //HttpContext.Session.Remove("username");

            //clear: Session ki saari keys (username, userId, email etc.) delete kar deta hai.
            //HttpContext.Session.Clear();


            //Ye code user ka data session se nikal ke view me use karne ke liye ViewBag me dalta hai
            ViewBag.user = HttpContext.Session.GetString("username");
            ViewBag.userId = HttpContext.Session.GetInt32("userId");
            ViewBag.email = HttpContext.Session.GetString("email");
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
