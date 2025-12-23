using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace assignment.Controllers
{
    public class EditorController : Controller
    {
        [Authorize(Roles ="Editor , Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
