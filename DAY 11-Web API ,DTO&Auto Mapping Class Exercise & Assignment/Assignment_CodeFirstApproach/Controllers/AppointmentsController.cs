using Assignment_CodeFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Assignment_CodeFirstApproach.Controllers

{
    public class AppointmentsController : Controller
    {
        private readonly MyContext _context;

        public AppointmentsController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointments = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).ToList();
            return View(appointments);
        }
        public IActionResult Details(int id)
        {
            var appointment = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefault(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        public IActionResult Create()
        {
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "Name");
            ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Repopulate the dropdowns in case of an error.
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "Name");
            ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "Name");
            return View(appointment);
        }


        public IActionResult Edit(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewBag.DoctorId = new SelectList(_context.Doctors, "DoctorId", "Name");
            ViewBag.PatientId = new SelectList(_context.Patients, "PatientId", "Name");
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(_context.Doctors, "DoctorId", "Name");
            ViewBag.PatientId = new SelectList(_context.Patients, "PatientId", "Name");
            return View(appointment);
        }

        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.Include(a => a.Doctor).Include(a => a.Patient).FirstOrDefault(a => a.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}

