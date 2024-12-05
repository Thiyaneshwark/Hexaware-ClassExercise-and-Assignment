using API_CF_Demo1.Models;

namespace API_CF_Demo1.Repositories
{
    public interface IDepartmentService
    {
        List<Department> GetAllDepartments();
        Department GetDepartmentById(int id);
        int AddNewDepartment(Department department);
        string UpdateDepartment(Department department); 
        string DeleteDepartment(int id);
        List<Department> SearchByName(string name);
    }
}
