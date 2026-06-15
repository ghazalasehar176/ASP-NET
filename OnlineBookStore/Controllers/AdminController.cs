using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
using System.Data.SqlClient;


namespace OnlineBookStore.Controllers
{


    public class AdminController : Controller
    {
        private readonly IConfiguration _config;
        public string con;

        public AdminController(IConfiguration config)
        {
            _config = config;
            con = _config.GetConnectionString("dbCon");
        }

        // Dashboard
        public IActionResult Index()
        {
            string role = HttpContext.Session.GetString("role");

            if(role != "Admin")
            {
                return RedirectToAction("Login", "Books");
            }

            return View();
        }

        // BOOKS (CRUD)
        public IActionResult Books()
        {
            List<Book> books = new List<Book>();

            using SqlConnection conn = new SqlConnection(con);
            conn.Open();

            string query = "SELECT * FROM Books";
            SqlCommand cmd = new SqlCommand(query, conn);
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                books.Add(new Book
                {
                    bookId = (int)rd["bookId"],
                    title = rd["title"].ToString(),
                    author = rd["author"].ToString(),
                    price = (int)rd["price"],
                    category = rd["category"].ToString(),
                    stock = (int)rd["stock"],
                    imageUrl = rd["imageUrl"].ToString()
                });
            }

            return View(books);
        }

        // USERS
        public IActionResult Users()
        {
            List<Register> users = new List<Register>();

            using SqlConnection conn = new SqlConnection(con);
            conn.Open();

            string query = "SELECT * FROM users";
            SqlCommand cmd = new SqlCommand(query, conn);

            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                users.Add(new Register
                {
                    name = rd["name"].ToString(),
                    email = rd["email"].ToString()
                });
            }

            return View(users);
        }

        // ORDERS
        public IActionResult Orders()
        {
            List<Order> orders = new List<Order>();

            using SqlConnection conn = new SqlConnection(con);
            conn.Open();

            string query = "SELECT * FROM Orders ORDER BY OrderDate DESC";
            SqlCommand cmd = new SqlCommand(query, conn);

            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                orders.Add(new Order
                {
                    OrderId = (int)rd["OrderId"],
                    Email = rd["Email"].ToString(),
                    TotalAmount = (int)rd["TotalAmount"],
                    OrderDate = Convert.ToDateTime(rd["OrderDate"])
                });
            }

            return View(orders);
        }
    }
}
