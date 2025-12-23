using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace class2.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Ye batata hai ke controller action kya result return karega
        //Yahan IActionResult matlab hai ke ye method kuch bhi return kar sakta hai jo action ka result ho — jaise:
        //View() → page show karega
        //RedirectToAction() → dusre page pe le jayega
        //Json() → data return karega
        public IActionResult Data()
        {
            return View();
        }
    }
}
