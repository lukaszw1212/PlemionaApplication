using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProjekt;
using PlemionaApplication.Data;
using PlemionaApplication.Entities;
using MiniProjekt.Enumerable;
namespace PlemionaApplication.Controllers
{
    public class ArmoriesController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public ArmoriesController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        // Metoda zwracająca listę jednostek dla danej wioski
        [HttpGet]
        public JsonResult GetUnits(string unitType)
        {
            var userName = User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);
            var player = _context.Players.FirstOrDefault(p => p.UserId == user.Id);
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);

            if (village == null)
                return Json(new { error = "Wioska nie została znaleziona." });

            List<UnitDto> units = unitType switch
            {
                "Archer" => _context.Archer
                    .Where(a => a.VillageId == village.Id)
                    .Select(a => new UnitDto { Id = a.Id, Name = a.Name, Level = a.Level })
                    .ToList(),
                "Kamikadze" => _context.Kamikadze
                    .Where(k => k.VillageId == village.Id)
                    .Select(k => new UnitDto { Id = k.Id, Name = k.Name, Level = k.Level })
                    .ToList(),
                "Warrior" => _context.Warrior
                    .Where(w => w.VillageId == village.Id)
                    .Select(w => new UnitDto { Id = w.Id, Name = w.Name, Level = w.Level })
                    .ToList(),
                "Trojan" => _context.Trojan
                    .Where(t => t.VillageId == village.Id)
                    .Select(t => new UnitDto { Id = t.Id, Name = t.Name, Level = t.Level })
                    .ToList(),
                "Hussar" => _context.Hussar
                    .Where(h => h.VillageId == village.Id)
                    .Select(h => new UnitDto { Id = h.Id, Name = h.Name, Level = h.Level })
                    .ToList(),
                "Catapult" => _context.Catapult
                    .Where(c => c.VillageId == village.Id)
                    .Select(c => new UnitDto { Id = c.Id, Name = c.Name, Level = c.Level })
                    .ToList(),
                _ => new List<UnitDto>()
            };

            if (!units.Any())
                return Json(new { error = "Brak jednostek w wiosce dla podanego typu." });

            return Json(units);
        }
        // Metoda zwracająca koszty ulepszenia jednostki na podstawie typu
        [HttpGet("Armories/GetUpgradeCost/{unitType}/{unitId}")]
        public JsonResult GetUpgradeCost(string unitType, int unitId)
        {
            dynamic unit = unitType switch
            {
                "Archer" => _context.Archer.FirstOrDefault(a => a.Id == unitId),
                "Kamikadze" => _context.Kamikadze.FirstOrDefault(k => k.Id == unitId),
                "Warrior" => _context.Warrior.FirstOrDefault(w => w.Id == unitId),
                "Trojan" => _context.Trojan.FirstOrDefault(t => t.Id == unitId),
                "Hussar" => _context.Hussar.FirstOrDefault(h => h.Id == unitId),
                "Catapult" => _context.Catapult.FirstOrDefault(c => c.Id == unitId),
                _ => null
            };

            if (unit == null)
                return Json(new { error = "Jednostka nie została znaleziona." });

            var upgradeCosts = CalculateUpgradeCost(unit);

            if (upgradeCosts == null)
                return Json(new { error = "Niepoprawne dane wejściowe." });

            return Json(upgradeCosts);
        }
        [HttpGet]
        // Funkcja pomocnicza do obliczania kosztów ulepszenia
        private object CalculateUpgradeCost(object unit)
        {

            if (unit == null) return null;

            var type = unit.GetType();
            var levelProperty = type.GetProperty("Level");
            var level = (int)(levelProperty?.GetValue(unit) ?? 0);

            return type.Name switch
            {
                "Archer" => new
                {
                    gold = level * 10,
                    wood = level * 5,
                    stone = level * 3,
                    iron = level * 2,
                    wheat = level * 1
                },
                "Kamikadze" => new
                {
                    gold = level * 15,
                    wood = level * 3,
                    stone = level * 5,
                    iron = level * 4,
                    wheat = level * 2
                },
                "Warrior" => new
                {
                    gold = level * 8,
                    wood = level * 4,
                    stone = level * 2,
                    iron = level * 1,
                    wheat = level * 2
                },
                "Trojan" => new
                {
                    gold = level * 18,
                    wood = level * 4,
                    stone = level * 6,
                    iron = level * 3,
                    wheat = level * 3
                },
                "Hussar" => new
                {
                    gold = level * 20,
                    wood = level * 6,
                    stone = level * 4,
                    iron = level * 5,
                    wheat = level * 4
                },
                "Catapult" => new
                {
                    gold = level * 25,
                    wood = level * 8,
                    stone = level * 10,
                    iron = level * 6,
                    wheat = level * 5
                },
                _ => new
                {
                    gold = 0,
                    wood = 0,
                    stone = 0,
                    iron = 0,
                    wheat = 0
                }
            };
        }
        // Metoda do ulepszania jednostek
        [HttpGet("Armories/UpgradeUnit/{unitType}/{unitId}")]
        public JsonResult UpgradeUnit(string unitType, int unitId)
        {
                var userName = User.Identity.Name;
                var user = _context.User.FirstOrDefault(u => u.UserName == userName);
                var player = _context.Players.FirstOrDefault(p => p.UserId == user.Id);
                var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);

                if (village == null)
                    return Json(new { error = "Wioska nie została znaleziona." });

                dynamic unit;
                switch (unitType)
                {
                    case "Archer":
                        unit = _context.Archer.FirstOrDefault(a => a.Id == unitId);
                        break;
                    case "Kamikadze":
                        unit = _context.Kamikadze.FirstOrDefault(k => k.Id == unitId);
                        break;
                    case "Warrior":
                        unit = _context.Warrior.FirstOrDefault(w => w.Id == unitId);
                        break;
                    case "Trojan":
                        unit = _context.Trojan.FirstOrDefault(t => t.Id == unitId);
                        break;
                    case "Hussar":
                        unit = _context.Hussar.FirstOrDefault(h => h.Id == unitId);
                        break;
                    case "Catapult":
                        unit = _context.Catapult.FirstOrDefault(c => c.Id == unitId);
                        break;
                    default:
                        return Json(new { error = "Niepoprawny typ jednostki." });
                }

                if (unit == null)
                    return Json(new { error = "Jednostka nie została znaleziona." });

                var cost = CalculateUpgradeCost(unit);

                // Sprawdzenie, czy użytkownik ma wystarczające zasoby
                if (!HasEnoughResources(player, ResourceType.Gold, cost.gold) ||
                     !HasEnoughResources(player, ResourceType.Wood, cost.wood) ||
                     !HasEnoughResources(player, ResourceType.Stone, cost.stone) ||
                     !HasEnoughResources(player, ResourceType.Iron, cost.iron) ||
                     !HasEnoughResources(player, ResourceType.Wheat, cost.wheat))
                {
                    return Json(new { error = "Niewystarczające zasoby." });
            }

                DeductResources(player, ResourceType.Gold, cost.gold);
                DeductResources(player, ResourceType.Wood, cost.wood);
                DeductResources(player, ResourceType.Stone, cost.stone);
                DeductResources(player, ResourceType.Iron, cost.iron);
                DeductResources(player, ResourceType.Wheat, cost.wheat);

                unit.UpgradeLevel();
                _context.Update(unit);

                _context.SaveChanges();

                return Json(new { success = "Jednostka została ulepszona.", newLevel = unit.Level });
        }
        // Metoda pomocnicza do pobierania jednostki według typu
        private dynamic GetUnitByType(string unitType, int unitId)
        {
            return unitType switch
            {
                "Archer" => _context.Archer.FirstOrDefault(a => a.Id == unitId),
                "Kamikadze" => _context.Kamikadze.FirstOrDefault(k => k.Id == unitId),
                "Warrior" => _context.Warrior.FirstOrDefault(w => w.Id == unitId),
                "Trojan" => _context.Trojan.FirstOrDefault(t => t.Id == unitId),
                "Hussar" => _context.Hussar.FirstOrDefault(h => h.Id == unitId),
                "Catapult" => _context.Catapult.FirstOrDefault(h => h.Id == unitId),
                _ => null
            };
        }
        private bool HasEnoughResources(Player player, ResourceType type, int amount)
        {
            var resource = _context.Resources.Where(r => r.PlayerId == player.Id).FirstOrDefault(r => r.Type == type);
            return resource != null && resource.Amount >= amount;
        }

        private void DeductResources(Player player, ResourceType type, int amount)
        {
            var resource = _context.Resources.Where(r => r.PlayerId == player.Id).FirstOrDefault(r => r.Type == type);
            if (resource != null)
            {
                resource.Amount -= amount;
                _context.Resources.Update(resource);
            }
        }
        public class UnitUpgradeRequest
        {
            public int Id { get; set; }
            public string Type { get; set; }
        }
        // GET: Armories
        public async Task<IActionResult> Index()
        {
            var plemionaApplicationContext = _context.Armory.Include(a => a.Village);
            return View(await plemionaApplicationContext.ToListAsync());
        }

        // GET: Armories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armory = await _context.Armory
                .Include(a => a.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armory == null)
            {
                return NotFound();
            }

            return View(armory);
        }

        // GET: Armories/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name");
            return View();
        }

        // POST: Armories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Level,Cost,MaxBuildingLevel,Id,Name,VillageId")] Armory armory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", armory.VillageId);
            return View(armory);
        }

        // GET: Armories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armory = await _context.Armory.FindAsync(id);
            if (armory == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", armory.VillageId);
            return View(armory);
        }

        // POST: Armories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Level,Cost,MaxBuildingLevel,Id,Name,VillageId")] Armory armory)
        {
            if (id != armory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmoryExists(armory.Id))
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
            ViewData["VillageId"] = new SelectList(_context.Villages, "Id", "Name", armory.VillageId);
            return View(armory);
        }

        // GET: Armories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armory = await _context.Armory
                .Include(a => a.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (armory == null)
            {
                return NotFound();
            }

            return View(armory);
        }

        // POST: Armories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var armory = await _context.Armory.FindAsync(id);
            if (armory != null)
            {
                _context.Armory.Remove(armory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmoryExists(int id)
        {
            return _context.Armory.Any(e => e.Id == id);
        }
    }
}
