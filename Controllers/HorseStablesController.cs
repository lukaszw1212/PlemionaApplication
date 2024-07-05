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
    public class HorseStablesController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public HorseStablesController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: HorseStables
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.HorseStable.Include(h => h.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: HorseStables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horseStable = await _context.HorseStable
                .Include(h => h.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horseStable == null)
            {
                return NotFound();
            }

            return View(horseStable);
        }

        // GET: HorseStables/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: HorseStables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,CurrentHorses,MaxHorses,MaxBuildingLevel,Id,Name,VillageId")] HorseStable horseStable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horseStable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", horseStable.VillageId);
            return View(horseStable);
        }

        // GET: HorseStables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horseStable = await _context.HorseStable.FindAsync(id);
            if (horseStable == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", horseStable.VillageId);
            return View(horseStable);
        }

        // POST: HorseStables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,CurrentHorses,MaxHorses,MaxBuildingLevel,Id,Name,VillageId")] HorseStable horseStable)
        {
            if (id != horseStable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horseStable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorseStableExists(horseStable.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", horseStable.VillageId);
            return View(horseStable);
        }

        // GET: HorseStables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horseStable = await _context.HorseStable
                .Include(h => h.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horseStable == null)
            {
                return NotFound();
            }

            return View(horseStable);
        }

        // POST: HorseStables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horseStable = await _context.HorseStable.FindAsync(id);
            if (horseStable != null)
            {
                _context.HorseStable.Remove(horseStable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorseStableExists(int id)
        {
            return _context.HorseStable.Any(e => e.Id == id);
        }
    }
}
