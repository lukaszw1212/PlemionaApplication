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
    public class StoneMinesController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public StoneMinesController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: StoneMines
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.StoneMine.Include(s => s.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: StoneMines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoneMine = await _context.StoneMine
                .Include(s => s.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stoneMine == null)
            {
                return NotFound();
            }

            return View(stoneMine);
        }

        // GET: StoneMines/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: StoneMines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,MaxBuildingLevel,GenerateStonePerTime,MaxStonePerTime,Time,Id,Name,VillageId")] StoneMine stoneMine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stoneMine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", stoneMine.VillageId);
            return View(stoneMine);
        }

        // GET: StoneMines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoneMine = await _context.StoneMine.FindAsync(id);
            if (stoneMine == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", stoneMine.VillageId);
            return View(stoneMine);
        }

        // POST: StoneMines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,MaxBuildingLevel,GenerateStonePerTime,MaxStonePerTime,Time,Id,Name,VillageId")] StoneMine stoneMine)
        {
            if (id != stoneMine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stoneMine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoneMineExists(stoneMine.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", stoneMine.VillageId);
            return View(stoneMine);
        }

        // GET: StoneMines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stoneMine = await _context.StoneMine
                .Include(s => s.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stoneMine == null)
            {
                return NotFound();
            }

            return View(stoneMine);
        }

        // POST: StoneMines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stoneMine = await _context.StoneMine.FindAsync(id);
            if (stoneMine != null)
            {
                _context.StoneMine.Remove(stoneMine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoneMineExists(int id)
        {
            return _context.StoneMine.Any(e => e.Id == id);
        }
    }
}
