using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PixelTown.Models;
using PixelTown.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PixelTown.Controllers
{
    public class AuthenticationController : ControllerBase
    {

        public IConfiguration _configuration;

        public AuthenticationController(IConfiguration config)
        {
            _configuration = config;
        }

        // POST api/<AuthenticationController>
        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] Account acc)
        {
            var message = "";
            if(acc.Email == "" || acc.Email == null)
            {
                message = "Please enter email!";
            } else if (acc.Password == "" || acc.Password == null)
            {
                message = "Please enter password!";
            }
            if (message == "")
            {
                var account = AuthenticationRes.Login(acc.Email, acc.Password);
                if (account == null)
                {
                    return Unauthorized(new JsonResult("Username or password incorrect. Please check again!"));
                }
                else
                {
                    var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Role, account.Type),
                new Claim(ClaimTypes.Name, account.Id)
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        account = new JsonResult(account)
                    });
                }
            } else
            {
                return BadRequest(new JsonResult(message));
            }
        }

        [HttpPost]
        [Route("api/register")]
        public IActionResult Register([FromForm] string name, string email, string password, string birthday, string address)
        {
            var message = "";
            if(name == "" || name == null)
            {
                message = "Please enter full name!";
            } else if (email == "" || email == null)
            {
                message = "Please enter email!";
            } else if (password == "" || password == null)
            {
                message = "Please enter password!";
            } else if (password.Length < 6)
            {
                message = "Password not less than 6 characters!";
            }

            if (message == "")
            {
                DateTime date = DateTime.ParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var account = AuthenticationRes.Register(name, email, password, date, address);

                if (account)
                {
                    return Ok(new JsonResult("Register Successfully"));
                }
                else
                {
                    return BadRequest(new JsonResult("Register Failed"));
                }
            } else
            {
                return BadRequest(new JsonResult(message));
            }    
        }

        [HttpPost]
        [Authorize]
        [Route("api/logout")]
        public IActionResult Logout()
        {
            return Ok(new JsonResult("Logout Success"));
        }
    }
}
