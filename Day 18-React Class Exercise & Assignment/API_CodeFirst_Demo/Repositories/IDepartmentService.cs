using API_CodeFirst_Demo.Models;

namespace API_CodeFirst_Demo.Repositories
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
