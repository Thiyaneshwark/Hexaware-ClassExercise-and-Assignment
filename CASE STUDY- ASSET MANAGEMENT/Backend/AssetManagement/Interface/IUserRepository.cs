using AssetManagement.DTOs;
using AssetManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.Interface
{
    public interface IUserRepository
    {
        Task<User> Register(UserRegisterDto userRegisterDto);
        Task<User> Login(UserLoginDto userLoginDto);
        string GenerateJwtToken(User user);

        // New methods for CRUD operations
        Task<IEnumerable<User>> GetAllUser();
        Task<User?> GetUserById(int id);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<bool> ChangePassword(ChangePasswordDto dto);
        Task Save();
    }
}
