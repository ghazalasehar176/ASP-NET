using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using class7.Models;


namespace class7.Controllers
{
    public class StudentController : Controller
    {

        string cs = ConfigurationManager.ConnectionStrings["testdb"].ConnectionString;


        // GET: Student/Create
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student std)
        {
            try
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "Insert into Student(Name , Age , stdAddress)values(@Name , @Age , @Address)";

                SqlCommand queryRun = new SqlCommand(query, con);

                queryRun.Parameters.AddWithValue("@Name", std.Name);
                queryRun.Parameters.AddWithValue("@Age", std.Age);
                queryRun.Parameters.AddWithValue("@Address", std.Address);

                con.Open();
                queryRun.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch(SqlException er)
            {
                ViewBag.error = er.Message;
                return View();
            }
        }

    }
}
