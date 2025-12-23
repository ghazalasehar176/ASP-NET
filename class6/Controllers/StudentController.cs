using class6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using static System.Formats.Asn1.AsnWriter;


namespace class6.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {

            //Ye ek single object hai jo Student class ka hai
            //Student class me id, name, age properties hain.
            //student variable me ek hi student ka data store ho raha hai.

            //var student = new Student { id = 1  , name  = "Biya" , age = 12};

            var student = new List<Student>
            {
                new Student { id = 1 , name = "Ali" , age = 23},
                new Student { id = 2 , name = "areej" , age = 12 },
                new Student { id = 3 , name = "Abiha" , age = 14 },
                new Student { id = 4 , name = "Fatima" , age = 32},
            };

            return View(student);
        }
    }
}
