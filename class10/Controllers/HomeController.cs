using System.Diagnostics;
using class10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace class10.Controllers
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

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("ghazalasehar644@gmail.com");
                mail.To.Add("aptechsrj176@gmail.com");


                mail.From = new MailAddress("kasbi2233@gmail.com");
                mail.Subject = "Asp.net MVC Core mail";
                mail.Body = "<h3>Hello ASP.NET </h3>";
                mail.IsBodyHtml = true;
                mail.ReplyTo = new MailAddress("testheaven617@gmail.com");


                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("kasbi2233@gmail.com", "iwfx svhu ivnl nbof");

                smtp.Send(mail);
            }
            catch (Exception er)
            {
                ViewBag.error = er.Message;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
