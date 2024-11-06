using DatabaseFirst_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace DatabaseFirst_Demo.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly BikeStoresContext _context;
        public CategoriesController(BikeStoresContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Details(int id)
        {
            var categories = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (categories == null)
                return NotFound();
            else
            return View(categories);
        }
    }
}
