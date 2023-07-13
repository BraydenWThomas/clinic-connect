using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicConnect.Models;

namespace ClinicConnect.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ClinicConnectContext _context;

        public DashboardController(ClinicConnectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch appointments or any data required for the scheduler view
            List<Appointment> appointments = _context.Appointments
                .Include(a => a.AppointmentType)
                .Include(a => a.Patient)
                .Include(a => a.Practitioner)
                .ToList();

            return View(appointments);
        }

        // Other actions for the scheduler functionality can be added here

    }
}