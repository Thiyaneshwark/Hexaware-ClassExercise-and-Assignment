using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_DatabaseFirst_Demo.Controllers;
using WebApplication_DatabaseFirst_Demo.Models;
using WebApplication_DatabaseFirst_Demo.Repositories;

namespace WebApplication_DatabaseFirst_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employees = _employeeServices.GetAllEmployees();
                if (employees != null && employees.Count > 0)
                    return Ok(employees);  // Return status 200 with the list of employees
                return NoContent();  // Return status 204 if no employees found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var employee = _employeeServices.GetEmployeeById(id);
                if (employee != null)
                    return Ok(employee);  // Return status 200 with the employee data
                return NotFound();  // Return status 404 if employee not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult AddNewEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Employee data is null.");
                }

                int employeeId = _employeeServices.AddNewEmployee(employee);
                if (employeeId > 0)
                    return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, employee); // Return status 201 (Created)
                return BadRequest("Failed to create employee.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Employee/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Employee data is null.");
                }

                if (id != employee.Id)
                {
                    return BadRequest("Employee ID mismatch.");
                }

                string updateResult = _employeeServices.UpdateEmployee(employee);
                if (updateResult == "Employee record updated successfully")
                    return Ok(updateResult);  // Return status 200 with success message
                return NotFound("Employee not found.");  // Return status 404 if employee is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                string deleteResult = _employeeServices.DeleteEmployee(id);
                if (deleteResult.Contains("Removed"))
                    return Ok(deleteResult);  // Return status 200 with success message
                return NotFound(deleteResult);  // Return status 404 if employee is not found
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
