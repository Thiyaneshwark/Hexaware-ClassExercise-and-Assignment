using System.ComponentModel.DataAnnotations;

namespace AssetManagement.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}