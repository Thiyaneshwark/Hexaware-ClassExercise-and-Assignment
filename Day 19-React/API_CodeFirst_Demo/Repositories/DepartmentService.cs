using API_CodeFirst_Demo.Data;
using API_CodeFirst_Demo.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace API_CodeFirst_Demo.Repositories
{
    public class DepartmentService : IDepartmentService
    {
        private MyDbContext _context;
        public DepartmentService(MyDbContext context)
        {
            _context = context;
        }
        public List<Department> GetAllDepartments()
        {
            var departments = _context.Departments.ToList();
            if (departments.Count > 0)
            {
                return departments;
            }
            else
                return null;
        }
        public List<Department> SearchByName(string name)
        {
            var department = _context.Departments.Where(x => x.Name.Contains(name)).ToList();
            return department;
        }

        public int AddNewDepartment(Department department)
        {
            try
            {
                if (department != null)
                {
                    _context.Departments.Add(department);
                    _context.SaveChanges();
                    return department.Id;
                }
                else return 0;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public string DeleteDepartment(int id)
        {
            if (id != null)
            {
                var department = _context.Departments.FirstOrDefault(x => x.Id == id);
                if (department != null)
                {
                    _context.Departments.Remove(department);
                    _context.SaveChanges();
                    return "the given Department id " + id + " Removed";
                }
                else
                    return "Something went wrong with deletion";

            }
            return "Id should not be null or zero";
        }

        public Department GetDepartmentById(int id)
        {
            if (id != 0 || id != null)
            {
                var depaertment = _context.Departments.FirstOrDefault(x => x.Id == id);
                if (depaertment != null)
                    return depaertment;
                else
                    return null;
            }
            return null;
        }

        public string UpdateDepartment(Department department)
        {
            var existingDepartment = _context.Departments.FirstOrDefault(x => x.Id == department.Id);
            if (existingDepartment != null)
            {
                existingDepartment.Name = department.Name;
                existingDepartment.DepartmentHead = department.DepartmentHead;
                _context.Entry(existingDepartment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return "Record Updated successfully";
            }
            else
            {
                return "something went wrong while update";
            }
        }
    }
}