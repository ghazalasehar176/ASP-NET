using class12.Models;
using Microsoft.AspNetCore.Http;
//Ye line app me ASP.NET Core Identity ke features (login, register, roles) use karne ke liye lagayi jaati hai.
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace class12.Controllers
{
    public class AccountController : Controller
    {

        //ek list create kar raha hai jisme users ka data store hai.
        public List<LoginViewModel> users = new List<LoginViewModel>
        {
            new LoginViewModel{ username = "admin" , password= "123"}
        };

        //get
        //Browser me login page show karta hai
        public IActionResult Index()
        {
            return View();
        }

        //Post
        //Form submit hone par ye run hota hy.username aur password form se aata hai
                [HttpPost]
        public IActionResult Index(string username , string password )
        {
            //List me match karta hy → agar user exist nahi karta → null

            //FirstOrDefault() collection me first matching item return karta hai, warna null.
            var user = users.FirstOrDefault(x => x.username == username && x.password == password);

            //agar user null hoga matlab username/password galat hai tu ye index page show karyga

            if (user == null )
            {
                return View("Index");
            }

            //Session me username store karta hai

            HttpContext.Session.SetString("username", username);
            // Fir Dashboard page redirect karta hai
            return RedirectToAction("Dashboard", "Home");
        }

        public IActionResult Logout()
        {
            //Session se username remove karna
            HttpContext.Session.Remove("username");
            //Login page pe redirect
            return RedirectToAction("Index");
        } 
    }
}
