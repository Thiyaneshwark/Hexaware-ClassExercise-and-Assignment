using Assignment_Authentication.Authentication;
using Assignment_Authentication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment_Authentication.Repositories
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all employees
        public List<Employee> GetAllEmployees()
        {
            var employees = _context.Employees.ToList();
            return employees.Count > 0 ? employees : null;  // Return employees if any, otherwise null
        }

        // Get an employee by ID
        public Employee GetEmployeeById(int id)
        {
            // Check if ID is valid
            if (id > 0)
            {
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
                return employee;
            }
            return null; // Return null if ID is invalid
        }

        // Add a new employee
        public int AddNewEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    // Hash the password before saving (optional, recommend a hashing algorithm like bcrypt or SHA256)
                    employee.Password = HashPassword(employee.Password);

                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return employee.EmployeeId; // Return the ID of the newly added employee
                }
                return 0; // Return 0 if the employee is null
            }
            catch (Exception e)
            {
                // Handle any exceptions that occur
                throw new Exception($"An error occurred while adding the employee: {e.Message}");
            }
        }

        // Update an existing employee
        public string UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if (existingEmployee != null)
            {
                // Update properties
                existingEmployee.Name = employee.Name;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Designation = employee.Designation;
                existingEmployee.Email = employee.Email;
                existingEmployee.Salary = employee.Salary;

                // Handle optional DepartmentId (can be null)
                if (employee.DepartmentId.HasValue)
                {
                    existingEmployee.DepartmentId = employee.DepartmentId.Value;
                }

                // Update the password if provided
                if (!string.IsNullOrEmpty(employee.Password))
                {
                    existingEmployee.Password = HashPassword(employee.Password);
                }

                _context.Entry(existingEmployee).State = EntityState.Modified;
                _context.SaveChanges();
                return "Employee updated successfully";
            }
            else
            {
                return "Employee not found"; // Employee does not exist
            }
        }

        // Delete an employee by ID
        public string DeleteEmployee(int id)
        {
            // Check if ID is valid
            if (id > 0)
            {
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    return $"Employee with ID {id} has been removed"; // Success message
                }
                else
                {
                    return "Employee not found"; // Employee not found in DB
                }
            }
            return "Invalid ID"; // Return message if ID is invalid
        }

        // Method to hash password (for demonstration, replace with actual hashing algorithm)
        private string HashPassword(string password)
        {
            // Use a proper password hashing algorithm like bcrypt or SHA256
            // For now, just a placeholder.
            return password; // Placeholder: Return the password directly. Implement proper hashing.
        }
    }
}
