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
    public class ArmoriesController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public ArmoriesController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Armories
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Armory.Include(a => a.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Armories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armory = await _context.Armory
                .Include(a => a.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armory == null)
            {
                return NotFound();
            }

            return View(armory);
        }

        // GET: Armories/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Armories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Cost,MaxBuildingLevel,Id,Name,VillageId")] Armory armory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", armory.VillageId);
            return View(armory);
        }

        // GET: Armories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armory = await _context.Armory.FindAsync(id);
            if (armory == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", armory.VillageId);
            return View(armory);
        }

        // POST: Armories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Cost,MaxBuildingLevel,Id,Name,VillageId")] Armory armory)
        {
            if (id != armory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmoryExists(armory.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", armory.VillageId);
            return View(armory);
        }

        // GET: Armories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armory = await _context.Armory
                .Include(a => a.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armory == null)
            {
                return NotFound();
            }

            return View(armory);
        }

        // POST: Armories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var armory = await _context.Armory.FindAsync(id);
            if (armory != null)
            {
                _context.Armory.Remove(armory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmoryExists(int id)
        {
            return _context.Armory.Any(e => e.Id == id);
        }
    }
}
