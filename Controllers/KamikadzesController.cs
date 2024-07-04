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
    public class KamikadzesController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public KamikadzesController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Kamikadzes
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Kamikadze.Include(k => k.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Kamikadzes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kamikadze = await _context.Kamikadze
                .Include(k => k.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kamikadze == null)
            {
                return NotFound();
            }

            return View(kamikadze);
        }

        // GET: Kamikadzes/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Kamikadzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Kamikadze kamikadze)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kamikadze);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", kamikadze.VillageId);
            return View(kamikadze);
        }

        // GET: Kamikadzes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kamikadze = await _context.Kamikadze.FindAsync(id);
            if (kamikadze == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", kamikadze.VillageId);
            return View(kamikadze);
        }

        // POST: Kamikadzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Kamikadze kamikadze)
        {
            if (id != kamikadze.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kamikadze);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KamikadzeExists(kamikadze.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", kamikadze.VillageId);
            return View(kamikadze);
        }

        // GET: Kamikadzes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kamikadze = await _context.Kamikadze
                .Include(k => k.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kamikadze == null)
            {
                return NotFound();
            }

            return View(kamikadze);
        }

        // POST: Kamikadzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kamikadze = await _context.Kamikadze.FindAsync(id);
            if (kamikadze != null)
            {
                _context.Kamikadze.Remove(kamikadze);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KamikadzeExists(int id)
        {
            return _context.Kamikadze.Any(e => e.Id == id);
        }
    }
}
