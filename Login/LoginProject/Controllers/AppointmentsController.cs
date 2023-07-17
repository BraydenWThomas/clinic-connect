using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicConnect.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClinicConnect.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ClinicConnectContext _context;

        public AppointmentsController(ClinicConnectContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var clinicConnectContext = _context.Appointments.Include(a => a.AppointmentType).Include(a => a.Patient).Include(a => a.Practitioner);
            return View(await clinicConnectContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.AppointmentType)
                .Include(a => a.Patient)
                .Include(a => a.Practitioner)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["AppointmentTypeId"] = new SelectList(_context.AppointmentTypes, "AppointmentTypeId", "AppointmentTypeId");
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            ViewData["PractitionerId"] = new SelectList(_context.Practitioners, "PractitionerId", "PractitionerId");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,Date,AppointmentTypeId,PatientId,PractitionerId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentTypeId"] = new SelectList(_context.AppointmentTypes, "AppointmentTypeId", "AppointmentTypeId", appointment.AppointmentTypeId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", appointment.PatientId);
            ViewData["PractitionerId"] = new SelectList(_context.Practitioners, "PractitionerId", "PractitionerId", appointment.PractitionerId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["AppointmentTypeId"] = new SelectList(_context.AppointmentTypes, "AppointmentTypeId", "AppointmentTypeId", appointment.AppointmentTypeId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", appointment.PatientId);
            ViewData["PractitionerId"] = new SelectList(_context.Practitioners, "PractitionerId", "PractitionerId", appointment.PractitionerId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,Date,AppointmentTypeId,PatientId,PractitionerId")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentTypeId"] = new SelectList(_context.AppointmentTypes, "AppointmentTypeId", "AppointmentTypeId", appointment.AppointmentTypeId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", appointment.PatientId);
            ViewData["PractitionerId"] = new SelectList(_context.Practitioners, "PractitionerId", "PractitionerId", appointment.PractitionerId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.AppointmentType)
                .Include(a => a.Patient)
                .Include(a => a.Practitioner)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'ClinicConnectContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
