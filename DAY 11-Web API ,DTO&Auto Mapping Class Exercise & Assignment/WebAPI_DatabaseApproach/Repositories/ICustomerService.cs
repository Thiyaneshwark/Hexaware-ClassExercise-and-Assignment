using WebAPI_DatabaseApproach.Models;
using System.Collections.Generic;

namespace WebAPI_DatabaseApproach.Repositories
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        int AddNewCustomer(Customer customer);
        string UpdateCustomer(Customer customer);
        string DeleteCustomer(int id);
    }
}
