using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AssetManagement.Interface;
using AssetManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AssetManagement.Models.MultiValues;
using Microsoft.Extensions.Logging;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnRequestsController : ControllerBase
    {
        private readonly IReturnReqRepo _returnRequestRepo;
        private readonly DataContext _context;
        private readonly IAsset _asset;
        private readonly IAssetAllocation _assetAlloc;
        private readonly IAssetRequest _assetRequest;
        private readonly ILogger<ReturnRequestsController> _logger;

        public ReturnRequestsController(
            DataContext context,
            IReturnReqRepo returnRequestRepo,
            IAsset asset,
            IAssetAllocation assetAllocation,
            IAssetRequest assetRequest,
            ILogger<ReturnRequestsController> logger)
        {
            _context = context;
            _returnRequestRepo = returnRequestRepo;
            _asset = asset;
            _assetAlloc = assetAllocation;
            _assetRequest = assetRequest;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ReturnRequest>>> GetReturnRequests()
        {
            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _))?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("User ID claim is missing or invalid.");
            }

            var userRole = User.FindFirstValue(ClaimTypes.Role);

            try
            {
                if (userRole == "Admin")
                {
                    var allRequests = await _returnRequestRepo.GetAllReturnRequest();
                    return Ok(allRequests);
                }
                else
                {
                    var userRequests = await _returnRequestRepo.GetReturnRequestsByUserId(userId);
                    //if (userRequests == null || !userRequests.Any())
                    //{
                    //    return NotFound("No details found for the current user.");
                    //}
                    return Ok(userRequests);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching return requests.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ReturnRequest>> GetReturnRequest(int id)
        {
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            try
            {
                if (userRole == "Admin")
                {
                    var request = await _returnRequestRepo.GetReturnRequestById(id);
                    if (request == null)
                    {
                        return NotFound($"No details found for the ReturnRequest ID: {id}.");
                    }
                    return Ok(request);
                }

                return Forbid();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching ReturnRequest with ID: {id}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutReturnRequest(int id, ReturnRequest returnRequest)
        {
            if (id != returnRequest.ReturnId)
            {
                return BadRequest("Mismatched ReturnRequest ID.");
            }

            try
            {
                var existingRequest = await _returnRequestRepo.GetReturnRequestById(id);
                if (existingRequest == null)
                {
                    return NotFound($"No ReturnRequest found with ID: {id}.");
                }

                _context.Entry(existingRequest).CurrentValues.SetValues(returnRequest);

                if (returnRequest.ReturnStatus == ReturnReqStatus.Approved || returnRequest.ReturnStatus == ReturnReqStatus.Returned)
                {
                    existingRequest.ReturnDate = DateTime.Now;
                }

                if (returnRequest.ReturnStatus == ReturnReqStatus.Returned)
                {
                    var asset = await _context.Assets.FindAsync(existingRequest.AssetId);
                    if (asset != null)
                    {
                        asset.AssetStatus = AssetStatus.OpenToRequest;
                        await _asset.UpdateAsset(asset);
                        await _asset.Save();

                        var allocation = await _context.AssetAllocations
                            .FirstOrDefaultAsync(a => a.AssetId == existingRequest.AssetId && a.UserId == existingRequest.UserId);

                        if (allocation != null)
                        {
                            await _assetAlloc.DeleteAllocation(allocation.AllocationId);
                            await _assetAlloc.Save();
                        }

                        var assetRequest = await _context.AssetRequests
                            .FirstOrDefaultAsync(a => a.AssetId == existingRequest.AssetId && a.UserId == existingRequest.UserId && a.RequestStatus == RequestStatus.Allocated);

                        if (assetRequest != null)
                        {
                            _assetRequest.DeleteAssetRequest(assetRequest.AssetReqId);
                            await _asset.Save();
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating ReturnRequest with ID: {id}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostReturnRequest(ReturnRequest returnRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!Enum.IsDefined(typeof(ReturnReqStatus), returnRequest.ReturnStatus))
            {
                return BadRequest("Invalid return status.");
            }

            try
            {
                await _returnRequestRepo.AddReturnRequest(returnRequest);
                return CreatedAtAction(nameof(GetReturnRequest), new { id = returnRequest.ReturnId }, returnRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving ReturnRequest.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReturnRequest(int id)
        {
            try
            {
                var loggedInUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var returnRequest = await _returnRequestRepo.GetReturnRequestById(id);

                if (returnRequest == null)
                {
                    return NotFound($"No ReturnRequest found with ID: {id}.");
                }

                if (returnRequest.UserId != loggedInUserId)
                {
                    return Forbid();
                }

                await _returnRequestRepo.DeleteReturnRequest(id);
                await _returnRequestRepo.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting ReturnRequest with ID: {id}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        private bool ReturnRequestExists(int id)
        {
            return _context.ReturnRequests.Any(e => e.ReturnId == id);
        }
    }
}
