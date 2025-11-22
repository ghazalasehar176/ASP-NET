using class4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace class4.Controllers
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
            
            ViewData["msg"] = "View Data Object"; 
        //Purpose: Controller se View me temporary data bhejne ke liye.
        //Lifetime: Current request tak, redirect ke baad nahi rahega. ViewData
            ViewBag.data = "View bag Method";
        //Purpose: Same as ViewData, lekin easier syntax (dynamic property).
        // Lifetime: Current request only.ViewBage.Data
            TempData["Message"] = "Temp data object";
            //Purpose: Controller ? next action/view me data bhejne ke liye (redirect ke baad bhi milega).
            //Lifetime: Next request only(agar TempData.Keep() use kiya to ek aur request ke liye).


            //return RedirectToAction("About");
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.Message = "Your application description page";
            TempData.Keep("Message");
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
