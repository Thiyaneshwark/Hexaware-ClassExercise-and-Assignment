using AssetManagement.DTOs;
using AssetManagement.Interface;
using AssetManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static AssetManagement.Models.MultiValues;

namespace AssetManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;  // Inject IConfiguration
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(DataContext context, IConfiguration configuration, ILogger<UserRepository> logger)
        {
            _context = context;
            _configuration = configuration;  // Assign configuration to the field
            _passwordHasher = new PasswordHasher<User>();
            _logger = logger;
        }

        // Register Method (No changes needed here)
        public async Task<User> Register(UserRegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                UserMail = dto.UserMail,
                Gender = dto.Gender,
                Dept = dto.Dept,
                Designation = dto.Designation,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                Branch = dto.Branch,
                User_Type = dto.User_Type
            };

            // Hash the password
            user.Password = _passwordHasher.HashPassword(user, dto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Login Method (No changes needed here)
        public async Task<User?> Login(UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }

            if (user.Password.Length < 20) // Plain-text password
            {
                if (user.Password == dto.Password)
                {
                    // Optionally hash the password and save
                    user.Password = _passwordHasher.HashPassword(user, dto.Password);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new UnauthorizedAccessException("Invalid password.");
                }
            }
            else
            {
                // Hashed password check
                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    throw new UnauthorizedAccessException("Invalid password.");
                }
            }

            return user;
        }

        // Updated GenerateJwtToken Method with Issuer and Audience Claims
        public string GenerateJwtToken(User user)
        {
            // Check if the user has a valid role
            if (user.User_Type == null)
            {
                throw new UnauthorizedAccessException("User role is not defined.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            // Set claims based on user role
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                };

            // Add role-specific claim (convert enum to string here)
            if (user.User_Type == UserType.Admin)
            {
                claims.Add(new Claim(ClaimTypes.Role, UserType.Admin.ToString()));
            }
            else if (user.User_Type == UserType.Employee)
            {
                claims.Add(new Claim(ClaimTypes.Role, UserType.Employee.ToString()));
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid role for the user.");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60), // Set the expiration time as required
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }







        //    public string GenerateJwtToken(User user)
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        //        var claims = new[]
        //        {
        //    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        //    new Claim(ClaimTypes.Role, "Admin") // Customize based on actual user role
        //};

        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(claims),
        //            Expires = DateTime.UtcNow.AddMinutes(60),
        //            Issuer = _configuration["Jwt:Issuer"],  // Add Issuer here
        //            Audience = _configuration["Jwt:Audience"],  // Add Audience here
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        };

        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        return tokenHandler.WriteToken(token);
        //    }



        // CRUD Methods
        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChangePassword(ChangePasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);
            if (user == null)
            {
                _logger.LogWarning($"User {dto.UserName} not found for password change.");
                return false;
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.OldPassword);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning($"Old password mismatch for user: {dto.UserName}");
                return false;
            }

            user.Password = _passwordHasher.HashPassword(user, dto.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Password changed successfully for user: {dto.UserName}");
            return true;
        }
    }
}