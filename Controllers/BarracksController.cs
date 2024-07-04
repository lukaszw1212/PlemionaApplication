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
    public class BarracksController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public BarracksController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Barracks
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Barracks.Include(b => b.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Barracks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barracks = await _context.Barracks
                .Include(b => b.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barracks == null)
            {
                return NotFound();
            }

            return View(barracks);
        }

        // GET: Barracks/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Barracks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Cost,MaxBuildingLevel,Id,Name,VillageId")] Barracks barracks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(barracks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", barracks.VillageId);
            return View(barracks);
        }

        // GET: Barracks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barracks = await _context.Barracks.FindAsync(id);
            if (barracks == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", barracks.VillageId);
            return View(barracks);
        }

        // POST: Barracks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Cost,MaxBuildingLevel,Id,Name,VillageId")] Barracks barracks)
        {
            if (id != barracks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barracks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarracksExists(barracks.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", barracks.VillageId);
            return View(barracks);
        }

        // GET: Barracks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barracks = await _context.Barracks
                .Include(b => b.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barracks == null)
            {
                return NotFound();
            }

            return View(barracks);
        }

        // POST: Barracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var barracks = await _context.Barracks.FindAsync(id);
            if (barracks != null)
            {
                _context.Barracks.Remove(barracks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarracksExists(int id)
        {
            return _context.Barracks.Any(e => e.Id == id);
        }
    }
}
