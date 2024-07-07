using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlemionaApplication.Data;
using PlemionaApplication.Models;

namespace PlemionaApplication.Controllers
{
    public class FractionsController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public FractionsController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Fractions
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Fractions.Include(f => f.GuildMaster);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Fractions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fraction = await _context.Fractions
                .Include(f => f.GuildMaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fraction == null)
            {
                return NotFound();
            }

            return View(fraction);
        }

        // GET: Fractions/Create
        public IActionResult Create()
        {
            ViewData["GuildMasterId"] = new SelectList(_context.Players, "Id", "Name");
            return View();
        }

        // POST: Fractions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GuildMasterId")] Fraction fraction)
        {
            if (ModelState.IsValid)
            {
                ViewData["GuildMasterId"] = new SelectList(_context.Players, "Id", "Name", fraction.GuildMasterId);
                return View(fraction);
            }
            else
            {
                _context.Add(fraction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
        }

        // GET: Fractions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fraction = await _context.Fractions.FindAsync(id);
            if (fraction == null)
            {
                return NotFound();
            }
            ViewData["GuildMasterId"] = new SelectList(_context.Players, "Id", "Name", fraction.GuildMasterId);
            return View(fraction);
        }

        // POST: Fractions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GuildMasterId")] Fraction fraction)
        {
            if (id != fraction.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(fraction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FractionExists(fraction.Id))
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
            ViewData["GuildMasterId"] = new SelectList(_context.Players, "Id", "Name", fraction.GuildMasterId);
            return View(fraction);
        }

        // GET: Fractions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fraction = await _context.Fractions
                .Include(f => f.GuildMaster)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fraction == null)
            {
                return NotFound();
            }

            return View(fraction);
        }

        // POST: Fractions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fraction = await _context.Fractions.FindAsync(id);
            if (fraction != null)
            {
                _context.Fractions.Remove(fraction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FractionExists(int id)
        {
            return _context.Fractions.Any(e => e.Id == id);
        }
    }
}
