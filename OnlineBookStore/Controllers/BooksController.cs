using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using OnlineBookStore.Models;
using System.Data.SqlClient;

namespace OnlineBookStore.Controllers
{ 

    public class BooksController : Controller
    {

        private readonly IConfiguration _config;
        public String con;

        public BooksController(IConfiguration config)
        {
            _config = config;
            con = _config.GetConnectionString("dbCon");
        }


        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book b)
        {
            SqlConnection conn = new SqlConnection(con);
            string query = "INSERT INTO Books (author ,title ,price , category , stock ,imageUrl)VALUES" +
                "(@t , @a , @p ,@c , @s , @i)";

            SqlCommand queryRun = new SqlCommand(query , conn);
            conn.Open();

            queryRun.Parameters.AddWithValue("@t" , b.title);
            queryRun.Parameters.AddWithValue("@a", b.author);
            queryRun.Parameters.AddWithValue("@p", b.price);
            queryRun.Parameters.AddWithValue("@c", b.category);
            queryRun.Parameters.AddWithValue("@s", b.stock);
            queryRun.Parameters.AddWithValue("@i", b.imageUrl);

            queryRun.ExecuteNonQuery();
            return RedirectToAction("Read");
        }

        //Register
        [HttpGet]
        public IActionResult Register() {
            return View();
        }


        [HttpPost]
        public IActionResult Register(Register reg)
        {
            using SqlConnection conn = new SqlConnection(con);
            conn.Open();

            // 1. check email
            string checkQuery = "SELECT COUNT(*) FROM users WHERE email = @e";
            SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
            checkCmd.Parameters.AddWithValue("@e", reg.email);

            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                ModelState.AddModelError("email", "Email already exists!");
                return View(reg);
            }

            // 2. insert user
            string query = "INSERT INTO users(name, email, passwords, role) VALUES(@n, @e, @p, @r)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@n", reg.name);
            cmd.Parameters.AddWithValue("@e", reg.email);
            cmd.Parameters.AddWithValue("@r", "User");

            string hashed = BCrypt.Net.BCrypt.HashPassword(reg.passwords);
            cmd.Parameters.AddWithValue("@p", hashed);

            cmd.ExecuteNonQuery();

            TempData["Register"] = "Ok";

            return RedirectToAction("Register");
        }
        //Login
        [HttpGet]
        public IActionResult Login() { 
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login log)
        {
            using SqlConnection conn = new SqlConnection(con);

            string query = "SELECT * FROM users WHERE email = @e";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@e", log.email);

            Console.WriteLine("Email" + log.email);
            conn.Open();
            var reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                ViewBag.error = "User not found";
                return View();
            }

          
            string dbPass = reader["passwords"].ToString();


            if (log == null)
            {
                return Content("log is null");
            }

            if (string.IsNullOrEmpty(log.email))
            {
                return Content("email is null");
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(log.passwords, dbPass);

            if (!isValid)
            {
                ViewBag.error = "Invalid password";
                return View();
            }

            Console.WriteLine("DB PASS: " + dbPass);
            Console.WriteLine("INPUT PASS: " + log.passwords);

            HttpContext.Session.SetInt32("userId", Convert.ToInt32(reader["userId"]));
            HttpContext.Session.SetString("email", reader["email"].ToString());
            HttpContext.Session.SetString("names", reader["name"].ToString());
            HttpContext.Session.SetString("role", reader["role"].ToString());

            TempData["Login"] = "Login Successful";

            string role = reader["role"].ToString();

          
            if (role == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Dashboard", "Books");
            }
            Console.WriteLine(reader["role"]);

            return View();
        }

        //Logout
        public IActionResult Logout()
        {
            TempData["Logout"] = "ok";
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        
        //Read
        public IActionResult Read() {
            try
            {
             List<Book> books = new List<Book>();
            SqlConnection conn = new SqlConnection(con);
            string query = "SELECT * FROM Books";
            SqlCommand queryRun = new SqlCommand(query, conn);
            conn.Open();

            var fetch = queryRun.ExecuteReader();

            while (fetch.Read()) {

                    books.Add(new Book
                    {
                        bookId = Convert.ToInt32(fetch["bookId"]),
                        title = fetch["title"].ToString(),
                        author = fetch["author"].ToString(),
                        price = Convert.ToInt32(fetch["price"]),
                        category = fetch["category"].ToString(),
                        stock = Convert.ToInt32(fetch["stock"]),
                        imageUrl = fetch["imageUrl"].ToString(),


                    });

            }
                return View(books);
            }
            catch (SqlException error)
            {

                ViewBag.error = error.Message;
                return View();
            }

        }

        //Edit
        public IActionResult Edit(int bookId) {
            Book book = null;

            using (SqlConnection conn = new SqlConnection(con))
            {
                string query = "SELECT * FROM Books WHERE bookId = @bookid";
                SqlCommand queryRun = new SqlCommand(query , conn);
                queryRun.Parameters.AddWithValue("@bookid" , bookId);
                conn.Open();

                var fetch = queryRun.ExecuteReader();

                if (fetch.Read()) {
                    book = new Book
                    {
                        bookId = Convert.ToInt32(fetch["bookId"]),
                        title = fetch["title"].ToString(),
                        author = fetch["author"].ToString(),
                        price = Convert.ToInt32(fetch["price"]),
                        category = fetch["category"].ToString(),
                        stock = Convert.ToInt32(fetch["stock"]),
                        imageUrl = fetch["imageUrl"].ToString(),
                    };

                }

            }

                return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book b)
        {
            using (SqlConnection conn = new SqlConnection(con)) {

              String query = "UPDATE Books SET author=@a, title=@t, price=@p, category=@c, stock=@s, imageUrl=@i WHERE bookId=@bookid";
  
                SqlCommand queryRun = new SqlCommand(query, conn);

                queryRun.Parameters.AddWithValue("@bookid", b.bookId);
                queryRun.Parameters.AddWithValue("@t", b.title);
                queryRun.Parameters.AddWithValue("@a", b.author);
                queryRun.Parameters.AddWithValue("@p", b.price);
                queryRun.Parameters.AddWithValue("@c", b.category);
                queryRun.Parameters.AddWithValue("@s", b.stock);
                queryRun.Parameters.AddWithValue("@i", b.imageUrl);

                conn.Open();
                queryRun.ExecuteNonQuery();
            }
               

            return RedirectToAction("Read");
        }

        //Delete
        public IActionResult Delete(int bookId) {

            Book book = null;

            using (SqlConnection conn = new SqlConnection(con))
            {
                string query = "SELECT * FROM Books WHERE bookId = @bookid";
                SqlCommand queryRun = new SqlCommand(query , conn);
                queryRun.Parameters.AddWithValue("@bookid", bookId);
                conn.Open();

                var fetch = queryRun.ExecuteReader();

                if (fetch.Read())
                {
                    book = new Book
                    {
                        bookId = Convert.ToInt32(fetch["bookId"]),
                        title = fetch["title"].ToString(),
                        author = fetch["author"].ToString(),
                        price = Convert.ToInt32(fetch["price"]),
                        category = fetch["category"].ToString(),
                        stock = Convert.ToInt32(fetch["stock"]),
                        imageUrl = fetch["imageUrl"].ToString(),
                    };

                }
            }


            if (book == null)
            {
                Console.WriteLine("Not Found");
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int bookId)
        {
            try {
                using (SqlConnection conn = new SqlConnection(con)) {
                    string query = "DELETE FROM Books WHERE bookId = @bookid";
                    SqlCommand queryRun = new SqlCommand(query, conn);
                    queryRun.Parameters.AddWithValue("@bookid" , bookId);

                    conn.Open();
                    queryRun.ExecuteNonQuery();
                }
                return RedirectToAction("Read");
            }
            catch 
            {
                return View();
            }
            
        }

        //Shop
        public IActionResult Shop(string search, string category, string sort)
        {
            List<Book> books = new List<Book>();

            using SqlConnection conn = new SqlConnection(con);
            conn.Open();


            string query = "SELECT * FROM Books WHERE 1=1";

            if (!string.IsNullOrEmpty(search))
                query += " AND title LIKE @search";

            if (!string.IsNullOrEmpty(category))
                query += " AND category = @category";

            if (sort == "low")
                query += " ORDER BY price ASC";
            else if (sort == "high")
                query += " ORDER BY price DESC";

            SqlCommand cmd = new SqlCommand(query, conn);

            if (!string.IsNullOrEmpty(search))
                cmd.Parameters.AddWithValue("@search", "%" + search + "%");

            if (!string.IsNullOrEmpty(category))
                cmd.Parameters.AddWithValue("@category", category);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                books.Add(new Book
                {
                    bookId = Convert.ToInt32(reader["bookId"]),
                    title = reader["title"].ToString(),
                    author = reader["author"].ToString(),
                    price = Convert.ToInt32(reader["price"]),
                    category = reader["category"].ToString(),
                    stock = Convert.ToInt32(reader["stock"]),
                    imageUrl = reader["imageUrl"].ToString(),
                });
            }
            if (books.Count == 0)
            {
                ViewBag.Message = "No books found for your search/filter.";
            }

            return View(books);
        }

        //Add to cart
        public IActionResult AddToCart(int bookId)
        {
            // Step 1: Get cart session
            var cartSession = HttpContext.Session.GetString("cart");

            List<CartItem> cartItems;

            // Step 2: If empty → new list create
            if (string.IsNullOrEmpty(cartSession))
            {
                cartItems = new List<CartItem>();
            }
            else
            {
                cartItems = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(cartSession);
            }

            // Step 3: Check if book already exists
            var existing = cartItems.FirstOrDefault(c => c.BookId == bookId);

            if (existing == null)
            {
                cartItems.Add(new CartItem
                {
                    BookId = bookId,
                    Quantity = 1
                });
            }
            else
            {
                existing.Quantity++;
            }

            // Step 4: Save back to session
            HttpContext.Session.SetString("cart",
                System.Text.Json.JsonSerializer.Serialize(cartItems));

            return RedirectToAction("Cart");
        }
        //Cart
        public IActionResult Cart()
        {
            List<CartViewModel> cartBooks = new List<CartViewModel>();

            var cartSession = HttpContext.Session.GetString("cart");

            if (cartSession != null)
            {
                List<CartItem> cartItems =
                    System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(cartSession);

                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    foreach (var item in cartItems)
                    {
                        string query = "SELECT * FROM Books WHERE bookId = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", item.BookId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cartBooks.Add(new CartViewModel
                                {
                                    bookId = item.BookId,
                                    Title = reader["title"].ToString(),
                                    imageUrl = reader["imageUrl"].ToString(),
                                    price = Convert.ToInt32(reader["price"]),
                                    Quantity = item.Quantity,
                                  
                                });
                            }
                        }
                    }
                }
            }

            return View(cartBooks);
        }

        //increase Quantity
        public IActionResult Increase(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.BookId == id);
            if (item != null) item.Quantity++;
            SaveCart(cart);

            return RedirectToAction("Cart");
        }

        //Decrease Quantity 
        public IActionResult Decrease(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.BookId == id);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity == 0)
                    cart.Remove(item);
            }
            SaveCart(cart);

            return RedirectToAction("Cart");
        }

        //remove from cart
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.BookId == id);
            if (item != null) cart.Remove(item);

            SaveCart(cart);

            return RedirectToAction("Cart");
        }

        //helper method
        private List<CartItem> GetCart()
        {
            var cartSession = HttpContext.Session.GetString("cart");
            if (cartSession == null)
                return new List<CartItem>();

            return System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(cartSession);
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString("cart",
                System.Text.Json.JsonSerializer.Serialize(cart));
        }

        //checkout  
        public IActionResult Checkout() {
            var cartSession = HttpContext.Session.GetString("cart");

            if (cartSession == null) {
                return RedirectToAction("cart");
            }

            List<CartItem> cartItems = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(cartSession);

            int total = 0;

            using (SqlConnection conn = new SqlConnection(con)) {
                conn.Open();

                foreach (var item in cartItems) {
                    string query = "SELECT price From Books Where bookId = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", item.BookId);

                    var price = (int)cmd.ExecuteScalar();
                    total += price * item.Quantity;
                }
            }

            ViewBag.Total = total;
             
            return View();
        }

        //checkout  
        [HttpPost]
        public IActionResult CheckoutConfirm()
        {
            string email = HttpContext.Session.GetString("email");
            string names = HttpContext.Session.GetString("names");

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var cartSession = HttpContext.Session.GetString("cart");

            if (string.IsNullOrEmpty(cartSession))
                return RedirectToAction("Cart");

            List<CartItem> cart = System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(cartSession);

            int total = 0;

            SqlConnection conn = new SqlConnection(con);
            conn.Open();

            // calculate total
            foreach (var item in cart)
            {
                string q = "SELECT price FROM Books WHERE bookId = @id";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@id", item.BookId);

                int price = (int)cmd.ExecuteScalar();
                total += price * item.Quantity;
            }

            // insert order
            string orderSql = "INSERT INTO Orders (names, Email, TotalAmount) OUTPUT INSERTED.OrderId VALUES (@n ,@e, @t)";
            SqlCommand ordercmd = new SqlCommand(orderSql, conn);
            ordercmd.Parameters.AddWithValue("@n", names);
            ordercmd.Parameters.AddWithValue("@e", email);
            ordercmd.Parameters.AddWithValue("@t", total);

            int orderId = (int)ordercmd.ExecuteScalar();

            foreach (var item in cart)
            {
                string q = "SELECT price FROM Books WHERE bookId = @id";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@id", item.BookId);

                int price = (int)cmd.ExecuteScalar();  

                string itemSql = "INSERT INTO OrderItems (OrderId, BookId, Quantity, Price) VALUES (@o, @b, @q, @p)";
                SqlCommand itemCmd = new SqlCommand(itemSql, conn);

                itemCmd.Parameters.AddWithValue("@o", orderId);
                itemCmd.Parameters.AddWithValue("@b", item.BookId);
                itemCmd.Parameters.AddWithValue("@q", item.Quantity);
                itemCmd.Parameters.AddWithValue("@p", item.Quantity * price); 

                itemCmd.ExecuteNonQuery();
            }

            HttpContext.Session.Remove("cart");

            TempData["Success"] = "Order placed successfully!";
            return RedirectToAction("MyOrders");
        }

        //MyOrders
        public IActionResult MyOrders()
        {
            string email = HttpContext.Session.GetString("email");
            if (email == null) return RedirectToAction("Login");

            List<Order> orders = new List<Order>();

            SqlConnection conn = new SqlConnection(con);
            conn.Open();

            string query = "SELECT * FROM Orders WHERE Email = @e ORDER BY OrderDate DESC";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@e", email);

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

        //OrderDetails
        public IActionResult OrderDetails(int id)
        {
            List<OrderItemViewModel> items = new List<OrderItemViewModel>();

            using SqlConnection conn = new SqlConnection(con);
            conn.Open();

            string sql = @"SELECT b.title AS Title, oi.Quantity, oi.Price 
                     FROM OrderItems oi INNER
                     JOIN Books b ON oi.BookId = b.BookId
                     WHERE oi.OrderId = @id";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                items.Add(new OrderItemViewModel
                {
                    Title = rd["title"].ToString(),
                    Quantity = Convert.ToInt32(rd["Quantity"]),
                    Price = Convert.ToInt32(rd["Price"])
                });
            }

            return View(items);
        }

        //Dashboard
        public IActionResult Dashboard()
        {

            if (TempData["Login"] != null)
            {
                ViewBag.LoginSuccess = true;
            }
            var uid = HttpContext.Session.GetInt32("userId");
            var names = HttpContext.Session.GetString("names");  

            if (uid == null || names == null)
                return RedirectToAction("Login");


            using SqlConnection conn = new SqlConnection(con);
            conn.Open();

            //Total orders
            SqlCommand totalCmd = new SqlCommand(
             "SELECT COUNT(*) FROM Orders WHERE names=@n", conn);

            totalCmd.Parameters.AddWithValue("@n", names);
             int totalOrders = (int)totalCmd.ExecuteScalar();


            SqlCommand lastCmd = new SqlCommand(
             "SELECT TOP 1 OrderDate FROM Orders WHERE names=@n ORDER BY OrderDate DESC", conn);

            lastCmd.Parameters.AddWithValue("@n", names);


            object last = lastCmd.ExecuteScalar();
            string lastDate = last != null ? last.ToString() : "No Orders Yet";

            Dashboard d = new Dashboard()
            {
                names = names,
                TotalOrders = totalOrders,
                LastOrderDate = lastDate
            };

            return View(d);
        }

        //print invoice
        public IActionResult Invoice(int id) {
            OrderInvoice invoice = new OrderInvoice();
            List<OrderItemViewModel> items = new List<OrderItemViewModel>();

            using SqlConnection conn = new SqlConnection(con);
            conn.Open();

            //order info
            string orderquery = "SELECT * FROM Orders Where OrderId = @id";
            SqlCommand ordercmd = new SqlCommand(orderquery, conn);
            ordercmd.Parameters.AddWithValue("@id", id);

            SqlDataReader rd = ordercmd.ExecuteReader();

            if (rd.Read()) {
                invoice.OrderId = (int)rd["OrderId"];
                invoice.Email = rd["Email"].ToString();
                invoice.TotalAmount = Convert.ToInt32(rd["TotalAmount"]);
                invoice.OrderDate = Convert.ToDateTime(rd["OrderDate"]);
            }
            rd.Close();

            //Order Items 
            string itemQuery = @"SELECT b.title AS Title , oi.Quantity , oi.Price FROM OrderItems oi JOIN Books b ON oi.BookId = b.BookId WHERE oi.OrderId = @id";

            SqlCommand cmd = new SqlCommand(itemQuery, conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read()) {
                items.Add(new OrderItemViewModel
                {
                    Title = rdr["Title"].ToString(),
                    Quantity = Convert.ToInt32(rdr["Quantity"]),
                    Price = Convert.ToInt32(rdr["Price"])
                });
            }
            invoice.Items = items;

            return View(invoice);
        }
    
    }
}

