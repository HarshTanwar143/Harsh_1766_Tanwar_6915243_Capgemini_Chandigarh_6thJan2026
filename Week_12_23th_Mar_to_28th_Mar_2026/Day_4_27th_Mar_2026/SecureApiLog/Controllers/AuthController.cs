using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(AuthController));
    private const string SecretKey = "SUPER_SECRET_KEY_FOR_JWT_AUTH_12345678";

    [HttpPost("login")]
    public IActionResult Login([FromQuery] string username, [FromQuery] string password)
    {
        try
        {
            log.Info($"Login attempt for user: {username}");

            if (username != "admin" || password != "1234")
            {
                log.Warn($"Failed login attempt for user: {username}");
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var key = Encoding.UTF8.GetBytes(SecretKey);
            var token = new JwtSecurityToken(
                claims: new[] { new Claim(ClaimTypes.Name, username) },
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            log.Info($"Token generated for user: {username}");

            return Ok(new { token = tokenString });
        }
        catch (Exception ex)
        {
            log.Error("Exception during login", ex);
            return StatusCode(500, new { message = "Login failed due to server error" });
        }
    }
}
