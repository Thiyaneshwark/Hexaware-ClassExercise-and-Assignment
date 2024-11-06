using Microsoft.EntityFrameworkCore;
using WebApplication_DatabaseFirst_Demo.Models;

namespace WebApplication_DatabaseFirst_Demo.Repositories
{
    public class CustomerServices : ICustomerServices
    {
        private readonly BikeStoresContext _context;  // Assuming BikeStoresContext is your DbContext

        public CustomerServices(BikeStoresContext dbContext)
        {
            _context = dbContext;
        }

        // Add a new customer to the database
        public int AddNewCustomer(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    return customer.CustomerId;  // Return the CustomerId of the newly added customer
                }
                return 0;  // Return 0 if the customer is null
            }
            catch (Exception e)
            {
                throw new Exception("Error adding customer: " + e.Message);
            }
        }

        // Delete a customer by ID
        public string DeleteCustomer(int id)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    _context.SaveChanges();
                    return $"Customer with ID {id} has been deleted successfully.";
                }
                return $"Customer with ID {id} not found.";
            }
            catch (Exception e)
            {
                throw new Exception("Error deleting customer: " + e.Message);
            }
        }

        // Get all customers
        public List<Customer> GetAllCustomers()
        {
            try
            {
                var customers = _context.Customers.ToList();
                return customers.Any() ? customers : new List<Customer>();
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving customers: " + e.Message);
            }
        }

        // Get customer by ID
        public Customer GetCustomerById(int id)
        {
            try
            {
                var customer = _context.Customers.FirstOrDefault(x => x.CustomerId == id);
                return customer;
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving customer by ID: " + e.Message);
            }
        }

        // Update customer details
        public string UpdateCustomer(Customer customer)
        {
            try
            {
                var existingCustomer = _context.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                if (existingCustomer != null)
                {
                    // Update customer details
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Active = customer.Active;

                    // Mark the entity as modified and save changes
                    _context.Entry(existingCustomer).State = EntityState.Modified;
                    _context.SaveChanges();

                    return "Customer record updated successfully.";
                }
                return "Customer not found.";
            }
            catch (Exception e)
            {
                throw new Exception("Error updating customer: " + e.Message);
            }
        }
    }
}







