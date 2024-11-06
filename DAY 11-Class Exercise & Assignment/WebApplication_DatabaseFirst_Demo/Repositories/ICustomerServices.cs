using WebApplication_DatabaseFirst_Demo.Models;

namespace WebApplication_DatabaseFirst_Demo.Repositories
{
    public interface ICustomerServices
    {
        List<Customer> GetAllCustomers();
        int AddNewCustomer(Customer customer);
        Customer GetCustomerById(int id);
        string UpdateCustomer(Customer customer);
        string DeleteCustomer(int id);
    }
}
