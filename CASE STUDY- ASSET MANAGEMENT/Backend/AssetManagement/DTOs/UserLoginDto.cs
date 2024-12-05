using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Models
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
