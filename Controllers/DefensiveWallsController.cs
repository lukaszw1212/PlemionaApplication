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
    public class DefensiveWallsController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public DefensiveWallsController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: DefensiveWalls
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.DefensiveWalls.Include(d => d.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: DefensiveWalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defensiveWalls = await _context.DefensiveWalls
                .Include(d => d.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defensiveWalls == null)
            {
                return NotFound();
            }

            return View(defensiveWalls);
        }

        // GET: DefensiveWalls/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: DefensiveWalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,DefensiveValue,MaxDefensiveValue,MaxBuildingLevel,Id,Name,VillageId")] DefensiveWalls defensiveWalls)
        {
            if (ModelState.IsValid)
            {
                _context.Add(defensiveWalls);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", defensiveWalls.VillageId);
            return View(defensiveWalls);
        }

        // GET: DefensiveWalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defensiveWalls = await _context.DefensiveWalls.FindAsync(id);
            if (defensiveWalls == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", defensiveWalls.VillageId);
            return View(defensiveWalls);
        }

        // POST: DefensiveWalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,DefensiveValue,MaxDefensiveValue,MaxBuildingLevel,Id,Name,VillageId")] DefensiveWalls defensiveWalls)
        {
            if (id != defensiveWalls.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(defensiveWalls);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefensiveWallsExists(defensiveWalls.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", defensiveWalls.VillageId);
            return View(defensiveWalls);
        }

        // GET: DefensiveWalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defensiveWalls = await _context.DefensiveWalls
                .Include(d => d.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defensiveWalls == null)
            {
                return NotFound();
            }

            return View(defensiveWalls);
        }

        // POST: DefensiveWalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var defensiveWalls = await _context.DefensiveWalls.FindAsync(id);
            if (defensiveWalls != null)
            {
                _context.DefensiveWalls.Remove(defensiveWalls);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DefensiveWallsExists(int id)
        {
            return _context.DefensiveWalls.Any(e => e.Id == id);
        }
    }
}
