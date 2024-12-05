﻿namespace AssetManagement.Interface
{
    public interface IServiceRequest
    {
        Task<List<ServiceRequest>> GetAllServiceRequests();
        Task<ServiceRequest?> GetServiceRequestById(int id);
        Task<bool> AssetExists(int assetId);
        Task AddServiceRequest(ServiceRequest serviceRequest);
        Task<ServiceRequest> UpdateServiceRequest(ServiceRequest existingRequest);
        Task DeleteServiceRequest(int id);
        Task Save();

        Task<List<ServiceRequest>> GetServiceRequestsByUserId(int userId);
    }
}
