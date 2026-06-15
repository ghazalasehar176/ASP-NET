using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using BCrypt.Net;
using System.Configuration;
using System.Data;
using System.Transactions;
using Bank_Account.Models;


namespace Bank_Account.Controllers
{
    
    public class TransactionsController : Controller
    {
        string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Bank;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;";
        public ActionResult Last(int n = 3)
        {
            List<TransactionModel> list = new List<TransactionModel>();

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(
                "SELECT TOP(@n) * FROM Transactions ORDER BY TranDate DESC", con);
            cmd.Parameters.AddWithValue("@n", n);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new TransactionModel
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    TranDate = dr["TranDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TranDate"]),
                    Description = dr["Description"].ToString(),
                    Amount = Convert.ToInt32(dr["Amount"]),
                    TranType = dr["TranType"].ToString()
                });
            }


            return View(list);
        }
        public ActionResult Report()
        {
            List<TransactionModel> list = new List<TransactionModel>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Transactions", con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new TransactionModel
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    TranDate = dr["TranDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["TranDate"]),
                    Description = dr["Description"].ToString(),
                    Amount = Convert.ToInt32(dr["Amount"]),
                    TranType = dr["TranType"].ToString()
                });
            }
            con.Close();

            return View(list);
        }
        public ActionResult Monthly(int month, int year)
        {
            List<TransactionModel> list = new List<TransactionModel>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Transactions WHERE MONTH(TranDate)=@m AND YEAR(TranDate)=@y", con);

            cmd.Parameters.AddWithValue("@m", month);
            cmd.Parameters.AddWithValue("@y", year);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new TransactionModel
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    TranDate = Convert.ToDateTime(dr["TranDate"]),
                    Description = dr["Description"].ToString(),
                    Amount = Convert.ToDecimal(dr["Amount"]),
                    TranType = dr["TranType"].ToString()
                });
            }
            return View(list);
        }

        public ActionResult Annual(int year)
        {
            List<TransactionModel> list = new List<TransactionModel>();
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Transactions WHERE YEAR(TranDate)=@y", con);

            cmd.Parameters.AddWithValue("@y", year);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new TransactionModel
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    TranDate = Convert.ToDateTime(dr["TranDate"]),
                    Description = dr["Description"].ToString(),
                    Amount = Convert.ToDecimal(dr["Amount"]),
                    TranType = dr["TranType"].ToString()
                });
            }


            return View("Report", list);
        }


    }
}



