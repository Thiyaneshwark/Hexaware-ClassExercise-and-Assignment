using WebApplication_DatabaseFirst_Demo.Models;

namespace WebApplication_DatabaseFirst_Demo.Repositories
{
    public interface IEmployeeServices
    {
        List<Employee> GetAllEmployees();  // Renamed to plural to reflect multiple employees
        int AddNewEmployee(Employee employee);
        Employee GetEmployeeById(int id);
        string UpdateEmployee(Employee employee);
        string DeleteEmployee(int id);
    }
}