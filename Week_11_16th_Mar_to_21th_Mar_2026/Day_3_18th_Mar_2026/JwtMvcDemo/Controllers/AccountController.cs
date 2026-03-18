using JwtMvcDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtMvcDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, go directly to dashboard
            if (HttpContext.Session.GetString("JWToken") != null)
                return RedirectToAction("Dashboard");

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(User user)
        {
            // ✅ Hardcoded credentials (admin/1234)
            if (user.Username == "admin" && user.Password == "1234")
            {
                // ✅ Generate JWT Token
                var token = GenerateToken(user);

                // ✅ Store token in Session
                HttpContext.Session.SetString("JWToken", token);

                return RedirectToAction("Dashboard");
            }

            ViewBag.Message = "❌ Invalid username or password!";
            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(User user)
        {
            // ✅ For demo: just show success and redirect to login
            // In real app: save user to DB with hashed password
            TempData["Success"] = $"Account created for '{user.Username}'! Please login.";
            return RedirectToAction("Login");
        }

        // GET: /Account/Dashboard  (Protected Page)
        public IActionResult Dashboard()
        {
            // ✅ Check if JWT token exists in session
            var token = HttpContext.Session.GetString("JWToken");

            if (token == null)
            {
                // Not logged in → redirect to login
                return RedirectToAction("Login");
            }

            // ✅ Decode token to get username and expiry
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var username = jwtToken.Claims
                                .FirstOrDefault(c => c.Type == ClaimTypes.Name
                                                  || c.Type == "unique_name")
                                ?.Value;
            var expiry = jwtToken.ValidTo.ToLocalTime();

            ViewBag.Username = username;
            ViewBag.Expiry = expiry.ToString("dd MMM yyyy hh:mm tt");
            ViewBag.Token = token;

            return View();
        }

        // POST: /Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            // ✅ Clear JWT token from session
            HttpContext.Session.Remove("JWToken");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ✅ Private method to generate JWT token
        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key,
                            SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}