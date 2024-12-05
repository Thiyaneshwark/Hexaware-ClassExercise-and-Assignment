using AssetManagement.Interface;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Repository
{
    public class ServiceRequestImpl : IServiceRequest
    {
        private readonly DataContext _context;

        public ServiceRequestImpl(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceRequest>> GetAllServiceRequests()
        {
            return await _context.ServiceRequests
                .Include(sr => sr.Asset)
                .Include(sr => sr.User)
                .ToListAsync();
        }

        public async Task<ServiceRequest?> GetServiceRequestById(int id)
        {
            return await _context.ServiceRequests
                .Include(sr => sr.Asset)
                .Include(sr => sr.User)
                .FirstOrDefaultAsync(u => u.ServiceId == id);
        }

        public async Task AddServiceRequest(ServiceRequest serviceRequest)
        {
            // Validate that the associated asset exists in the database
            var assetExists = await _context.Assets.AnyAsync(a => a.AssetId == serviceRequest.AssetId);
            if (!assetExists)
            {
                // Throw a clear exception if the asset does not exist
                throw new ArgumentException("Invalid AssetId. The specified asset does not exist.");
            }

            // Add the service request to the DbContext
            await _context.ServiceRequests.AddAsync(serviceRequest);

            // Save the changes to the database
            await _context.SaveChangesAsync();
        }




        public async Task<bool> AssetExists(int assetId)
        {
            return await _context.Assets.AnyAsync(a => a.AssetId == assetId);
        }

        public Task<ServiceRequest> UpdateServiceRequest(ServiceRequest existingRequest)
        {
            _context.ServiceRequests.Update(existingRequest);
            return Task.FromResult(existingRequest);
        }

        public async Task DeleteServiceRequest(int id)
        {
            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest == null)
            {
                throw new Exception("Service Request not Found");
            }
            _context.ServiceRequests.Remove(serviceRequest);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<ServiceRequest>> GetServiceRequestsByUserId(int userId)
        {
            return await _context.ServiceRequests
                .Where(sr => sr.UserId == userId)
                .Include(sr => sr.Asset)
                .Include(sr => sr.User)
                .ToListAsync();
        }


    }

}
