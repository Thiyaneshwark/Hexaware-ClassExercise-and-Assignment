using System.Security.Claims;
using AssetManagement.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceLogsController : ControllerBase
    {
        private readonly IMaintenanceLogRepo _maintenanceLogRepo;

        public MaintenanceLogsController(IMaintenanceLogRepo maintenanceLogRepo)
        {
            _maintenanceLogRepo = maintenanceLogRepo;
        }

        // GET: api/MaintenanceLogs
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MaintenanceLog>>> GetMaintenanceLogs()
        {
            // Get all claims to inspect what user-related information is available.
            var claims = User.Claims;
            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _))?.Value;
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            // Debug: Log all claims to check what is available
            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            // If the NameIdentifier claim is empty or invalid, attempt to find a custom claim for userId
            if (string.IsNullOrEmpty(userIdClaim))
            {
                userIdClaim = User.FindFirstValue("userId");  // Replace with the actual claim type if it's custom
            }

            // Ensure the claim is valid and parse it to an integer
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest(new { message = $"Invalid or missing user ID claim: {userIdClaim}" });
            }

            // Check user role and handle accordingly
            if (userRole == "Admin")
            {
                // Admin can access all logs
                var allLogs = await _maintenanceLogRepo.GetAllMaintenanceLog();
                return Ok(allLogs);
            }

            // Non-admin users can only access their own logs
            var userLogs = await _maintenanceLogRepo.GetMaintenanceLogByUserId(userId);
            if (userLogs == null || !userLogs.Any())  // Ensure proper check for empty logs
            {
                return NotFound(new { message = "No maintenance logs found for the user." });
            }

            return Ok(userLogs);
        }



        // GET: api/MaintenanceLogs/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MaintenanceLog>> GetMaintenanceLog(int id)
        {
            var maintenanceLog = await _maintenanceLogRepo.GetMaintenanceLogById(id);
            if (maintenanceLog == null)
            {
                return NotFound(new { message = $"Maintenance log with ID {id} not found." });
            }

            return Ok(maintenanceLog);
        }

        // POST: api/MaintenanceLogs
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MaintenanceLog>> PostMaintenanceLog(MaintenanceLog maintenanceLog)
        {
            await _maintenanceLogRepo.AddMaintenanceLog(maintenanceLog);
            await _maintenanceLogRepo.Save();

            if (maintenanceLog.MaintenanceId == 0)
            {
                return BadRequest(new { message = "Maintenance ID is not set." });
            }

            return CreatedAtAction(nameof(GetMaintenanceLog), new { id = maintenanceLog.MaintenanceId }, maintenanceLog);
        }

        // PUT: api/MaintenanceLogs/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutMaintenanceLog(int id, MaintenanceLog maintenanceLog)
        {
            if (id != maintenanceLog.MaintenanceId)
            {
                return BadRequest(new { message = "Mismatched maintenance log ID." });
            }

            try
            {
                await _maintenanceLogRepo.UpdateMaintenanceLog(maintenanceLog);
                await _maintenanceLogRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MaintenanceLogExistsAsync(id))
                {
                    return NotFound(new { message = $"Maintenance log with ID {id} not found." });
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/MaintenanceLogs/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMaintenanceLog(int id)
        {
            try
            {
                var log = await _maintenanceLogRepo.GetMaintenanceLogById(id);
                if (log == null)
                {
                    return NotFound(new { message = $"Maintenance log with ID {id} not found." });
                }

                await _maintenanceLogRepo.DeleteMaintenanceLog(id);
                await _maintenanceLogRepo.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error occurred while deleting log: {ex.Message}" });
            }
        }

        // Helper method to check if a maintenance log exists
        private async Task<bool> MaintenanceLogExistsAsync(int id)
        {
            return await _maintenanceLogRepo.GetMaintenanceLogById(id) != null;
        }
    }
}