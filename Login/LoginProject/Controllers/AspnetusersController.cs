using System;
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
