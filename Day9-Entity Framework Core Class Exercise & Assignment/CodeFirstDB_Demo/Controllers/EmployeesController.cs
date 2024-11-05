using CodeFirstDB_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CodeFirstDB_Demo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MyContext _context;

        public EmployeesController(MyContext context)
        {
            _context = context;
        }

        // Index - Display list of all employees
        public IActionResult Index()
        {
            var employees = _context.Employees.Include(e => e.Department).ToList();
            return View(employees);
        }

        // Details - Display details of a specific employee by ID
        public IActionResult Details(int id)
        {
            var employee = _context.Employees.Include(e => e.Department).FirstOrDefault(x => x.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Create - Show form to create a new employee
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }

        // POST: Create (POST) - Save a new employee to the database
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = _context.Departments.ToList();
            return View(employee);
        }

        // GET: Edit - Show form to edit an existing employee
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Include(e => e.Department).FirstOrDefault(x => x.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Departments = _context.Departments.ToList();
            return View(employee);
        }

        // POST: Edit (POST) - Update an existing employee
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = _context.Employees.Find(employee.EmpId);
                if (existingEmployee == null)
                {
                    return NotFound();
                }

                existingEmployee.Name = employee.Name;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Email = employee.Email;
                existingEmployee.Designation = employee.Designation;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.DepartmentId = employee.DepartmentId;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _context.Departments.ToList();
            return View(employee);
        }


        // GET : Delete - Confirm deletion of an employee
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Include(e => e.Department).FirstOrDefault(x => x.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: DeleteConfirmed (POST) - Remove an employee from the database
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
