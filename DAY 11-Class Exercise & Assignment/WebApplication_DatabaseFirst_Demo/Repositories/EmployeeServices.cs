using Microsoft.EntityFrameworkCore;
using WebApplication_DatabaseFirst_Demo.Models;

namespace WebApplication_DatabaseFirst_Demo.Repositories
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly BikeStoresContext _context;

        public EmployeeServices(BikeStoresContext dbContext)
        {
            _context = dbContext;
        }

        public int AddNewEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return employee.Id ?? 0; // Return employee ID or 0 if null
                }
                else return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string DeleteEmployee(int id)
        {
            if (id != 0)
            {
                var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    return "The given Employee id " + id + " has been removed";
                }
                else
                    return "Employee not found for deletion";
            }
            return "Id should not be zero or null";
        }

        public List<Employee> GetAllEmployees()
        {
            var employees = _context.Employees.ToList();
            if (employees.Count > 0)
                return employees;
            return new List<Employee>(); // Return an empty list instead of null
        }

        public Employee GetEmployeeById(int id)
        {
            if (id != 0)
            {
                var employee = _context.Employees.FirstOrDefault(x => x.Id == id);
                if (employee != null)
                    return employee;
                else
                    return null; // Employee not found
            }
            return null; // Id is invalid
        }

        public string UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(x => x.Id == employee.Id);
            if (existingEmployee != null)
            {
                // Update the existing employee details
                existingEmployee.Name = employee.Name;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Dob = employee.Dob;
                existingEmployee.DeptId = employee.DeptId;

                // Mark the entity as modified and save changes
                _context.Entry(existingEmployee).State = EntityState.Modified;
                _context.SaveChanges();

                return "Employee record updated successfully";
            }

            return "Employee record not found";
        }
    }
}
