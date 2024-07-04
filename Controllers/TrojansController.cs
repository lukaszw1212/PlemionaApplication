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
    public class TrojansController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public TrojansController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Trojans
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Trojan.Include(t => t.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Trojans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trojan = await _context.Trojan
                .Include(t => t.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trojan == null)
            {
                return NotFound();
            }

            return View(trojan);
        }

        // GET: Trojans/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Trojans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Trojan trojan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trojan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", trojan.VillageId);
            return View(trojan);
        }

        // GET: Trojans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trojan = await _context.Trojan.FindAsync(id);
            if (trojan == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", trojan.VillageId);
            return View(trojan);
        }

        // POST: Trojans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Trojan trojan)
        {
            if (id != trojan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trojan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrojanExists(trojan.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", trojan.VillageId);
            return View(trojan);
        }

        // GET: Trojans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trojan = await _context.Trojan
                .Include(t => t.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trojan == null)
            {
                return NotFound();
            }

            return View(trojan);
        }

        // POST: Trojans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trojan = await _context.Trojan.FindAsync(id);
            if (trojan != null)
            {
                _context.Trojan.Remove(trojan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrojanExists(int id)
        {
            return _context.Trojan.Any(e => e.Id == id);
        }
    }
}
