using System.Security.Claims;
using AssetManagement.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using static AssetManagement.Models.MultiValues;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditsController : ControllerBase
    {
        private readonly IAuditRepo _auditRepo;
        private readonly DataContext _context;
        private readonly ILogger<AuditsController> _logger;

        public AuditsController(IAuditRepo auditRepo, DataContext context, ILogger<AuditsController> logger)
        {
            _auditRepo = auditRepo;
            _context = context;
            _logger = logger;
        }

        // GET: api/Audits
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Audit>>> GetAudits()
        {
            // Retrieve the user ID claim and validate its format
            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim is missing from the token.");
            }

            if (int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("User ID claim is not in the correct numeric format.");
            }

            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userRole))
            {
                return Unauthorized("User role claim is missing.");
            }

            try
            {
                // Admin role: Retrieve all audits
                if (userRole == "Admin")
                {
                    List<Audit> audits1 = await _auditRepo.GetAllAudits();
                    var audits12 = audits1;
                    return Ok(audits12);
                }

                // Non-admin role: Retrieve audits specific to userId
                var audits = await _auditRepo.GetAuditsByUserId(userId);
                if (audits == null || audits.Count == 0)
                {
                    return NotFound("No audit records found for this user.");
                }
                return Ok(audits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving audit records.");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/Audits/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Audit>> GetAudit(int id)
        {
            var audit = await _auditRepo.GetAuditById(id);
            if (audit == null)
            {
                return NotFound();
            }
            return Ok(audit);
        }

        // PUT: api/Audits/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Employee,Admin")]
        public async Task<IActionResult> PutAudit(int id, Audit audit)
        {
            if (id != audit.AuditId)
            {
                return BadRequest("Audit ID mismatch.");
            }

            var userId = id;
            var existingAudit = await _auditRepo.GetAuditById(id);

            if (existingAudit == null)
            {
                return NotFound("Audit not found.");
            }

            // Ensure the logged-in user can only update their own audits
            if (existingAudit.UserId != userId)
            {
                return Forbid("You are not allowed to modify this audit.");
            }

            // Update fields as necessary
            existingAudit.AuditDate = audit.AuditDate;
            existingAudit.AuditMessage = audit.AuditMessage;
            existingAudit.Audit_Status = audit.Audit_Status;

            try
            {
                _auditRepo.UpdateAudit(existingAudit);
                await _auditRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditExists(id))
                {
                    return NotFound("Audit not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Audits
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Audit>> PostAudit(Audit audit)
        {
            try
            {
                // Validate asset and user existence before adding the audit
                var assetExists = await _context.Assets.AnyAsync(a => a.AssetId == audit.AssetId);
                var userExists = await _context.Users.AnyAsync(u => u.UserId == audit.UserId);

                if (!assetExists || !userExists)
                {
                    return BadRequest("Invalid assetId or userId.");
                }

                // Validate if the audit status is a valid enum
                if (!Enum.IsDefined(typeof(AuditStatus), audit.Audit_Status))
                {
                    return BadRequest("Invalid audit status.");
                }

                // Check model validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Start a transaction if needed
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Add the audit record
                        _auditRepo.AddAuditReq(audit);

                        // Save the audit record
                        await _auditRepo.Save();

                        // Commit the transaction
                        await transaction.CommitAsync();

                        // Return a success response
                        return CreatedAtAction(nameof(GetAudit), new { id = audit.AuditId }, audit);
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction if an error occurs
                        await transaction.RollbackAsync();

                        // Log the error for debugging
                        _logger.LogError(ex, "An error occurred while creating an audit record. Exception Details: {Message}", ex.Message);

                        // Return a generic server error response
                        return StatusCode(500, "Internal server error");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and handle any unexpected issues
                _logger.LogError(ex, "An error occurred while processing the audit request.");
                return StatusCode(500, "Internal server error");
            }
        }


        // DELETE: api/Audits/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAudit(int id)
        {
            try
            {
                await _auditRepo.DeleteAuditReq(id);
                await _auditRepo.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the audit.");
                return NotFound("Audit not found.");
            }
        }

        private bool AuditExists(int id)
        {
            return _context.Audits.Any(e => e.AuditId == id);
        }
    }
}
