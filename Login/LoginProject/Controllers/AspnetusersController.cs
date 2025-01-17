﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoginProject.Areas.Identity.Data;
using LoginProject.Models;

namespace LoginProject.Controllers
{
    public class AspnetusersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AspnetusersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aspnetusers
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Aspnetusers != null ? 
        //                  View(await _context.Aspnetusers.ToListAsync()) :
        //                  Problem("Entity set 'ApplicationDbContext.Aspnetusers'  is null.");
        //}

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
        
            var Aspnetusers = from s in _context.Aspnetusers select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Aspnetusers = Aspnetusers.Where(s => s.FirstName.Contains(searchString)|| s.LastName.Contains(searchString) || s.Email.Contains(searchString));
            }
            return View(await Aspnetusers.AsNoTracking().ToListAsync());
        }

        // GET: Aspnetusers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Aspnetusers == null)
            {
                return NotFound();
            }

            var aspnetusers = await _context.Aspnetusers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspnetusers == null)
            {
                return NotFound();
            }

            return View(aspnetusers);
        }

        // GET: Aspnetusers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aspnetusers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,IsStaff,Email")] Aspnetusers aspnetusers)
        {
            if (ModelState.IsValid)
            {
                //ModelState.SetModelValue("");
                _context.Add(aspnetusers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspnetusers);
        }

        // GET: Aspnetusers/AddStaffRole
        public IActionResult AddStaffRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStaffRole(string email)
        {
            // Find the user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                // Update the boolean variable
                user.Staff = true; 

                // Update the user in the database
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Successful update

                TempData["SuccessMessage"] = "Staff role added successfully.";
                ViewBag.Message = TempData["SuccessMessage"];
                return View(); // Redirect to a success page
            }

            // If user is not found
            TempData["ErrorMessage"] = "User not found.";
            ViewBag.Message = TempData["ErrorMessage"];
            return View(); // Redirect to an error page or home page
        }

        // GET: Aspnetusers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Aspnetusers == null)
            {
                return NotFound();
            }

            var aspnetusers = await _context.Aspnetusers.FindAsync(id);
            if (aspnetusers == null)
            {
                return NotFound();
            }
            return View(aspnetusers);
        }

        // POST: Aspnetusers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,IsStaff,Email")] Aspnetusers aspnetusers)
        {
            if (id != aspnetusers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspnetusers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspnetusersExists(aspnetusers.Id))
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
            return View(aspnetusers);
        }


        public async Task<IActionResult> RemoveStaffRole(string id)
        {
            if (id == null || _context.Aspnetusers == null)
            {
                return NotFound();
            }

            var aspnetusers = await _context.Aspnetusers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspnetusers == null)
            {
                return NotFound();
            }

            return View(aspnetusers);
        }

        [HttpPost, ActionName("RemoveStaffRole")]
        public async Task<IActionResult> RemoveStaffRoleConfirmed(string id)
        {
            // Find the user by ID
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            

            if (user != null)
            {
                // Update the staff property
                user.Staff = false; 

                // Update the user in the database
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Successful update
                return RedirectToAction("Index"); // Redirect to a success page
            }

            // If user is not found
            return RedirectToAction("Index"); // Redirect to an error page or home page
        }


        // GET: Aspnetusers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Aspnetusers == null)
            {
                return NotFound();
            }

            var aspnetusers = await _context.Aspnetusers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspnetusers == null)
            {
                return NotFound();
            }

            return View(aspnetusers);
        }

        // POST: Aspnetusers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Aspnetusers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Aspnetusers'  is null.");
            }
            var aspnetusers = await _context.Aspnetusers.FindAsync(id);
            if (aspnetusers != null)
            {
                _context.Aspnetusers.Remove(aspnetusers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspnetusersExists(string id)
        {
          return (_context.Aspnetusers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
