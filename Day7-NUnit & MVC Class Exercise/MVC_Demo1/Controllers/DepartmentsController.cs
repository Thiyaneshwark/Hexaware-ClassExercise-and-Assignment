using Microsoft.AspNetCore.Mvc;
using MVC_Demo1.Models;

namespace MVC_Demo1.Controllers
{
    public class Departmentscontroller : Controller
    {

        public class DepartmentsController
        {
        }
        public IActionResult Index()
        {
            Department department = new Department() { DepartmentId = 1, Name = "HR", Location = "India" };
            ViewData["Message"] = "Welcome to web app Development using MVC";
            return View(department);
        }
        public IActionResult Details()
        {
            ViewBag.msg1 = "This is the example for transferring data from controller to view using ViewBag";
            return View();
        }
    }
}