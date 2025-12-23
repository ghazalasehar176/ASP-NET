using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using class8.Models;


namespace class8.Controllers
{
    public class StudentController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["testdb"].ConnectionString;
        // GET: Student
        public ActionResult Index()
        {
            List<Student> students = new List<Student>();
            SqlConnection con = new SqlConnection(cs);
            string query = "Select * From Student";
            SqlCommand queryRun = new SqlCommand(query, con);
            con.Open();

            var fetch = queryRun.ExecuteReader();

            while(fetch.Read()){
                students.Add(new Student {
                    Id = Convert.ToInt32(fetch["Id"]),            // int assign
                    Name = fetch["Name"].ToString(),              // string assign
                    Age = Convert.ToInt32(fetch["Age"]),        // int assign
                    Address = fetch["stdAddress"].ToString()      // string assign

                });

            }
            return View(students);
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
                string query  = "Insert into Student (Name , Age , stdAddress)values(@Name , @Age , @Address)";

                SqlCommand queryRun = new SqlCommand(query, con);
                con.Open();
                queryRun.Parameters.AddWithValue("@Name", std.Name);
                queryRun.Parameters.AddWithValue("@Age", std.Age);
                queryRun.Parameters.AddWithValue("@Address", std.Address);

                queryRun.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            Student std = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "Select * From Student Where Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    std = new Student
                    {
                        Id = Convert.ToInt32(reader["Id"]),            // int assign
                        Name = reader["Name"].ToString(),              // string assign
                        Age = Convert.ToInt32(reader["Age"]),        // int assign
                        Address = reader["stdAddress"].ToString()      // string assign

                    };
                }
            }
                return View(std);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(Student std)
        {
            try
            {
               using (SqlConnection con = new SqlConnection(cs))
                {
                    String query = "Update Student Set Name = @Name , Age = @Age , stdAddress = @Address WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Id", std.Id);
                    cmd.Parameters.AddWithValue("@Name", std.Name);
                    cmd.Parameters.AddWithValue("@Age", std.Age);
                    cmd.Parameters.AddWithValue("@Address", std.Address);

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
                ;

                return RedirectToAction("Index");
            }
            catch
            {
                return View(std);
            }
        }

        // GET: Student/Delete/5
        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            Student std = null;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Student WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    std = new Student
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Address = reader["stdAddress"].ToString()
                    };
                }
            }

            if (std == null) return HttpNotFound();
            return View(std);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "DELETE FROM Student WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
