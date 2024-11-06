using Microsoft.AspNetCore.Mvc;
using MVC_Demo1.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Demo1.Controllers
{
    public class DepartmentsController : Controller
    {
        static List<Department> departments = new List<Department>()
        {
            new Department(){ DepartmentId=1, Name="HR", Location="Delhi" },
            new Department(){ DepartmentId=2, Name="IT", Location="Bangalore" },
            new Department(){ DepartmentId=3, Name="Admin", Location="Chennai" },
            new Department(){ DepartmentId=4, Name="Transport", Location="Kolkata" }
        };

        public IActionResult Index()
        {
            return View(departments);
        }

        public IActionResult Details(int id)
        {
            var department = departments.FirstOrDefault(x => x.DepartmentId == id);
            if (department != null)
                return View(department);
            else
                return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                department.DepartmentId = departments.Count > 0 ? departments.Max(d => d.DepartmentId) + 1 : 1;
                departments.Add(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = departments.FirstOrDefault(x => x.DepartmentId == id);
            if (department != null)
                return View(department);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Department updateDepartment)
        {
            if (ModelState.IsValid)
            {
                var department = departments.FirstOrDefault(x => x.DepartmentId == updateDepartment.DepartmentId);
                if (department != null)
                {
                    department.Name = updateDepartment.Name;
                    department.Location = updateDepartment.Location;
                }
                return RedirectToAction("Index");
            }
            return View(updateDepartment);
        }

        public IActionResult Delete(int id)
        {
            var department = departments.FirstOrDefault(x => x.DepartmentId == id);
            if (department != null)
                return View(department);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = departments.FirstOrDefault(x => x.DepartmentId == id);
            if (department != null)
                departments.Remove(department);
            return RedirectToAction("Index");
        }
    }
}
