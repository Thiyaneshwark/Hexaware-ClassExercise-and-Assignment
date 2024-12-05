using System.Security.Claims;
using AssetManagement.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AssetManagement.Models.MultiValues;
using Microsoft.Extensions.Logging;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IServiceRequest _serviceRequest;
        private readonly IMaintenanceLogRepo _maintenanceLog;
        private readonly ILogger<ServiceRequestsController> _logger;

        public ServiceRequestsController(DataContext context, IServiceRequest serviceRequest, IMaintenanceLogRepo maintenanceLog, ILogger<ServiceRequestsController> logger)
        {
            _context = context;
            _serviceRequest = serviceRequest;
            _maintenanceLog = maintenanceLog;
            _logger = logger;
        }

        // GET: api/ServiceRequests

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequests()
        {
            try
            {
                // Log user claims for debugging
                var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
                _logger.LogDebug("User Claims: {@Claims}", claims);

                // Retrieve the numeric NameIdentifier claim
                var userIdClaim = User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _))?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    _logger.LogWarning("Invalid or missing User ID claim.");
                    return BadRequest("User ID claim is missing or not in numeric format.");
                }

                // Retrieve user role
                var userRole = User.FindFirstValue(ClaimTypes.Role);

                if (userRole == "Admin")
                {
                    _logger.LogInformation("Admin user fetching all service requests.");
                    var allRequests = await _serviceRequest.GetAllServiceRequests();
                    return Ok(allRequests);
                }

                // For regular users, fetch their service requests
                _logger.LogInformation("User {UserId} fetching their service requests.", userId);
                var userRequests = await _serviceRequest.GetServiceRequestsByUserId(userId);

                //if (!userRequests.Any())
                //{
                //    _logger.LogWarning("No service requests found for User ID {UserId}.", userId);
                //    return NotFound("No service requests found for the logged-in user.");
                //}

                return Ok(userRequests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching service requests.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        // PUT: api/ServiceRequests/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutServiceRequest(int id, ServiceRequest serviceRequest)
        {
            if (id != serviceRequest.ServiceId)
            {
                return BadRequest("Service request ID mismatch.");
            }

            var existingRequest = await _serviceRequest.GetServiceRequestById(id);
            if (existingRequest == null)
            {
                return NotFound("Service request not found.");
            }

            existingRequest.ServiceReqStatus = serviceRequest.ServiceReqStatus;

            if (serviceRequest.ServiceReqStatus == ServiceReqStatus.Approved)
            {
                var asset = await _context.Assets.FindAsync(serviceRequest.AssetId);
                if (asset != null)
                {
                    asset.AssetStatus = AssetStatus.UnderMaintenance;
                    _context.Entry(asset).State = EntityState.Modified;
                }
            }
            else if (serviceRequest.ServiceReqStatus == ServiceReqStatus.Completed)
            {
                var asset = await _context.Assets.FindAsync(serviceRequest.AssetId);
                if (asset != null)
                {
                    asset.AssetStatus = AssetStatus.Allocated;
                    _context.Entry(asset).State = EntityState.Modified;
                }
            }

            try
            {
                _serviceRequest.UpdateServiceRequest(existingRequest);
                await _serviceRequest.Save();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRequestExists(id))
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


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ServiceRequest>> PostServiceRequest(ServiceRequest serviceRequest)
        {
            try
            {
                // Validate the model state
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Model validation failed: {Errors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                    return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
                }

                _logger.LogInformation("Received ServiceRequest: {@ServiceRequest}", serviceRequest);

                // Get User ID from Claims
                var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && int.TryParse(c.Value, out _))?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    _logger.LogError("UserId claim is missing or empty.");
                    return Unauthorized("User is not authenticated.");
                }

                if (!int.TryParse(userIdClaim, out var loggedInUserId))
                {
                    _logger.LogError("UserId claim is not a valid integer. Value: {UserIdClaim}", userIdClaim);
                    return Unauthorized("User ID is invalid.");
                }

                serviceRequest.UserId = loggedInUserId;

                // Validate that the asset exists
                var assetExists = await _serviceRequest.AssetExists(serviceRequest.AssetId);
                if (!assetExists)
                {
                    _logger.LogWarning("Asset with ID {AssetId} does not exist.", serviceRequest.AssetId);
                    return BadRequest($"Asset with ID {serviceRequest.AssetId} does not exist.");
                }

                // Add and save the service request
                await _serviceRequest.AddServiceRequest(serviceRequest);
                await _serviceRequest.Save();

                // Log successful creation
                _logger.LogInformation("ServiceRequest created successfully: {@ServiceRequest}", serviceRequest);

                // Return the created resource
                return CreatedAtAction("GetServiceRequests", new { id = serviceRequest.ServiceId }, serviceRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating ServiceRequest");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }







        // DELETE: api/ServiceRequests/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
            try
            {
                var loggedInUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var serviceRequest = await _serviceRequest.GetServiceRequestById(id);
                if (serviceRequest == null)
                {
                    return NotFound();
                }

                if (serviceRequest.UserId != loggedInUserId)
                {
                    return Forbid();
                }

                await _serviceRequest.DeleteServiceRequest(id);
                await _serviceRequest.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting service request");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        private bool ServiceRequestExists(int id)
        {
            return _context.ServiceRequests.Any(e => e.ServiceId == id);
        }
    }
}
