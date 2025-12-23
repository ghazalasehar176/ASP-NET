using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;


namespace assignment.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles ="User , Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
