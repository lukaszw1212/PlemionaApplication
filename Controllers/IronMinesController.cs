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
    public class IronMinesController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public IronMinesController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // GET: IronMines
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.IronMine.Include(i => i.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: IronMines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ironMine = await _context.IronMine
                .Include(i => i.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ironMine == null)
            {
                return NotFound();
            }

            return View(ironMine);
        }

        // GET: IronMines/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: IronMines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,GenerateIronPerTime,MaxIronPerTime,MaxBuildingLevel,Time,Id,Name,VillageId")] IronMine ironMine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ironMine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", ironMine.VillageId);
            return View(ironMine);
        }

        // GET: IronMines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ironMine = await _context.IronMine.FindAsync(id);
            if (ironMine == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", ironMine.VillageId);
            return View(ironMine);
        }

        // POST: IronMines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,GenerateIronPerTime,MaxIronPerTime,MaxBuildingLevel,Time,Id,Name,VillageId")] IronMine ironMine)
        {
            if (id != ironMine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ironMine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IronMineExists(ironMine.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", ironMine.VillageId);
            return View(ironMine);
        }

        // GET: IronMines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ironMine = await _context.IronMine
                .Include(i => i.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ironMine == null)
            {
                return NotFound();
            }

            return View(ironMine);
        }

        // POST: IronMines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ironMine = await _context.IronMine.FindAsync(id);
            if (ironMine != null)
            {
                _context.IronMine.Remove(ironMine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IronMineExists(int id)
        {
            return _context.IronMine.Any(e => e.Id == id);
        }
    }
}
