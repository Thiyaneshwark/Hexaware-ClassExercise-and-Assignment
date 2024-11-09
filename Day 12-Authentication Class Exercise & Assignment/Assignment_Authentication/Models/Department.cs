using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Assignment_Authentication.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Department Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Department Head")]
        [Required]
        public string DepartmentHead { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
