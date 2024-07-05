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
    public class ArchersController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public ArchersController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Archers
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Archer.Include(a => a.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Archers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archer = await _context.Archer
                .Include(a => a.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archer == null)
            {
                return NotFound();
            }

            return View(archer);
        }

        // GET: Archers/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Archers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Archer archer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(archer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", archer.VillageId);
            return View(archer);
        }

        // GET: Archers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archer = await _context.Archer.FindAsync(id);
            if (archer == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", archer.VillageId);
            return View(archer);
        }

        // POST: Archers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Archer archer)
        {
            if (id != archer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(archer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArcherExists(archer.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", archer.VillageId);
            return View(archer);
        }

        // GET: Archers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var archer = await _context.Archer
                .Include(a => a.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (archer == null)
            {
                return NotFound();
            }

            return View(archer);
        }

        // POST: Archers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var archer = await _context.Archer.FindAsync(id);
            if (archer != null)
            {
                _context.Archer.Remove(archer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArcherExists(int id)
        {
            return _context.Archer.Any(e => e.Id == id);
        }
    }
}
