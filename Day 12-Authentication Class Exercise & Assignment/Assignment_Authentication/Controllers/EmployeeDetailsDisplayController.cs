using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Assignment_Authentication.Models;
using Assignment_Authentication.Authentication;

namespace Assignment_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailsDisplayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeDetailsDisplayController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [Authorize]  
        [HttpGet("details")]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized(new { message = "User not found in token" });
            }

            //---> Fetch employee details from the database
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Name == userName);

            if (employee == null)
            {
                return NotFound(new { message = "Employee not found" });
            }

            //----> Returning employee details
            return Ok(new
            {
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Gender = employee.Gender,
                Designation = employee.Designation,
                Email = employee.Email,
                Salary = employee.Salary
            });
        }
    }
}
