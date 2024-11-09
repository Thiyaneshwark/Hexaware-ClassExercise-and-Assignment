using Assignment_Authentication.Models;
using Assignment_Authentication.Models;
using System.Collections.Generic;

namespace Assignment_Authentication.Repositories
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees(); // Get all employees
        Employee GetEmployeeById(int id); // Get employee by ID
        int AddNewEmployee(Employee employee); // Add a new employee
        string UpdateEmployee(Employee employee); // Update employee details
        string DeleteEmployee(int id); // Delete an employee by ID
    }
}
