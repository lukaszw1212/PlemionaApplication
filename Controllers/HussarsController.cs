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
    public class HussarsController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public HussarsController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Hussars
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Hussar.Include(h => h.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Hussars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hussar = await _context.Hussar
                .Include(h => h.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hussar == null)
            {
                return NotFound();
            }

            return View(hussar);
        }

        // GET: Hussars/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Hussars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Hussar hussar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hussar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", hussar.VillageId);
            return View(hussar);
        }

        // GET: Hussars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hussar = await _context.Hussar.FindAsync(id);
            if (hussar == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", hussar.VillageId);
            return View(hussar);
        }

        // POST: Hussars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Id,Name,CurrentHP,MaxHP,AttackSpeed,DamageType,Damage,PhysicalResistance,RangeResistance,VillageId")] Hussar hussar)
        {
            if (id != hussar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hussar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HussarExists(hussar.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", hussar.VillageId);
            return View(hussar);
        }

        // GET: Hussars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hussar = await _context.Hussar
                .Include(h => h.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hussar == null)
            {
                return NotFound();
            }

            return View(hussar);
        }

        // POST: Hussars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hussar = await _context.Hussar.FindAsync(id);
            if (hussar != null)
            {
                _context.Hussar.Remove(hussar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HussarExists(int id)
        {
            return _context.Hussar.Any(e => e.Id == id);
        }
    }
}
