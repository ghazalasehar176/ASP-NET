using System.Diagnostics;
using classTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace classTask.Controllers
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

        private static List<Employee> employees = new List<Employee>();


        [HttpGet]
        public IActionResult employeeForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Employee(Employee employee)
        {
            employees.Add(employee);
            return RedirectToAction("Privacy");
        }

        public IActionResult Privacy()
        {
            return View(employees);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
