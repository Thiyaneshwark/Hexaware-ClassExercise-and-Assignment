using System.Security.Claims;
using AssetManagement.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetAllocationsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAssetAllocation _assetAllocationService;
        private readonly ILogger<AssetAllocationsController> _logger;

        public AssetAllocationsController(
            DataContext context,
            IAssetAllocation assetAllocationService,
            ILogger<AssetAllocationsController> logger)
        {
            _context = context;
            _assetAllocationService = assetAllocationService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AssetAllocation>>> GetAssetAllocations()
        {
            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _))?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                _logger.LogWarning("Invalid or missing User ID claim.");
                return Unauthorized("User authentication is required or User ID is invalid.");
            }

            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(userRole))
            {
                _logger.LogWarning("User role claim is missing.");
                return Unauthorized("User role is missing.");
            }

            try
            {
                if (userRole == "Admin")
                {
                    var allocations = await _assetAllocationService.GetAllAllocations();
                    return Ok(allocations);
                }
                else
                {
                    var userAllocations = await _assetAllocationService.GetAllocationListById(userId);
                    if (!userAllocations.Any())
                    {
                        _logger.LogWarning($"No allocations found for user with ID {userId}.");
                        return NotFound("No asset allocations found.");
                    }
                    return Ok(userAllocations);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving asset allocations.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AssetAllocation>> PostAssetAllocation(AssetAllocation allocation)
        {
            try
            {
                await _assetAllocationService.AddAllocation(allocation);
                await _assetAllocationService.Save();
                return CreatedAtAction("GetAssetAllocations", new { id = allocation.AllocationId }, allocation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new asset allocation.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAssetAllocation(int id, AssetAllocation allocation)
        {
            if (id != allocation.AllocationId)
            {
                _logger.LogWarning($"Allocation ID mismatch: {id} != {allocation.AllocationId}");
                return BadRequest("Allocation ID mismatch.");
            }

            try
            {
                await _assetAllocationService.UpdateAllocation(allocation);
                await _assetAllocationService.Save();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetAllocationExists(id))
                {
                    _logger.LogWarning($"Asset allocation with ID {id} not found.");
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Concurrency issue while updating asset allocation.");
                    return Conflict("The allocation could not be updated due to a concurrency issue.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the asset allocation.");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAssetAllocation(int id)
        {
            try
            {
                if (!AssetAllocationExists(id))
                {
                    _logger.LogWarning($"Asset allocation with ID {id} not found.");
                    return NotFound();
                }

                await _assetAllocationService.DeleteAllocation(id);
                await _assetAllocationService.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the asset allocation.");
                return StatusCode(500, "Internal server error.");
            }
        }

        private bool AssetAllocationExists(int id)
        {
            return _context.AssetAllocations.Any(e => e.AllocationId == id);
        }
    }
}
