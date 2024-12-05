using System.ComponentModel.DataAnnotations;
using static AssetManagement.Models.MultiValues;

namespace AssetManagement.Models
{
    public class UserRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string UserMail { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Dept { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public UserType User_Type { get; set; }
    }
}
