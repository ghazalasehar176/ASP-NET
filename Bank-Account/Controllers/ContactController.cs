using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using Bank_Account.Models;

namespace Bank_Account.Controllers
{
    public class ContactController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string cs;

        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
            cs = _configuration.GetConnectionString("Dbcon");
        }

        // GET: Contact
        public IActionResult Index()
        {
            return View();
        }
      

        // POST: Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {

                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = @"INSERT INTO Contact (Name, Email, Phone, Message)
                                     VALUES (@Name, @Email, @Phone, @Message)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Phone", model.Phone);
                    cmd.Parameters.AddWithValue("@Message", model.Message);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                // Send email
                MailMessage mail = new MailMessage();
                mail.To.Add("testheaven617@gmail.com");
                mail.From = new MailAddress("testheaven617@gmail.com");
                mail.Subject = "New Contact Message";
                mail.Body = $"<h3>{model.Name}</h3><p>{model.Message}</p>";
                mail.IsBodyHtml = true;


                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("testheaven617@gmail.com", "wuzg bgxz ykva kwej");
                smtp.Send(mail);

                ViewBag.success = "Message sent successfully!";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
            }

            return View();
        }
    }
}

