using Assignment_CodeFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Assignment_CodeFirstApproach.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly MyContext _context;

        public DoctorsController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var doctors = _context.Doctors.Include(d => d.Hospital).ToList();
            return View(doctors);
        }
        public IActionResult Details(int id)
        {
            var doctor = _context.Doctors.Include(d => d.Hospital).Include(d => d.Appointments).ThenInclude(a => a.Patient).FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }


        public IActionResult Create()
        {
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate the dropdown in case of an error.
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalId", "Name");
            return View(doctor);
        }


        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewBag.HospitalId = new SelectList(_context.Hospitals, "HospitalId", "Name");
            return View(doctor);
        }

        [HttpPost]
        public IActionResult Edit(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                _context.Doctors.Update(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HospitalId = new SelectList(_context.Hospitals, "HospitalId", "Name");
            return View(doctor);
        }

        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Include(d => d.Hospital).FirstOrDefault(d => d.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}

