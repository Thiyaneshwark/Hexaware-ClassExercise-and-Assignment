using WebAPI_DatabaseApproach.Data;
using WebAPI_DatabaseApproach.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI_DatabaseApproach.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly MyDbContext _context;

        public CustomerService(MyDbContext context)
        {
            _context = context;
        }

        public List<Customer> GetAllCustomers()
        {
            var customer = _context.Customers.ToList();
            if (customer.Count > 0)
            {
                return customer;
            }
            else
                return null;
        }

        public Customer GetCustomerById(int id)
        {
            if (id == 0) return null;

            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            return customer;
        }

        public int AddNewCustomer(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    return customer.CustomerId;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding new customer: " + ex.Message);
            }
        }

        public string UpdateCustomer(Customer customer)
        {
            var existingCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
                existingCustomer.Password = customer.Password;

                _context.Entry(existingCustomer).State = EntityState.Modified;
                _context.SaveChanges();
                return "Customer updated successfully";
            }
            return "Customer not found";
        }

        public string DeleteCustomer(int id)
        {
            if (id == 0) return "Invalid customer ID";

            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return $"Customer with ID {id} has been removed";
            }
            return "Customer not found";
        }
    }
}
