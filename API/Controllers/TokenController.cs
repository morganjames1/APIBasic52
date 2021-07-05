using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryService.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly AccountRepository repository;

        public IConfiguration configuration;
        private readonly MyContext myContext;

        public TokenController(IConfiguration config, MyContext context)
        {
            this.configuration = config;
            this.myContext = context;
        
        }

        public static bool ValidatePassword(string password, string corectHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, corectHash);
        }

        [HttpPost]
        public IActionResult Post(LoginVM loginVM)
        {
                
                var jwt = myContext.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault<Employee>();
                if (jwt != null)
                // mencocokkan email yg ada dengan nik
                {

                    var cekEmail = myContext.Employees.FirstOrDefault(c => c.Email == loginVM.Email);
                    var user = myContext.Accounts.Find(cekEmail.NIK);

                if (user != null && ValidatePassword(loginVM.Password, user.Password))
                {

                    //create claims details based on the user information
                    var email = myContext.Employees.Find(user.NIK);
                    var role = myContext.AccountRoles.FirstOrDefault(a => a.NIK == user.NIK);
                    var find = myContext.Roles.FirstOrDefault(a => a.RoleId == role.RoleId);

                    var claims = new[] {

                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", email.Email),
                        new Claim("role", find.RoleName)
                        //new Claim("Nama", email.FirstName),

                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"],
                        claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    var show = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new { status = HttpStatusCode.OK, nik = user.NIK, token = show });

                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }        
    }
}