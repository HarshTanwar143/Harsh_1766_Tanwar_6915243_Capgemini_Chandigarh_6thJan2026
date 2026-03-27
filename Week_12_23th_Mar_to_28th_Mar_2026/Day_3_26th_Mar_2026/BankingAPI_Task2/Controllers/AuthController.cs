using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingAPI_Task2.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Login with username/password → returns JWT token.
        /// Use: john / pass123  (userId = user1)
        ///      jane / pass456  (userId = user2, short account number edge case)
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Demo users
            var users = new Dictionary<string, (string password, string userId)>
            {
                { "john", ("pass123", "user1") },
                { "jane", ("pass456", "user2") }
            };

            if (!users.TryGetValue(request.Username, out var userInfo)
                || userInfo.password != request.Password)
                return Unauthorized("Invalid credentials");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.userId),
                new Claim(ClaimTypes.Name, request.Username)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
