﻿namespace AssetManagement.Interface
{
    public interface IUserProfileRepo
    {
        //Task<List<UserProfile>> GetAllProfiles();
        Task<UserProfile?> GetProfilesById(int id);
        Task AddProfiles(UserProfile userProfile);
        Task<UserProfile> UpdateProfiles(UserProfile userProfile);
        Task DeleteProfiles(int id);
        Task Save();
        Task<string?> UploadProfileImageAsync(int userId, IFormFile file);
        //Task<string?> GetProfileImageAsync(int userId);
        //Task<bool> SetDefaultProfileImageAsync(int userId);
    }
}
