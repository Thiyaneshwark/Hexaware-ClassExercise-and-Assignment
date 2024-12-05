using AssetManagement.DTOs;
using AssetManagement.Interface;
using AssetManagement.Models;
using AssetManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var user = await _userRepository.Register(dto);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            var user = await _userRepository.Login(dto);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = _userRepository.GenerateJwtToken(user);
            var expiresAt = DateTime.UtcNow.AddMinutes(60); 

            return Ok(new { token, expiresAt });
        }
        // Change Password API
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var isPasswordChanged = await _userRepository.ChangePassword(dto);
            if (isPasswordChanged)
            {
                return Ok(new { message = "Password changed successfully" });
            }
            else
            {
                return Unauthorized(new { message = "Invalid old password" });
            }
        }
    }
}
