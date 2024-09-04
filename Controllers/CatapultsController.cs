using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlemionaApplication.Data;
using MiniProjekt;

namespace PlemionaApplication.Controllers
{
    public class CatapultsController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public CatapultsController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Catapults
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Catapult.Include(c => c.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Catapults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catapult = await _context.Catapult
                .Include(c => c.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catapult == null)
            {
                return NotFound();
            }

            return View(catapult);
        }

        // GET: Catapults/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Catapults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Catapult catapult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catapult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", catapult.VillageId);
            return View(catapult);
        }

        // GET: Catapults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catapult = await _context.Catapult.FindAsync(id);
            if (catapult == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", catapult.VillageId);
            return View(catapult);
        }

        // POST: Catapults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Catapult catapult)
        {
            if (id != catapult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catapult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatapultExists(catapult.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", catapult.VillageId);
            return View(catapult);
        }

        // GET: Catapults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catapult = await _context.Catapult
                .Include(c => c.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catapult == null)
            {
                return NotFound();
            }

            return View(catapult);
        }

        // POST: Catapults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catapult = await _context.Catapult.FindAsync(id);
            if (catapult != null)
            {
                _context.Catapult.Remove(catapult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatapultExists(int id)
        {
            return _context.Catapult.Any(e => e.Id == id);
        }
    }
}
