using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_CodeFirst_Demo.Models
{
    [Table("tblEmployee")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }    
        public string Gender { get; set; }
        public string? Designation {  get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId {  get; set; }
        public virtual Department? Department { get; set; }
    }
}
