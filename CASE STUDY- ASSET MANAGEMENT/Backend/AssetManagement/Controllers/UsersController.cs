using AssetManagement.Interface;
using AssetManagement.Models;
using AssetManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly DataContext _context;

        public UsersController(DataContext context, IUserRepository userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepo.GetAllUser();
            return Ok(users);  // Wrap the result in Ok() to return a 200 response
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepo.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (id != user.UserId)
            {
                return BadRequest();
            }

            if (userRole != "Admin")
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (id != userId)
                {
                    return Forbid();
                }
            }

            _userRepo.UpdateUser(user);

            try
            {
                await _userRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            
            _userRepo.AddUser(user);
            await _userRepo.Save();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepo.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                await _userRepo.DeleteUser(id);
                await _userRepo.Save();
            }
            catch (Exception)
            {
                return NotFound("Error deleting the user");
            }

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
