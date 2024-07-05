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
    public class SawmillsController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public SawmillsController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Sawmills
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Sawmill.Include(s => s.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Sawmills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sawmill = await _context.Sawmill
                .Include(s => s.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sawmill == null)
            {
                return NotFound();
            }

            return View(sawmill);
        }

        // GET: Sawmills/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Sawmills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,GenerateWoodPerTime,MaxWoodPerTime,Time,MaxBuildingLevel,Id,Name,VillageId")] Sawmill sawmill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sawmill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", sawmill.VillageId);
            return View(sawmill);
        }

        // GET: Sawmills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sawmill = await _context.Sawmill.FindAsync(id);
            if (sawmill == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", sawmill.VillageId);
            return View(sawmill);
        }

        // POST: Sawmills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,GenerateWoodPerTime,MaxWoodPerTime,Time,MaxBuildingLevel,Id,Name,VillageId")] Sawmill sawmill)
        {
            if (id != sawmill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sawmill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SawmillExists(sawmill.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", sawmill.VillageId);
            return View(sawmill);
        }

        // GET: Sawmills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sawmill = await _context.Sawmill
                .Include(s => s.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sawmill == null)
            {
                return NotFound();
            }

            return View(sawmill);
        }

        // POST: Sawmills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sawmill = await _context.Sawmill.FindAsync(id);
            if (sawmill != null)
            {
                _context.Sawmill.Remove(sawmill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SawmillExists(int id)
        {
            return _context.Sawmill.Any(e => e.Id == id);
        }
    }
}
