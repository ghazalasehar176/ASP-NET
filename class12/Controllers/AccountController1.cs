using class12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace class12.Controllers
{
    public class AccountController1 : Controller
    {

        public List<LoginViewModel> users = new List<LoginViewModel>
        {
            new LoginViewModel{ username = "admin" , password = "12345"}
        };
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(string username , string password)
        {
            var user = users.FirstOrDefault(x => x.username == username && x.password == password);

            if (user == null)
            {
                return View("Index");
            }
            HttpContext.Session.SetString("username" , username);
            return RedirectToAction("Dashboard" , "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }


    }
}
