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
    public class WarriorsController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public WarriorsController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Warriors
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Warrior.Include(w => w.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Warriors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warrior = await _context.Warrior
                .Include(w => w.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warrior == null)
            {
                return NotFound();
            }

            return View(warrior);
        }

        // GET: Warriors/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Warriors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Warrior warrior)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warrior);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", warrior.VillageId);
            return View(warrior);
        }

        // GET: Warriors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warrior = await _context.Warrior.FindAsync(id);
            if (warrior == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", warrior.VillageId);
            return View(warrior);
        }

        // POST: Warriors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Warrior warrior)
        {
            if (id != warrior.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warrior);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarriorExists(warrior.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", warrior.VillageId);
            return View(warrior);
        }

        // GET: Warriors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warrior = await _context.Warrior
                .Include(w => w.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warrior == null)
            {
                return NotFound();
            }

            return View(warrior);
        }

        // POST: Warriors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warrior = await _context.Warrior.FindAsync(id);
            if (warrior != null)
            {
                _context.Warrior.Remove(warrior);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarriorExists(int id)
        {
            return _context.Warrior.Any(e => e.Id == id);
        }
    }
}
