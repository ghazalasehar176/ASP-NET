using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace assignment.Controllers
{
    public class AdminController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Authorize(Roles ="Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
