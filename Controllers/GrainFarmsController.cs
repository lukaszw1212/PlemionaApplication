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
    public class GrainFarmsController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public GrainFarmsController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: GrainFarms
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.GrainFarm.Include(g => g.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: GrainFarms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grainFarm = await _context.GrainFarm
                .Include(g => g.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grainFarm == null)
            {
                return NotFound();
            }

            return View(grainFarm);
        }

        // GET: GrainFarms/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: GrainFarms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,GenerateWheatPerTime,MaxFarmPerTime,MaxBuildingLevel,Time,Id,Name,VillageId")] GrainFarm grainFarm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grainFarm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", grainFarm.VillageId);
            return View(grainFarm);
        }

        // GET: GrainFarms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grainFarm = await _context.GrainFarm.FindAsync(id);
            if (grainFarm == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", grainFarm.VillageId);
            return View(grainFarm);
        }

        // POST: GrainFarms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,GenerateWheatPerTime,MaxFarmPerTime,MaxBuildingLevel,Time,Id,Name,VillageId")] GrainFarm grainFarm)
        {
            if (id != grainFarm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grainFarm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrainFarmExists(grainFarm.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", grainFarm.VillageId);
            return View(grainFarm);
        }

        // GET: GrainFarms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grainFarm = await _context.GrainFarm
                .Include(g => g.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grainFarm == null)
            {
                return NotFound();
            }

            return View(grainFarm);
        }

        // POST: GrainFarms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grainFarm = await _context.GrainFarm.FindAsync(id);
            if (grainFarm != null)
            {
                _context.GrainFarm.Remove(grainFarm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrainFarmExists(int id)
        {
            return _context.GrainFarm.Any(e => e.Id == id);
        }
    }
}
