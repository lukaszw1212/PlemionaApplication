using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProjekt;
using MiniProjekt.Enumerable;
using PlemionaApplication.Data;
using PlemionaApplication.Entities;
using System.Configuration;
using MiniProjekt.Enumerable;

namespace PlemionaApplication.Controllers
{
    public class RecruitmentController : Controller
    {
        private readonly PlemionaApplicationContext _context;
        public RecruitmentController(PlemionaApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(int Archers, int Hussars, int Kamikadze, int Catapults, int Trojans, int Warriors, string GoldRequired, string WheatRequired, string StoneRequired, string IronRequired, string WoodRequired)
        {
            if (ModelState.IsValid)
            {
                int goldRequired = Int32.Parse(GoldRequired);
                int woodRequired = Int32.Parse(WoodRequired);
                int stoneRequired = Int32.Parse(StoneRequired);
                int ironRequired = Int32.Parse(IronRequired);
                int wheatRequired = Int32.Parse(WheatRequired);
                string userName = User.Identity.Name;
                var user = _context.User.FirstOrDefault(u => u.UserName == userName);
                var player = _context.Players.FirstOrDefault(u => u.UserId == user.Id);
                var playerId = player.Id;
                var village = _context.Villages.FirstOrDefault(v => v.Id == playerId);
                if (HasEnoughResources(player, ResourceType.Wheat, wheatRequired) && HasEnoughResources(player, ResourceType.Gold, goldRequired)
                    && HasEnoughResources(player, ResourceType.Stone, stoneRequired) && HasEnoughResources(player, ResourceType.Iron, ironRequired)
                    && HasEnoughResources(player, ResourceType.Wood, woodRequired))
                {
                    DeductResources(player, ResourceType.Wheat, wheatRequired);
                    DeductResources(player, ResourceType.Gold, goldRequired);
                    DeductResources(player, ResourceType.Stone, stoneRequired);
                    DeductResources(player, ResourceType.Iron, ironRequired);
                    DeductResources(player, ResourceType.Wood, woodRequired);
                    // Dodaj jednostki do bazy danych
                    for (int i = 0; i < Archers; i++)
                    {
                        var archer = new Archer()
                        {
                            VillageId = village.Id,
                            Name = "Archer",
                            CurrentHP = 10,
                            MaxHP = 10,
                            AttackSpeed = 1.3,
                            DamageType = Damage.Range,
                            Damage = 5,
                            PhysicalResistance = 2,
                            RangeResistance = 2,
                            Level = 1,
                        };
                        _context.Add(archer);
                    }
                    for (int i = 0; i < Hussars; i++)
                    {
                        var hussar = new Hussar()
                        {
                            VillageId = village.Id,
                            Name = "Hussar",
                            CurrentHP = 20,
                            MaxHP = 20,
                            AttackSpeed = 2,
                            DamageType = Damage.Physical,
                            Damage = 20,
                            PhysicalResistance = 6,
                            RangeResistance = 1,
                            Level = 1,
                        };
                        _context.Add(hussar);
                    }
                    for (int i = 0; i < Kamikadze; i++)
                    {
                        var kamikadze = new Kamikadze()
                        {
                            VillageId = village.Id,
                            Name = "Kamikadze",
                            CurrentHP = 10,
                            MaxHP = 10,
                            AttackSpeed = 0.5,
                            DamageType = Damage.Destruction,
                            Damage = 50,
                            PhysicalResistance = 2,
                            RangeResistance = 8,
                            Level = 1,
                        };
                        _context.Add(kamikadze);
                    }
                    for (int i = 0; i < Catapults; i++)
                    {
                        var catapult = new Catapult()
                        {
                            Name = "Catapult",
                            Level = 1,
                            VillageId = village.Id,
                            CurrentHP = 20,
                            MaxHP = 20,
                            AttackSpeed = 0.2,
                            DamageType = Damage.Range,
                            Damage = 30,
                            PhysicalResistance = 1,
                            RangeResistance = 8,

                        };
                        _context.Add(catapult);
                    }
                    for (int i = 0; i < Trojans; i++)
                    {
                        var trojan = new Trojan()
                        {
                            Name = "Trojan",
                            Level = 1,
                            VillageId = village.Id,
                            CurrentHP = 80,
                            MaxHP = 80,
                            AttackSpeed = 1,
                            DamageType = Damage.Physical,
                            Damage = 0,
                            PhysicalResistance = 15,
                            RangeResistance = 15,
                        };
                        _context.Add(trojan);
                    }
                    for (int i = 0; i < Warriors; i++)
                    {
                        var warrior = new Warrior()
                        {
                            VillageId = village.Id,
                            Name = "Warrior",
                            CurrentHP = 25,
                            MaxHP = 25,
                            AttackSpeed = 1,
                            DamageType = Damage.Physical,
                            Damage = 8,
                            PhysicalResistance = 5,
                            RangeResistance = 3,
                            Level = 1,
                        };
                        _context.Add(warrior);
                    }

                    _context.SaveChanges();

                    // Opcjonalnie: Przekierowanie lub powiadomienie o sukcesie
                    return RedirectToAction("Index", "Home");
                }
            }

            // W przypadku błędów walidacji, zwróć widok z powrotem
            return RedirectToAction("Index", "Home");
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
    }
}
