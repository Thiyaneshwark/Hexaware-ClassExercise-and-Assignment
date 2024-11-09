using Assignment_Authentication.Authentication;
using Assignment_Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Assignment_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (await _context.Employees.AnyAsync(e => e.Name == model.Name))
                return Conflict(new Response { Status = "Error", Message = "Employee already exists!" });

            var employee = new Employee
            {
                Name = model.Name,
                Gender = model.Gender,
                Designation = model.Designation,
                Email = model.Email,
                //DepartmentId=model.DepartmentId,
                Password = model.Password,
                Salary = model.Salary
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Employee registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Name == model.Name && e.Password == model.Password);
            if (employee == null)
                return Unauthorized(new Response { Status = "Error", Message = "Invalid credentials!" });

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.Name)
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}



        