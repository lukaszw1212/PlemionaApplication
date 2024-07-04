using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProjekt;
using PlemionaApplication.Data;

namespace PlemionaApplication.Controllers
{
    public class SilosController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public SilosController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Silos
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Silo.Include(s => s.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Silos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silo = await _context.Silo
                .Include(s => s.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (silo == null)
            {
                return NotFound();
            }

            return View(silo);
        }

        // GET: Silos/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Silos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Level,VillageId")] Silo silo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(silo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", silo.VillageId);
            return View(silo);
        }

        // GET: Silos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silo = await _context.Silo.FindAsync(id);
            if (silo == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", silo.VillageId);
            return View(silo);
        }

        // POST: Silos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Level,VillageId")] Silo silo)
        {
            if (id != silo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(silo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiloExists(silo.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", silo.VillageId);
            return View(silo);
        }

        // GET: Silos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var silo = await _context.Silo
                .Include(s => s.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (silo == null)
            {
                return NotFound();
            }

            return View(silo);
        }

        // POST: Silos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var silo = await _context.Silo.FindAsync(id);
            if (silo != null)
            {
                _context.Silo.Remove(silo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiloExists(int id)
        {
            return _context.Silo.Any(e => e.Id == id);
        }
    }
}
