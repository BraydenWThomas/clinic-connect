using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoginProject.Areas.Identity.Data;
using LoginProject.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LoginProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AspnetrolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AspnetrolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aspnetroles
        public async Task<IActionResult> Index()
        {
              return _context.Aspnetroles != null ? 
                          View(await _context.Aspnetroles.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Aspnetroles'  is null.");
        }

        // GET: Aspnetroles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Aspnetroles == null)
            {
                return NotFound();
            }

            var aspnetrole = await _context.Aspnetroles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspnetrole == null)
            {
                return NotFound();
            }

            return View(aspnetrole);
        }

        // GET: Aspnetroles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aspnetroles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] Aspnetrole aspnetrole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspnetrole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspnetrole);
        }

        // GET: Aspnetroles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Aspnetroles == null)
            {
                return NotFound();
            }

            var aspnetrole = await _context.Aspnetroles.FindAsync(id);
            if (aspnetrole == null)
            {
                return NotFound();
            }
            return View(aspnetrole);
        }

        // POST: Aspnetroles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] Aspnetrole aspnetrole)
        {
            if (id != aspnetrole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspnetrole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspnetroleExists(aspnetrole.Id))
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
            return View(aspnetrole);
        }

        // GET: Aspnetroles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Aspnetroles == null)
            {
                return NotFound();
            }

            var aspnetrole = await _context.Aspnetroles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspnetrole == null)
            {
                return NotFound();
            }

            return View(aspnetrole);
        }

        // POST: Aspnetroles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Aspnetroles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Aspnetroles'  is null.");
            }
            var aspnetrole = await _context.Aspnetroles.FindAsync(id);
            if (aspnetrole != null)
            {
                _context.Aspnetroles.Remove(aspnetrole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspnetroleExists(string id)
        {
          return (_context.Aspnetroles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
