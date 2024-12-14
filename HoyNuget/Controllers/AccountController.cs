using HoyNuget.API.Models.AccountDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HoyNuget.API.Controllers
{
    [ApiController]
    public class AccountController(IConfiguration configuration) : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // شبیه‌سازی کاربر از دیتابیس
            if (request.Username == "admin" && request.Password == "password")
            {
                var token = GenerateJwtToken(request.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            // چک کردن Refresh Token در دیتابیس
            if (IsValidRefreshToken(refreshToken))
            {
                var newToken = GenerateJwtToken("username");
                return Ok(new { Token = newToken });
            }

            return Unauthorized("Invalid refresh token");
        }

        private bool IsValidRefreshToken(string token)
        {
            // بررسی وجود در دیتابیس
            return true;
        }


        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["BearerTokens:Issuer"],
                audience: configuration["BearerTokens:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
