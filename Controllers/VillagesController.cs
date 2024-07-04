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
    public class VillagesController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public VillagesController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: Villages
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Villages.Include(v => v.Player);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Villages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var village = await _context.Villages
                .Include(v => v.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (village == null)
            {
                return NotFound();
            }

            return View(village);
        }

        // GET: Villages/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name");
            return View();
        }

        // POST: Villages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PlayerId")] Village village)
        {
            if (ModelState.IsValid)
            {
                _context.Add(village);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", village.PlayerId);
            return View(village);
        }

        // GET: Villages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var village = await _context.Villages.FindAsync(id);
            if (village == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", village.PlayerId);
            return View(village);
        }

        // POST: Villages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PlayerId")] Village village)
        {
            if (id != village.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(village);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VillageExists(village.Id))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "Name", village.PlayerId);
            return View(village);
        }

        // GET: Villages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var village = await _context.Villages
                .Include(v => v.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (village == null)
            {
                return NotFound();
            }

            return View(village);
        }

        // POST: Villages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var village = await _context.Villages.FindAsync(id);
            if (village != null)
            {
                _context.Villages.Remove(village);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VillageExists(int id)
        {
            return _context.Villages.Any(e => e.Id == id);
        }
    }
}
