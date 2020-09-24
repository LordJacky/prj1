using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using prj1.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace prj1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly Prj1DBContext _context;

        public TokenController(IConfiguration config, Prj1DBContext context)
        {
            _configuration = config;
            _context = context;
        }

        
        [HttpPost]
        public async Task<IActionResult> Post(UserData _userData)
        {
            if (_userData != null && _userData.UserEmail != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.UserEmail, _userData.Password);

                if (user != null)
                {
                    var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("UserEmail", user.UserEmail)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                }

                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<UserData> GetUser(string email, string password)
        {
            return await _context.UsersData.FirstOrDefaultAsync(u => u.UserEmail == email && u.Password == password);
        }

    }
}
