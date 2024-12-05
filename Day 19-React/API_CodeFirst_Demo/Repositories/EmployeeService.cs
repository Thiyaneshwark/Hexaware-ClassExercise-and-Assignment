using API_CodeFirst_Demo.Data;
using API_CodeFirst_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace API_CodeFirst_Demo.Repositories
{
    public class EmployeeService : IEmployeeService
    {
        private MyDbContext _context;

        public EmployeeService(MyDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = _context.Employees.ToList();
            if (employees.Count > 0)
            {
                return employees;
            }
            else
                return null;  
        }

        public Employee GetEmployeeById(int id)
        {
            if (id != 0 || id != null)
            {
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
                return employee;
            }
            else
            {
                return null;
            }
            return null;
                
        }

        public int AddNewEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return employee.EmployeeId;
                }
                else
                    return 0;  
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Designation = employee.Designation;
                existingEmployee.Email = employee.Email;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.DepartmentId = employee.DepartmentId;

                _context.Entry(existingEmployee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return "Employee updated successfully";
            }
            else
                return "Employee not found";
        }

        public string DeleteEmployee(int id)
        {
            if (id != null)
            {
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    return $"Employee with ID {id} has been removed";
                }
                else
                    return "Employee not found";
            }
            return "Id should not be null or zero";
        }               
        
    }
}
