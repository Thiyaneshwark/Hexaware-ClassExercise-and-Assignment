using AssetManagement.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Repository
{
    public class AssetAllocationService : IAssetAllocation
    {
        private readonly DataContext _context;

        public AssetAllocationService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AssetAllocation>> GetAllAllocations()
        {
            try
            {
                return await _context.AssetAllocations
                    .AsNoTracking()
                    .Include(aa => aa.Asset)
                        .ThenInclude(asset => asset.Category)
                        .ThenInclude(category => category.SubCategories)
                    .Include(aa => aa.User)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error fetching asset allocations: {ex.Message}");
                return new List<AssetAllocation>(); // Or handle error as appropriate
            }
        }


        public async Task AddAllocation(AssetAllocation allocation)
        {
            _context.AssetAllocations.Add(allocation);

        }
        public async Task DeleteAllocation(int id)
        {
            var allocation = await GetAllocationById(id);
            if (allocation == null)
            {
                throw new Exception("Allocation not found");
            }
            _context.AssetAllocations.Remove(allocation);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<AssetAllocation?> GetAllocationById(int id)
        {
            return await _context.AssetAllocations
                .Include(aa => aa.Asset)
                    .ThenInclude(asset => asset.Category)
                    .ThenInclude(category => category.SubCategories)
                .Include(aa => aa.User)
                .FirstOrDefaultAsync(aa => aa.AllocationId == id);
        }

        public async Task<List<AssetAllocation>> GetAllocationListById(int userId)
        {
            return await _context.AssetAllocations
                .Where(aa => aa.UserId == userId)
                .Include(aa => aa.Asset)
                    .ThenInclude(asset => asset.Category)
                    .ThenInclude(category => category.SubCategories)
                .Include(aa => aa.User)
                .Include(aa => aa.AssetRequest)
                .ToListAsync();
        }

        public Task<AssetAllocation> UpdateAllocation(AssetAllocation allocation)
        {
            _context.AssetAllocations.Update(allocation);
            return Task.FromResult(allocation);

        }


    }

}
