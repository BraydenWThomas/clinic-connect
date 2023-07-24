using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using ClinicConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicConnect.Controllers
{
    public class BookingController : Controller
    {
        private readonly ClinicConnectContext _context;

        public BookingController(ClinicConnectContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.AppointmentTypes != null ?
                        View(await _context.AppointmentTypes.ToListAsync()) :
                        Problem("Entity set 'ClinicConnectContext.AppointmentTypes'  is null.");
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppointmentTypes == null)
            {
                return NotFound();
            }

            var appointmentType = await _context.AppointmentTypes
                .FirstOrDefaultAsync(m => m.AppointmentTypeId == id);
            if (appointmentType == null)
            {
                return NotFound();
            }

            var doctors = await _context.Practitioners.ToListAsync();
            ViewData["Doctors"] = doctors;

            var appointments = await _context.Appointments.ToListAsync();
            //ViewData["Appointments"] = await _context.Appointments.ToListAsync();
            ViewData["Appointments"] = JsonSerializer.Serialize(appointments, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve });

            return View(appointmentType);
        }

        public async Task<IActionResult> Confirm(string appointmentDate, int? doctorId, int appointmentTypeId)
        {
            if (doctorId is null)
            {
                return NotFound();
            }
            int nonNullableUserId = doctorId.Value;

            var p = await _context.Practitioners.FindAsync(nonNullableUserId);

            if (p is null)
            {
                return NotFound();
            }

            // Format the appointment date string
            var appointmentDateString = DateTime.Parse(appointmentDate).ToString("yyyy-MM-dd HH:mm:ss");

            var patient = await _context.Patients.FindAsync(1);
            if (patient is null)
            {
                return NotFound();
            }

            var appointmentType = await _context.AppointmentTypes.FindAsync(appointmentTypeId);

            Appointment a = new Appointment();
            a.Date = appointmentDateString;
            a.AppointmentTypeId = appointmentTypeId;
            a.PatientId = 1;
            a.PractitionerId = nonNullableUserId;
            a.AppointmentType = appointmentType;
            a.Patient = patient;

            var patient_user = await _context.Users.FindAsync(patient.UserId);

            ViewData["Patient"] = patient_user;
            ViewData["Doctor"] = p;
            return View(a);
        }


        public async Task<IActionResult> saveToDatabase(String date, int AppointmentTypeId, int patient_id, int practitioner_id)
        {
            Console.WriteLine("saveToDatabase Called");

            var appointmentType = await _context.AppointmentTypes.FindAsync(AppointmentTypeId);

            Appointment a = new Appointment();
            a.Date = date;
            a.AppointmentTypeId = AppointmentTypeId;
            a.PatientId = 1;
            a.PractitionerId = practitioner_id;
            a.AppointmentType = appointmentType;

            var latestAppointment = await _context.Appointments
                .OrderByDescending(a => a.AppointmentId)
                .FirstOrDefaultAsync();

            int newAppointmentId = (latestAppointment != null)
                ? latestAppointment.AppointmentId + 1
                : 1;

            a.AppointmentId = newAppointmentId;
            _context.Add(a);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return NotFound();
            }
        }

    }
}
