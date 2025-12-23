using Microsoft.AspNetCore.Mvc;
using assignment.Models;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;


namespace assignment.Controllers
{
   public class AccountController : Controller
    {
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        public void clearForm(RegisterViewModel user)
        {
            user.Username = "";
            user.Email = "";
            user.Password = "";
            user.Role = "";
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel user)
        {
            try
            {
                //DB connection
                string cs = _config.GetConnectionString("dbcon");

                using (SqlConnection con = new SqlConnection(cs))
                {

                    con.Open();

                    //Check if email or Password already exist
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE Username  = @u OR Email = @e";
                    SqlCommand checkcmd = new SqlCommand(checkQuery , con);
                    checkcmd.Parameters.AddWithValue("@u", user.Username);
                    checkcmd.Parameters.AddWithValue("@e", user.Email);

                    int count = (int)checkcmd.ExecuteScalar();

                    if (count > 0) {
                        ViewBag.Error = "Username or Email already Exist";

                        //Clear form feilds
                        ModelState.Clear();
                        clearForm(user);

                        return View();
                    }



                    //Password Hashing
                    string hashedPass = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    //Insert New User
                    string query = "INSERT INTO Users(Username ,Email, Password , Role) VALUES(@u ,@e, @p , @r)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@u", user.Username);
                    cmd.Parameters.AddWithValue("@e", user.Email);
                    cmd.Parameters.AddWithValue("@p", hashedPass);
                    cmd.Parameters.AddWithValue("@r", user.Role);

                   
                    cmd.ExecuteNonQuery();

                    //Clear form feilds
                    ModelState.Clear();
                    clearForm(user);
                }
                ViewBag.Message = "User Registered SuccessFully";
            }

            catch (Exception ex)
            {
                ViewBag.Error = "Error Occurred" + ex.Message;

                //Clear form feilds
                ModelState.Clear();
                clearForm(user);
            }
            return View(user);
        }


        //Get
        public IActionResult Login()
        {
            return View();
        }

        //Post
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            //DB connection
            string cs = _config.GetConnectionString("dbcon");

            using (SqlConnection con = new SqlConnection(cs)) {

            con.Open();

            string query = "SELECT Password, Role FROM Users WHERE Username = @u";

            SqlCommand queryRun = new SqlCommand(query, con);
            queryRun.Parameters.AddWithValue("@u", model.Username);

            SqlDataReader row = queryRun.ExecuteReader();

            if (row.Read())
            {
                string hashedPass = row["Password"].ToString();
                string role = row["role"].ToString();

                if (BCrypt.Net.BCrypt.Verify(model.Password, hashedPass))
                {
                    var claim = new List<Claim>
                {
                   new Claim(ClaimTypes.Name , model.Username),
                   new Claim(ClaimTypes.Role , role)
                };

                    var identity = new ClaimsIdentity(claim, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync("CookieAuth", principal);
                    HttpContext.Session.SetString("Username",model.Username );
                    return RedirectToAction("Index", "Home");

                }

            }
        }

            ViewBag.Error = "Invalid Credential";
            return View(model);
        }


          public IActionResult Logout()
            {
              HttpContext.SignOutAsync("CookieAuth");
              return RedirectToAction("login");
        }
        }
    }

