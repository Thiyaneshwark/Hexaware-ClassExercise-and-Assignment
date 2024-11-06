using CodeFirstDB_Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstDB_Demo.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly MyContext _context;
        public DepartmentsController(MyContext context)
        {
            _context = context;
        }
        //Index
        public IActionResult Index()
        {
            var departments = _context.Departments.ToList();
            return View(departments);
        }
        //Details
        public IActionResult Details(int id)
        {
            var department = _context.Departments.FirstOrDefault(x => x.DepartmentId == id);
            return View(department);
        }
        //Edit
        public IActionResult Edit(int id)
        {
            var department = _context.Departments.FirstOrDefault(x => x.DepartmentId == id);
            return View(department);
        }
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (department != null)
            {
                _context.Entry(department).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        //Create
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        //Delete
        public IActionResult Delete(int id)
        {
            var department = _context.Departments.FirstOrDefault(x => x.DepartmentId == id);
            return View(department);
        }
        [HttpPost("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _context.Departments.FirstOrDefault(x => x.DepartmentId == id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}