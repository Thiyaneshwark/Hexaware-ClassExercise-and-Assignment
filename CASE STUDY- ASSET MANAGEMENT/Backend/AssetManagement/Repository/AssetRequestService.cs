using AssetManagement.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Repository
{
    public class AssetRequestService : IAssetRequest
    {
        private readonly DataContext _context;

        public AssetRequestService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AssetRequest>> GetAllAssetRequests()
        {
            return await _context.AssetRequests.AsNoTracking()
                .Include(ar => ar.Asset)
                .Include(ar => ar.User)
                .ToListAsync();
        }

        public async Task<List<AssetRequest>> GetAssetRequestsByUserId(int userId)
        {
            return await _context.AssetRequests
                .Where(sr => sr.UserId == userId)
                .Include(sr => sr.Asset)
                .Include(sr => sr.User)
                .ToListAsync();
        }

        public async Task<AssetRequest?> GetAssetRequestById(int id)
        {
            return await _context.AssetRequests
                .Include(ar => ar.Asset)
                .Include(ar => ar.User)
                .FirstOrDefaultAsync(u => u.AssetReqId == id);
        }

        public async Task AddAssetRequest(AssetRequest assetRequest)
        {
            _context.AssetRequests.Add(assetRequest); // Add to change tracker
            await _context.SaveChangesAsync();        // Save changes to the database
        }


        public Task<AssetRequest> UpdateAssetRequest(AssetRequest assetRequest)
        {
            _context.AssetRequests.Update(assetRequest);//changes
            return Task.FromResult(assetRequest);// updating

        }

        public async Task DeleteAssetRequest(int id)
        {
            var assetRequest = await _context.AssetRequests.FindAsync(id);
            if (assetRequest == null)
            {
                throw new Exception("Request not found");
            }

            _context.AssetRequests.Remove(assetRequest);

        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }

}
