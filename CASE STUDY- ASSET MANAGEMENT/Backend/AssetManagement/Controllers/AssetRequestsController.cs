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
using static AssetManagement.Models.MultiValues;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetRequestsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAssetRequest _assetRequest;
        private readonly IAssetAllocation _assetAlloc;
        private readonly IAsset _asset;
        private readonly ILogger<AssetRequestsController> _logger;


        public AssetRequestsController(DataContext context,
                                       IAssetRequest assetRequest,
                                       IAssetAllocation assetAlloc,
                                       IAsset asset,
                                       ILogger<AssetRequestsController> logger)
        {
            _context = context;
            _assetRequest = assetRequest;
            _assetAlloc = assetAlloc;
            _asset = asset;
            _logger = logger;
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AssetRequest>>> GetAssetRequests()
        {

            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }


            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _))?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                _logger.LogWarning("User ID claim is missing or invalid.");
                return Unauthorized("User ID claim is missing or invalid.");
            }

            if (!int.TryParse(userIdClaim, out var userId))
            {
                _logger.LogWarning("User ID claim is not in the correct numeric format.");
                return Unauthorized("User ID claim is not in the correct numeric format.");
            }

            // Retrieve and validate the user role claim
            var userRole = User.FindFirstValue(ClaimTypes.Role);
            //if (string.IsNullOrEmpty(userRole))
            //{
            //    _logger.LogWarning("User role claim is missing.");
            //    return Unauthorized("User role claim is missing.");
            //}

            try
            {
                // If the user is an Admin, retrieve all asset requests
                if (userRole == "Admin")
                {
                    var assetRequests = await _assetRequest.GetAllAssetRequests();
                    return Ok(assetRequests);
                }
                else
                {
                    // If the user is not an Admin, retrieve asset requests by user ID
                    var userRequests = await _assetRequest.GetAssetRequestsByUserId(userId);
                    //if (userRequests == null || !userRequests.Any())
                    //{
                    //    _logger.LogWarning($"No asset requests found for user with ID {userId}.");
                    //    return NotFound("No asset requests found.");
                    //}
                    return Ok(userRequests);
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions and return an internal server error
                _logger.LogError(ex, "An error occurred while retrieving asset requests.");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: api/AssetRequests/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAssetRequest(int id, AssetRequest assetRequest)
        {

            if (id != assetRequest.AssetReqId)
            {
                _logger.LogWarning($"Asset request ID mismatch: {id} != {assetRequest.AssetReqId}");
                return BadRequest();
            }

            _assetRequest.UpdateAssetRequest(assetRequest);
            await _assetRequest.Save();
            //if (assetRequest.RequestStatus == RequestStatus.Allocated)
            //{
            var existingAllocId = await _context.AssetAllocations
                .FirstOrDefaultAsync(aa => aa.AssetReqId == assetRequest.AssetReqId);
            if (existingAllocId == null)
            {
                var assetAllocation = new AssetAllocation
                {
                    AssetId = assetRequest.AssetId,
                    UserId = assetRequest.UserId,
                    AssetReqId = assetRequest.AssetReqId,
                    AllocatedDate = DateTime.Now
                };
                await _assetAlloc.AddAllocation(assetAllocation);

                var asset = await _context.Assets.FindAsync(assetRequest.AssetId);
                if (asset != null)
                {
                    asset.AssetStatus = AssetStatus.Allocated;
                    _asset.UpdateAsset(asset);
                }
            }
            //}

            try
            {
                _assetAlloc.Save();
                _asset.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetRequestExists(id))
                {
                    _logger.LogWarning($"Asset request with ID {id} not found.");
                    return NotFound();
                }
                else
                {
                    _logger.LogError("Concurrency issue while updating asset request.");
                    return Conflict("The asset request could not be updated due to a concurrency issue.");
                }
            }

            return NoContent();
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<AssetRequest>> PostAssetRequest(AssetRequest assetRequest)
        {
            // Log all claims to identify the correct one
            foreach (var claim in User.Claims)
            {
                _logger.LogInformation($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            // Retrieve the user ID claim using ClaimTypes.NameIdentifier
            var userIdClaim = User.Claims
               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _))?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                _logger.LogWarning("User ID claim is missing or invalid.");
                return Unauthorized("User authentication is required or User ID is invalid.");
            }

            assetRequest.UserId = userId;

            var existingRequest = await _assetRequest.GetAssetRequestsByUserId(assetRequest.UserId);
            if (existingRequest != null && existingRequest.Any(r => r.RequestStatus == RequestStatus.Pending))
            {
                _logger.LogWarning("User already has a pending asset request.");
                return Conflict("User already has a pending asset request.");
            }

            _assetRequest.AddAssetRequest(assetRequest);

            return CreatedAtAction("GetAssetRequests", new { id = assetRequest.AssetReqId }, assetRequest);
        }







        // DELETE: api/AssetRequests/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAssetRequest(int id)
        {
            try
            {
                var loggedInUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var assetRequest = await _assetRequest.GetAssetRequestById(id);
                if (assetRequest == null)
                {
                    _logger.LogWarning($"Asset request with ID {id} not found.");
                    return NotFound();
                }

                if (assetRequest.UserId != loggedInUserId)
                {
                    _logger.LogWarning($"User {loggedInUserId} is trying to delete another user's asset request.");
                    return Forbid();
                }

                await _assetRequest.DeleteAssetRequest(id);
                await _assetRequest.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the asset request.");
                return BadRequest();
            }
        }

        // Helper method to check if an asset request exists
        private bool AssetRequestExists(int id)
        {
            return _context.AssetRequests.Any(e => e.AssetReqId == id);
        }
    }
}