using Microsoft.AspNetCore.Mvc;

namespace class3.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection obj)
        {
            try
            {
                int num1 = Convert.ToInt32(obj["num1"]);
                int num2 = Convert.ToInt32(obj["num2"]);

                string opt = obj["operator"];

                string result = string.Empty;

                switch (opt)
                {
                    case "Add":
                        result = $"Addition: {num1 + num2}";
                        break;

                    case "Sub":
                        result = $"Subtraction: {num1 - num2}";
                        break;

                    case "Multiply":
                        result = $"Multiplication: {num1 * num2}";
                        break;

                    case "Divide":
                        result = $"Divison: {num1 + num2}";
                        break;
                }
                ViewBag.result = result;
            }
            catch (Exception ex)
            {
                ViewBag.result = $"Error: {ex.Message}";
            }

            return View();
        } 
    }
}
