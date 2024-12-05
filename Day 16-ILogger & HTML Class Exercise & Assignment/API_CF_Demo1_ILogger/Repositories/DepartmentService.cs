using API_CF_Demo1.Data;
using API_CF_Demo1.Models;

namespace API_CF_Demo1.Repositories
{
    public class DepartmentService : IDepartmentService
    {
        private  MyDbContext _context;
        private MyDbContext @object;
        private readonly ILogger<DepartmentService> _logger;
        public DepartmentService(MyDbContext context,ILogger<DepartmentService> logger)
        {
            _context=context;
            _logger=logger;
        }
        public DepartmentService(MyDbContext @object)
        {
            this.@object=@object;
        }

        public List<Department> GetAllDepartments() {
            _logger.LogInformation("GetallDepartments Called");
          
            var departments =_context.Departments.ToList();
            if (departments.Count > 0)
            { return departments; }
            else
                return null;
        }

        public List<Department> SearchByName(string name)
        {
            var departments=_context.Departments.Where(x => x.Name.Contains(name)).ToList();
            return departments;
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
            catch (Exception e )
            {

                throw new Exception(e.Message);
            }
        }

        public string DeleteDepartment(int id)
        {
           if(id!=null )
            {
                var department =_context.Departments.FirstOrDefault(x => x.Id==id);
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
           if(id!=0 || id != null)
            {
                var depaertment= _context.Departments.FirstOrDefault(x => x.Id==id);
                if (depaertment != null)
                    return depaertment;
                else
                    return null;
            }
            return null;
        }

        public string UpdateDepartment(Department department)
        {
            _logger.LogInformation("Update Department Called");
            var existingDepartment =_context.Departments.FirstOrDefault(x => x.Id==department.Id);
            if(existingDepartment!= null)
            {
                existingDepartment.Name=department.Name;
                existingDepartment.DepartmentHead=department.DepartmentHead;
             //   _context.Entry(existingDepartment).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
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
