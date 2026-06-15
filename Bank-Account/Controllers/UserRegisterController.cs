using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Bank_Account.Models;
using Bank_Account.Services;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.RegularExpressions;


namespace Bank_Account.Controllers
{
    public class UserRegisterController : Controller
    {
    
        private readonly DbService _db;
        public UserRegisterController(DbService db) {
            _db = db;
        }


        public IActionResult AboutUs() {
            return View();
        }
        public void ClearForm(RegisterViewModel userReg)
        {
            userReg.fullname = "";
            userReg.dob = default;
            userReg.cnicnum = "";
            userReg.phonenum = "";
            userReg.email = "";
            userReg.accounttype = "";
            userReg.username = "";
            userReg.password = "";
            userReg.otpcode = "";
            userReg.otpexpiry = default;
            userReg.agreeterm = false;

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel userReg)
        {
            try
            {

                //  Server side validadtion
                if (!Regex.IsMatch(userReg.cnicnum, @"^\d{5}-\d{7}-\d{1}$"))
                {
                    ViewBag.Error = "Invalid CNIC format!";
                    return View(userReg);
                }

                if (!Regex.IsMatch(userReg.phonenum, @"^03\d{9}$"))
                {
                    ViewBag.Error = "Invalid phone number!";
                    return View(userReg);
                }

                if (!Regex.IsMatch(userReg.email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {
                    ViewBag.Error = "Invalid email format!";
                    return View(userReg);
                }


                if (!Regex.IsMatch(userReg.username, @"^[A-Za-z]+$"))
                {
                    ViewBag.Error = "Username must contain only letters!";
                    return View(userReg);
                }
                if (string.IsNullOrWhiteSpace(userReg.password) || userReg.password.Length < 6)
                {
                    ViewBag.Error = "Password is required and must be at least 6 characters!";
                    return View(userReg);
                }





                //OTP code Generated
                Random rnd = new Random();
                userReg.otpcode = rnd.Next(1000, 9009).ToString();

                //OTP code expire
                userReg.otpexpiry = DateTime.Now.AddMinutes(5);


                //Email Send
                new EmailService().sendEmail(
                        userReg.email,
                        "Your OTP Code ",
                        $"Your OTP Code Is: {userReg.otpcode}. It will expire in 5 minutes. "
                    );

                //password hashing 
                string HashedPass = BCrypt.Net.BCrypt.HashPassword(userReg.password);

                int count = _db.ExecuteScaler(
                         "SELECT COUNT(*) FROM userReg WHERE email = @e OR username = @u",
                         new[]
                         {
                            new SqlParameter("@e", userReg.email),
                            new SqlParameter("@u", userReg.username)
                         }
                        );

         
                    if (count > 0)
                    {

                        ViewBag.Error = "Email Or Password already Exist";
                        return View();
                    }

                //Insert User
                _db.ExecuteNonQuery(
                        "INSERT INTO UserReg(FullName , dob , cnicNum , phoneNum , email , AccountType , userName , password , otpCode , otpExpiry , agreeTerm)VALUES" +
                        "(@fn , @dob , @cn , @pn , @e, @at , @un , @p , @oc , @oe , @aterm)",
                        new[]
                        {
                            new SqlParameter("@fn", userReg.fullname),
                            new SqlParameter("@dob", userReg.dob),
                            new SqlParameter("@cn", userReg.cnicnum),
                            new SqlParameter("@pn", userReg.phonenum),
                            new SqlParameter("@e", userReg.email),
                            new SqlParameter("@at", userReg.accounttype),
                            new SqlParameter("@un", userReg.username),
                            new SqlParameter("@p", HashedPass),
                            new SqlParameter("@oc", userReg.otpcode),
                            new SqlParameter("@oe", userReg.otpexpiry),
                            new SqlParameter("@aterm", userReg.agreeterm)
                        }
                    );


                ModelState.Clear();
                ClearForm(userReg);

                TempData["Message"] = "User Registered Successfully! OTP sent to your email";
                return RedirectToAction("OTPVerify", new { email = userReg.email });
            }

            catch (Exception e) 
            {
                ModelState.Clear();
                ClearForm(userReg);
                ViewBag.Error = "Error Occurred" + e.Message;
            
            }
            return View(userReg);

        }


        [HttpGet]
        public IActionResult Login() {

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel userReg)
        {
            var reader = _db.ExecuteReader(
              "SELECT password, isVerified , loginAttempts , lockTime FROM userReg WHERE userName = @u",
              new[]
              {
                  new SqlParameter("@u", userReg.username)
              }
            );

            if (!reader.Read())
            {
                ViewBag.Error = "Invalid username";
                return View();
            }

            string hashedPass = reader["password"].ToString();
            int loginAttempts = Convert.ToInt32(reader["loginAttempts"]);

            DateTime lockTime;
            if (reader["lockTime"] == DBNull.Value)
            {
                lockTime = DateTime.MinValue;
            }
            else
            {
                lockTime = Convert.ToDateTime(reader["lockTime"]);
            }

            //check if account is locked 
            if (lockTime != DateTime.MinValue && lockTime > DateTime.Now)
            {
                ViewBag.Error = $"Account locked! Try again after {lockTime}.";
                return View();
            }


            // Hash Password
            if (!BCrypt.Net.BCrypt.Verify(userReg.password, hashedPass))
            {
                loginAttempts++;
                SqlParameter[] param =
                    {
                        new SqlParameter("@attempts" , loginAttempts),
                        new SqlParameter("@username" , userReg.username)
                };


                if (loginAttempts >= 3)
                {
                    //lock account for 15 min
                    _db.ExecuteNonQuery(
                        "UPDATE userReg SET loginAttempts = @attempts , lockTime = DATEADD(MINUTE , 3, GETDATE()) WHERE userName = @username",
                        param
                        );
                    ViewBag.Error = "Account locked! Too many attempts. Try again after 3 minutes.";
                }
                else
                {
                    //update attempts only 
                    _db.ExecuteNonQuery(
                          "UPDATE userReg SET loginAttempts = @attempts WHERE userName = @username",
                        param
                        );
                    ViewBag.Error = $"Invalid password! {3 - loginAttempts} attempts left.";
                }
                return View();
            }

            //Success login attempt and lockedTime
            _db.ExecuteNonQuery(
                      "UPDATE userReg SET loginAttempts = 0 , lockTime = NULL WHERE userName = @username",
                        new[] { new SqlParameter ("@username" , userReg.username)}
                      );


            //Login session
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userReg.username) };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync("CookieAuth", principal);
            HttpContext.Session.SetString("username", userReg.username);

            TempData["Success"] = "Login successful! Welcome back.";
            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult OTPverify()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OTPverify(OTPverifyModel otp)
        {
            var reader = _db.ExecuteReader(
                "SELECT otpCode, otpExpiry FROM userReg WHERE email = @e",
                new[]
                {
            new SqlParameter("@e", otp.email)
                }
            );

            if (!reader.Read())
            {
                ViewBag.Error = "Email not found";
                return View();
            }

            string correctOtp = reader["otpCode"].ToString();
            DateTime expiry = Convert.ToDateTime(reader["otpExpiry"]);

            if (DateTime.Now > expiry)
            {
                ViewBag.Error = "OTP Expired!!";
                return View();
            }

            if (otp.enteredOtp != correctOtp)
            {
                ViewBag.Error = "Invalid OTP";
                return View();
            }

            // OTP 
            _db.ExecuteNonQuery(
                "UPDATE userReg SET isVerified = 1 WHERE email = @e",
                new[] { new SqlParameter("@e", otp.email) }
            );

            TempData["Message"] = "OTP verified successfully! You can now login. ";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuth");
            TempData["Success"] = "You have successfully logged out!";
            return RedirectToAction("Login");
        }
    }
}
