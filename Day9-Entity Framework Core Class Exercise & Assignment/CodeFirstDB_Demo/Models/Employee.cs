using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CodeFirstDB_Demo.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        [Required(ErrorMessage = "Enter a Name")]
        [StringLength(15, ErrorMessage ="Name should not more than 15 characters")]
        [MinLength(3, ErrorMessage = "should not be less than 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage="Enter the Gender")]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage ="Gender Must be Male, Female or others")]
        public string Gender { get; set; }
        [Required(ErrorMessage="Enter Email")]
        [EmailAddress(ErrorMessage="Enter valid Email Address")]
        public string Email { get; set; }
        public string Designation { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        
        public virtual Department? Department { get; set; }
    }
}
