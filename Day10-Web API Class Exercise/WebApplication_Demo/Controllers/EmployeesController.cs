using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_Demo.Models;

namespace WebApplication_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        static List<Employee> employees;
        static EmployeesController()
        {
            employees = new List<Employee>()
            {
                new Employee() { EmployeeId= 1, Name="Ramesh",DOJ=new DateTime(2024,10,24), Designation="Trainee", Salary=34909.20M },
                new Employee() { EmployeeId= 2, Name="Deepak",DOJ=new DateTime(2024,10,24), Designation="Trainee", Salary=30009.20M },
                new Employee() { EmployeeId= 3, Name="Barath",DOJ=new DateTime(2024,10,24), Designation="Trainee", Salary=31009.20M },
                new Employee() { EmployeeId= 4, Name="Dinesh",DOJ=new DateTime(2024,10,24), Designation="Trainee", Salary=34909.20M },
            };
        }
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            if(employees.Count>0)
                return Ok(employees);
            else
                return NotFound();
        }
        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();
            else
                employees.Add(employee);
            return CreatedAtAction(nameof(GetAllEmployees), new { id = employee.EmployeeId, message = "Data Added" });
        }
        [HttpPut]
        public IActionResult PutEmployee(int id, Employee employee)
        {
            if(id!=employee.EmployeeId)
            {
                return BadRequest("Id mismatch");
            }
            var existingEmployee = employees.Where(x=>x.EmployeeId==id).FirstOrDefault();
            if(existingEmployee == null)
            {
                return NotFound();
            }
            existingEmployee.Name=employee.Name;
            existingEmployee.Designation=employee.Designation;
            existingEmployee.DOJ = employee.DOJ;
            existingEmployee.Salary=employee.Salary;
            return Ok(existingEmployee);
        }
        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            var existingEmployee = employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (existingEmployee == null)
            {
                return NotFound();
            }
            employees.Remove(existingEmployee);
            return Ok(existingEmployee.EmployeeId + " Removed");
        }
    }
}
