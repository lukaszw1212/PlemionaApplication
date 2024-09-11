using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProjekt;
using MiniProjekt.Enumerable;
using PlemionaApplication.Data;
using PlemionaApplication.Entities;

namespace PlemionaApplication.Controllers
{
    public class ExpeditionsController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public ExpeditionsController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        //metoda zwracająca listę jednostek dla ekspedycji
        [HttpGet("Expeditions/GetExpeditionUnits/{expeditionId}")]
        public JsonResult GetExpeditionUnits(int expeditionId)
        {
            var expedition = _context.Expedition.FirstOrDefault(e => e.Id == expeditionId);
            if (expedition == null)
                return Json(new { error = "Ekspedycja nie została znaleziona." });
            var archerAmount = _context.Archer.Count(a => a.ExpeditionId == expeditionId);
            var warriorAmount = _context.Warrior.Count(w => w.ExpeditionId == expeditionId);
            var catapultAmount = _context.Catapult.Count(c => c.ExpeditionId == expeditionId);
            var hussarAmount = _context.Hussar.Count(h => h.ExpeditionId == expeditionId);
            var kamikadzeAmount = _context.Kamikadze.Count(k => k.ExpeditionId == expeditionId);
            var trojanAmount = _context.Trojan.Count(t => t.ExpeditionId == expeditionId);
            var result = new
            {
                ArcherAmount = archerAmount,
                WarriorAmount = warriorAmount,
                CatapultAmount = catapultAmount,
                HussarAmount = hussarAmount,
                KamikadzeAmount = kamikadzeAmount,
                TrojanAmount = trojanAmount
            };
            return Json(result);
        }

        [HttpPost]
        public JsonResult Fight([FromBody] JsonElement data)
        {
            var userName = User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);
            var player = _context.Players.FirstOrDefault(p => p.UserId == user.Id);
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            // Pobranie wartości z Dictionary
            int archerAmount = int.TryParse(data.GetProperty("archerAmount").GetString() ?? "0", out var arch) ? arch : 0;
            int warriorAmount = int.TryParse(data.GetProperty("warriorAmount").GetString() ?? "0", out var war) ? war : 0;
            int hussarAmount = int.TryParse(data.GetProperty("hussarAmount").GetString() ?? "0", out var hus) ? hus : 0;
            int kamikadzeAmount = int.TryParse(data.GetProperty("kamikadzeAmount").GetString() ?? "0", out var kam) ? kam : 0;
            int trojanAmount = int.TryParse(data.GetProperty("trojanAmount").GetString() ?? "0", out var tro) ? tro : 0;
            int expeditionId = Int32.Parse(data.GetProperty("expeditionId").GetString());
            var expedition = _context.Expedition.FirstOrDefault(e => e.Id == expeditionId);
            var archers = _context.Archer.Where(a => a.VillageId == village.Id).OrderBy(a => a.Level).Take(archerAmount).ToList();
            var warriors = _context.Warrior.Where(w => w.VillageId == village.Id).OrderBy(a => a.Level).Take(warriorAmount).ToList();
            var hussars = _context.Hussar.Where(h => h.VillageId == village.Id).OrderBy(a => a.Level).Take(hussarAmount).ToList();
            var kamikadzes = _context.Kamikadze.Where(k => k.VillageId == village.Id).OrderBy(a => a.Level).Take(kamikadzeAmount).ToList();
            var trojans = _context.Trojan.Where(t => t.VillageId == village.Id).OrderBy(a => a.Level).Take(trojanAmount).ToList();

            var playerUnitEntities = new List<Entity>();
            playerUnitEntities.AddRange(archers);
            playerUnitEntities.AddRange(warriors);
            playerUnitEntities.AddRange(hussars);
            playerUnitEntities.AddRange(kamikadzes);
            playerUnitEntities.AddRange(trojans);

            var enemyArchers = _context.Archer.Where(e => e.ExpeditionId == expeditionId).ToList();
            var enemyWarriors = _context.Warrior.Where(e => e.ExpeditionId == expeditionId).ToList();
            var enemyHussars = _context.Hussar.Where(e => e.ExpeditionId == expeditionId).ToList();
            var enemyKamikadzes = _context.Kamikadze.Where(e => e.ExpeditionId == expeditionId).ToList();
            var enemyTrojans = _context.Trojan.Where(e => e.ExpeditionId == expeditionId).ToList();

            // Dodaj jednostki przeciwnika do listy
            var enemyUnitEntities = new List<Entity>();
            enemyUnitEntities.AddRange(enemyArchers);
            enemyUnitEntities.AddRange(enemyWarriors);
            enemyUnitEntities.AddRange(enemyHussars);
            enemyUnitEntities.AddRange(enemyKamikadzes);
            enemyUnitEntities.AddRange(enemyTrojans);
            return Battle(enemyUnitEntities, playerUnitEntities, expeditionId);
        }
        private JsonResult Battle(List<Entity> enemyUnits, List<Entity> playerUnitEntities, int expeditionId)
        {
            var userName = User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);
            var player = _context.Players.FirstOrDefault(p => p.UserId == user.Id);

            var allUnits = playerUnitEntities.Concat(enemyUnits).OrderBy(unit => unit.AttackSpeed).ToList();

            while (playerUnitEntities.Any(unit => unit.CurrentHP > 0) && enemyUnits.Any(unit => unit.CurrentHP > 0))
            {
                foreach (var unit in allUnits)
                {
                    // Sprawdź czy jednostka nadal żyje
                    if (unit.CurrentHP <= 0) continue;

                    if (playerUnitEntities.Contains(unit)) // Jednostka gracza atakuje
                    {
                        if (enemyUnits.Count == 0) break; // Przerwij jeśli nie ma więcej przeciwników
                        PerformAttack(unit, enemyUnits);  // Atakuj przeciwników
                        enemyUnits.RemoveAll(unit => unit.CurrentHP <= 0); // Usuń pokonane jednostki
                    }
                    else // Jednostka przeciwnika atakuje
                    {
                        if (playerUnitEntities.Count == 0) break; // Przerwij jeśli nie ma więcej jednostek gracza
                        PerformAttack(unit, playerUnitEntities); // Atakuj jednostki gracza
                        playerUnitEntities.RemoveAll(unit => unit.CurrentHP <= 0); // Usuń pokonane jednostki
                    }
                }
            }
            // Usuń jednostki gracza z bazy danych
            var defeatedPlayerUnits = playerUnitEntities.Where(unit => unit.CurrentHP <= 0).ToList();
            foreach (var unit in defeatedPlayerUnits)
            {
                _context.Remove(unit); // Usuń z bazy danych
            }
            _context.SaveChanges(); // Zapisz zmiany
            // Aktualizacja zdrowia po walce
            int playerHealth = playerUnitEntities.Sum(unit => unit.CurrentHP);
            int enemyHealth = enemyUnits.Sum(unit => unit.CurrentHP);
            var enemyArchers = _context.Archer.Where(e => e.ExpeditionId == expeditionId).ToList();
            var enemyWarriors = _context.Warrior.Where(e => e.ExpeditionId == expeditionId).ToList();
            var enemyHussars = _context.Hussar.Where(e => e.ExpeditionId == expeditionId).ToList();
            var enemyKamikadzes = _context.Kamikadze.Where(e => e.ExpeditionId == expeditionId).ToList();
            var enemyTrojans = _context.Trojan.Where(e => e.ExpeditionId == expeditionId).ToList();

            // Dodaj jednostki przeciwnika do listy
            var enemyUnitEntities = new List<Entity>();
            enemyUnitEntities.AddRange(enemyArchers);
            enemyUnitEntities.AddRange(enemyWarriors);
            enemyUnitEntities.AddRange(enemyHussars);
            enemyUnitEntities.AddRange(enemyKamikadzes);
            enemyUnitEntities.AddRange(enemyTrojans);
            enemyUnitEntities.ForEach(e => e.CurrentHP = e.MaxHP);
            // Save changes to remove player units from the database
            _context.SaveChanges();

            int experienceGained = 0;
            int goldGained = 0;
            int ironGained = 0;
            int stoneGained = 0;
            int woodGained = 0;
            int wheatGained = 0;
            var expedition = _context.Expedition.FirstOrDefault(e => e.Id == expeditionId);
            if (playerHealth > 0)
            {
                experienceGained = expedition.ExperienceGained;
                goldGained = _context.Resources
                    .Where(r => r.ExpeditionId == expeditionId)
                    .FirstOrDefault(r => r.Type == ResourceType.Gold)?.Amount ?? 0;

                ironGained = _context.Resources
                    .Where(r => r.ExpeditionId == expeditionId)
                    .FirstOrDefault(r => r.Type == ResourceType.Iron)?.Amount ?? 0;

                stoneGained = _context.Resources
                    .Where(r => r.ExpeditionId == expeditionId)
                    .FirstOrDefault(r => r.Type == ResourceType.Stone)?.Amount ?? 0;

                woodGained = _context.Resources
                    .Where(r => r.ExpeditionId == expeditionId)
                    .FirstOrDefault(r => r.Type == ResourceType.Wood)?.Amount ?? 0;

                wheatGained = _context.Resources
                    .Where(r => r.ExpeditionId == expeditionId)
                    .FirstOrDefault(r => r.Type == ResourceType.Wheat)?.Amount ?? 0;
                AddExp(player, experienceGained);
                var goldResource = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == ResourceType.Gold);
                var woodResource = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == ResourceType.Wood);
                var ironResource = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == ResourceType.Iron);
                var stoneResource = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == ResourceType.Stone);
                var wheatResource = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == ResourceType.Wheat);
                goldResource.Amount += goldGained;
                woodResource.Amount += woodGained;
                ironResource.Amount += ironGained;
                stoneResource.Amount += stoneGained;
                wheatResource.Amount += wheatGained;
                _context.SaveChanges();
            }
            var result = new BattleResult
            {
                PlayerWin = playerHealth > 0,
                PlayerRemainingHealth = playerHealth,
                EnemyRemainingHealth = enemyHealth,
                GoldGained = goldGained,
                ExperienceGained = experienceGained,
                IronGained = ironGained,
                StoneGained = stoneGained,
                WoodGained = woodGained,
                WheatGained = wheatGained
            };
            return Json(result);
        }
        private int CalculateDamage(Entity attacker, Entity defender)
        {
            double baseDamage = attacker.Damage;

            // Adjust damage based on type
            switch (attacker.DamageType)
            {
                case Damage.Physical:
                    baseDamage = baseDamage / defender.PhysicalResistance;
                    break;
                case Damage.Range:
                    baseDamage = baseDamage / defender.RangeResistance;
                    break;
                case Damage.Destruction:
                    // Destruction damage ignores resistance
                    break;
            }

            return (int)(Math.Max(baseDamage, 1)); // Ensure damage is not negative
        }
        private void PerformAttack(Entity attacker, List<Entity> defenders)
        {
            var target = defenders.OrderBy(d => d.CurrentHP).FirstOrDefault(); // Atakuj cel z najmniejszym HP
            if (target != null)
            {
                int effectiveDamage = CalculateDamage(attacker, target);
                target.CurrentHP -= effectiveDamage;
            }
        }

        // GET: Expeditions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Expedition.ToListAsync());
        }

        // GET: Expeditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedition = await _context.Expedition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expedition == null)
            {
                return NotFound();
            }

            return View(expedition);
        }

        // GET: Expeditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expeditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Level,ExperienceGained,Cooldown")] Expedition expedition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expedition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expedition);
        }

        // GET: Expeditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedition = await _context.Expedition.FindAsync(id);
            if (expedition == null)
            {
                return NotFound();
            }
            return View(expedition);
        }

        // POST: Expeditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Level,ExperienceGained,Cooldown")] Expedition expedition)
        {
            if (id != expedition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expedition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpeditionExists(expedition.Id))
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
            return View(expedition);
        }

        // GET: Expeditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedition = await _context.Expedition
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expedition == null)
            {
                return NotFound();
            }

            return View(expedition);
        }

        // POST: Expeditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expedition = await _context.Expedition.FindAsync(id);
            if (expedition != null)
            {
                _context.Expedition.Remove(expedition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpeditionExists(int id)
        {
            return _context.Expedition.Any(e => e.Id == id);
        }
        public void AddExp(Player player, int exp)
        {
            player.CurrentExperience += exp;
            _context.Players.Update(player);
            CheckLevel(player);
        }

        private void CheckLevel(Player player)
        {
            int requiredExp = CalculateRequiredExperience(player.Level);
            while (player.CurrentExperience >= requiredExp)
            {
                player.CurrentExperience -= requiredExp;
                player.Level++;
                requiredExp = CalculateRequiredExperience(player.Level);
            }
            _context.Players.Update(player);
        }

        public int CalculateRequiredExperience(int level)
        {
            return level * 20;
        }
    }
}
