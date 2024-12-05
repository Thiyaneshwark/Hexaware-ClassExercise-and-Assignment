using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Interface;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAsset _asset;
        //private readonly IAsset _assetRequest;  
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(IAsset asset, IAssetRequest assetRequest, ILogger<AssetsController> logger)
        {
            _asset = asset;
            //_assetRequest = assetRequest;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAsset()
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


            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(userRole))
            {
                _logger.LogWarning("User role claim is missing.");
                return Unauthorized("User role claim is missing.");
            }

            try
            {
                _logger.LogInformation("Fetching all assets...");
                var assets = await _asset.GetAllAssets();
                return Ok(assets);
            }
            catch (Exception ex)
            {
                // Log any exceptions and return an internal server error
                _logger.LogError(ex, "An error occurred while retrieving asset requests.");
                return StatusCode(500, "Internal server error");
            }
        }




        // PUT: api/Assets/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutAsset(int id, Asset asset)
        {
            if (id != asset.AssetId)
            {
                _logger.LogWarning("Asset ID in the URL does not match the asset ID in the body.");
                return BadRequest("Asset ID mismatch.");
            }

            try
            {
                await _asset.UpdateAsset(asset);
                await _asset.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AssetExists(id))
                {
                    _logger.LogWarning("Asset with ID {AssetId} not found during update.", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the asset with ID {AssetId}.", id);
                return StatusCode(500, "Internal server error");
            }

            return NoContent();
        }

        // POST: api/Assets
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Asset>> PostAsset(Asset asset)
        {
            try
            {
                await _asset.AddAsset(asset);
                await _asset.Save();

                return CreatedAtAction(nameof(GetAsset), new { id = asset.AssetId }, asset);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new asset.");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Assets/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            try
            {
                var assetExists = await _asset.GetAssetById(id);
                if (assetExists == null)
                {
                    _logger.LogWarning("Asset with ID {AssetId} not found for deletion.", id);
                    return NotFound();
                }

                await _asset.DeleteAsset(id);
                await _asset.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the asset with ID {AssetId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task<bool> AssetExists(int id)
        {
            var asset = await _asset.GetAssetById(id);
            return asset != null;
        }
    }
}
