using DatabaseFirst_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DatabaseFirst_Demo.Controllers
{
    public class Products1Controller : Controller
    {
        private readonly BikeStoresContext _context;

        public Products1Controller(BikeStoresContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            var products = _context.Products1.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProductName.Contains(searchString));
            }

            return View(products.ToList());
        }
    }
}
