using Assignment_CodeFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment_CodeFirstApproach.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly MyContext _context;

        public HospitalsController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hospitals = _context.Hospitals.ToList();
            return View(hospitals);
        }

        public IActionResult Details(int id)
        {
            var hospital = _context.Hospitals.Include(h => h.Doctors).FirstOrDefault(h => h.HospitalId == id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        // GET: Hospital/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hospital/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hospital hospital)
        {
            if (!ModelState.IsValid)
            {
                _context.Hospitals.Add(hospital);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hospital);
        }


        public IActionResult Edit(int id)
        {
            var hospital = _context.Hospitals.Find(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hospital hospital)
        {
            if (!ModelState.IsValid)
            {
                _context.Hospitals.Update(hospital);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hospital);
        }

        public IActionResult Delete(int id)
        {
            var hospital = _context.Hospitals.Find(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var hospital = _context.Hospitals.Find(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
