using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private const string SecretKey = "SUPER_SECRET_KEY_FOR_JWT_AUTH_12345678";

    [HttpPost("login")]
    public IActionResult Login([FromQuery] string username, [FromQuery] string password)
    {
        if (username != "admin" || password != "1234")
            return Unauthorized(new { message = "Invalid credentials" });

        var key = Encoding.UTF8.GetBytes(SecretKey);

        var token = new JwtSecurityToken(
            claims: new[] { new Claim(ClaimTypes.Name, username) },
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}