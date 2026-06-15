using Bank_Account.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Principal;


namespace Bank_Account.Controllers
{

    public class FundTransferController1 : Controller
    {

        public void clearForm(FundTransferModel model)
        {
            model.FromAccount = "";
            model.ToAccount = "";
            model.Amount = 0;
            model.TransactionDate = DateTime.Now;
            model.TransactionPassword = "";
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FundTransferModel model)
        {

            string password = model.TransactionPassword;
            string hashed = BCrypt.Net.BCrypt.HashPassword(password);
            string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Bank;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;";


            SqlConnection con = new SqlConnection(cs);
            string query = "Insert into Transactions(FromAccount , ToAccount , Amount ,TransactionDate,TransactionPassword) values (@FromAccount, @ToAccount, @Amount,@TransactionDate,@TransactionPassword)";
            SqlCommand queryRun = new SqlCommand(query, con);
            queryRun.Parameters.AddWithValue("@FromAccount", model.FromAccount);
            queryRun.Parameters.AddWithValue("@ToAccount", model.ToAccount);
            queryRun.Parameters.AddWithValue("@Amount", model.Amount);


            queryRun.Parameters.AddWithValue("@TransactionDate", model.TransactionDate);
            queryRun.Parameters.AddWithValue("@TransactionPassword", hashed);

            con.Open();

            queryRun.ExecuteNonQuery();
            ViewBag.SuccessMessage = "Transfer completed successfully!";
            ModelState.Clear();
            clearForm(model);

            return View(model);
        }

    }
}




