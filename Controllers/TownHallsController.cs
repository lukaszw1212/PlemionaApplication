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
    public class TownHallsController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public TownHallsController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: TownHalls
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.TownHall.Include(t => t.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: TownHalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var townHall = await _context.TownHall
                .Include(t => t.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (townHall == null)
            {
                return NotFound();
            }

            return View(townHall);
        }

        // GET: TownHalls/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: TownHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,MaxBuildingLevel,GenerateGoldPerTime,MaxGoldPerTime,Time,Id,Name,VillageId")] TownHall townHall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(townHall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", townHall.VillageId);
            return View(townHall);
        }

        // GET: TownHalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var townHall = await _context.TownHall.FindAsync(id);
            if (townHall == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", townHall.VillageId);
            return View(townHall);
        }

        // POST: TownHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,MaxBuildingLevel,GenerateGoldPerTime,MaxGoldPerTime,Time,Id,Name,VillageId")] TownHall townHall)
        {
            if (id != townHall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(townHall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TownHallExists(townHall.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", townHall.VillageId);
            return View(townHall);
        }

        // GET: TownHalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var townHall = await _context.TownHall
                .Include(t => t.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (townHall == null)
            {
                return NotFound();
            }

            return View(townHall);
        }

        // POST: TownHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var townHall = await _context.TownHall.FindAsync(id);
            if (townHall != null)
            {
                _context.TownHall.Remove(townHall);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TownHallExists(int id)
        {
            return _context.TownHall.Any(e => e.Id == id);
        }
    }
}
