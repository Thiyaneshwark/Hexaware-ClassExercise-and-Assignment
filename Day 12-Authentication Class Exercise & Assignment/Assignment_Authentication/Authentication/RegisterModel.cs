using System.ComponentModel.DataAnnotations;

namespace Assignment_Authentication.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        public string? Designation { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
      //  public int? DepartmentId { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
