using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingAPI_Task3.Controllers
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
        /// Login and receive a JWT with a Role claim embedded.
        ///
        /// Test credentials:
        ///   alice / admin123  → Role: "Admin"  → sees full details
        ///   bob   / user123   → Role: "User"   → sees limited details
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Demo users with roles
            var users = new Dictionary<string, (string password, string userId, string role)>
            {
                { "alice", ("admin123", "admin1", "Admin") },
                { "bob",   ("user123",  "user1",  "User")  }
            };

            if (!users.TryGetValue(request.Username, out var info)
                || info.password != request.Password)
                return Unauthorized("Invalid credentials.");

            // *** KEY: Role is added as a Claim inside the JWT token ***
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, info.userId),
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, info.role)   // ← Role claim embedded here
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

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                role = info.role,   // shown for clarity in testing
                message = $"Logged in as {info.role}"
            });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
