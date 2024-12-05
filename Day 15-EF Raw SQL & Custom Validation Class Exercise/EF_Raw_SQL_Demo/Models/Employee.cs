using EF_Raw_SQL_Demo.Validations;
using System;
using System.Collections.Generic;

namespace EF_Raw_SQL_Demo.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Gender { get; set; } = null!;
        //For Validation:
        [EmployeeDateOfBirthValidation]
        public DateTime Dob { get; set; }
        public int? DeptId { get; set; }

        public virtual Department? Dept { get; set; }
    }
}
