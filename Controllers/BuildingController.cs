using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProjekt; // Poprawne nazwy przestrzeni nazw Twojej aplikacji
using MiniProjekt.Enumerable;
using PlemionaApplication.Data;
using PlemionaApplication.Entities;
using System.Linq;
namespace PlemionaApplication.Controllers
{
    public class BuildingController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public BuildingController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult BuildBuilding(string buildingName)
        {
            string userName = User.Identity.Name;

            // Pobierz użytkownika
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                return RedirectToAction("Index"); // lub inna obsługa błędu
            }

            // Pobierz gracza na podstawie ID użytkownika
            var player = _context.Players.FirstOrDefault(u => u.UserId == user.Id);

            if (player == null)
            {
                return RedirectToAction("Index"); // lub inna obsługa błędu
            }
            var village = _context.Villages.FirstOrDefault(v => v.PlayerId == player.Id);
            // Sprawdź i aktualizuj zasoby w zależności od budynku

            string insufficientResourcesMessage = string.Empty;

            switch (buildingName)
            {
                case "Tartak":
                    if (!_context.Sawmill.Any(b => b.VillageId == village.Id))
                    {
                        if (HasEnoughResources(player, ResourceType.Gold, 100))
                        {
                            DeductResources(player, ResourceType.Gold, 100);
                            AddTartakToPlayer(player);
                        }
                    }
                    break;
                case "Koszary":
                    if (!_context.Barracks.Any(b => b.VillageId == village.Id))
                    {
                        if (HasEnoughResources(player, ResourceType.Wood, 80) && HasEnoughResources(player, ResourceType.Gold, 150) && HasEnoughResources(player, ResourceType.Wheat, 100))
                        {
                            DeductResources(player, ResourceType.Wood, 80);
                            DeductResources(player, ResourceType.Gold, 150);
                            DeductResources(player, ResourceType.Wheat, 100);
                            AddKoszaryToPlayer(player);
                        }
                    }
                    break;
                case "Zbrojownia":
                    if (!_context.Armory.Any(b => b.VillageId == village.Id))
                    {
                        if (HasEnoughResources(player, ResourceType.Iron, 100) && HasEnoughResources(player, ResourceType.Stone, 200) && HasEnoughResources(player, ResourceType.Gold, 250))
                        {
                            DeductResources(player, ResourceType.Iron, 100);
                            DeductResources(player, ResourceType.Stone, 200);
                            DeductResources(player, ResourceType.Gold, 250);
                            AddZbrojowniaToPlayer(player);
                        }
                    }
                    break;
                case "Farma":
                    if (!_context.GrainFarm.Any(b => b.VillageId == village.Id))
                    {
                        if (HasEnoughResources(player, ResourceType.Wood, 80) && HasEnoughResources(player, ResourceType.Gold, 200))
                        {
                            DeductResources(player, ResourceType.Wood, 80);
                            DeductResources(player, ResourceType.Gold, 200);
                            AddFarmaToPlayer(player);
                        }
                    }
                    break;
                case "Kopalnia Kamienia":
                    if (!_context.StoneMine.Any(b => b.VillageId == village.Id))
                    {
                        if (HasEnoughResources(player, ResourceType.Gold, 150))
                        {
                            DeductResources(player, ResourceType.Gold, 150);
                            AddKopalniaKamieniaToPlayer(player);
                        }
                    }
                    break;
                case "Kopalnia Żelaza":
                    if (!_context.IronMine.Any(b => b.VillageId == village.Id))
                    {
                        if (HasEnoughResources(player, ResourceType.Gold, 150) && HasEnoughResources(player, ResourceType.Stone, 100))
                        {
                            DeductResources(player, ResourceType.Stone, 100);
                            DeductResources(player, ResourceType.Gold, 150);
                            AddKopalniaZelazaToPlayer(player);
                        }
                    }
                    break;
                case "Mury Obronne":
                    if (!_context.DefensiveWalls.Any(b => b.VillageId == village.Id))
                    {
                        if (HasEnoughResources(player, ResourceType.Iron, 100) && HasEnoughResources(player, ResourceType.Wood, 100) && HasEnoughResources(player, ResourceType.Stone, 100))
                        {
                            DeductResources(player, ResourceType.Iron, 100);
                            DeductResources(player, ResourceType.Wood, 100);
                            DeductResources(player, ResourceType.Stone, 100);
                            AddMuryObronneToPlayer(player);
                        }
                    }
                    break;
                case "Silos":
                    if (!_context.Silo.Any(b => b.VillageId == village.Id))
                    {
                        if (HasEnoughResources(player, ResourceType.Gold, 250) && HasEnoughResources(player, ResourceType.Wood, 300))
                        {
                            DeductResources(player, ResourceType.Gold, 250);
                            DeductResources(player, ResourceType.Wood, 300);
                            AddSiloToPlayer(player);
                        }
                    }
                    break;
                case "Stajnia":
                    if (!_context.HorseStable.Any(b => b.VillageId == village.Id))
                    {
                        if (HasEnoughResources(player, ResourceType.Wheat, 300) && HasEnoughResources(player, ResourceType.Gold, 200) && HasEnoughResources(player, ResourceType.Wood, 200))
                        {
                            DeductResources(player, ResourceType.Wheat, 300);
                            DeductResources(player, ResourceType.Gold, 200);
                            DeductResources(player, ResourceType.Wood, 200);
                            AddStajniaToPlayer(player);
                        }
                    }
                    break;
                default:
                    // Obsługa nieznanego budynku (opcjonalnie)
                    break;
            }

            // Zapisz zmiany w bazie danych
            _context.SaveChanges();

            return Json(new { success = true });
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

        private void AddTartakToPlayer(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            Sawmill tartak = new Sawmill()
            {
                Name = "Tartak",
                Level = 1,
                VillageId = village.Id,
                GenerateWoodPerTime = 10,
                MaxWoodPerTime = 500,
                Time = 30,
                MaxBuildingLevel = 5
            };
            _context.Add(tartak);
        }
         private void AddKoszaryToPlayer(Player player)
         {
                var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
                Barracks koszary = new Barracks()
                {
                    Name = "Koszary",
                    Level = 1,
                    VillageId = village.Id,
                    Cost = 1,
                    MaxBuildingLevel = 5
                };
            _context.Add(koszary);
         }
        private void AddZbrojowniaToPlayer(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            Armory zbrojownia = new Armory()
            {
                Name = "Zbrojownia",
                Level = 1,
                VillageId = village.Id,
                Cost = 1,
                MaxBuildingLevel = 5
            };
            _context.Add(zbrojownia);
        }
        private void AddFarmaToPlayer(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            GrainFarm farma = new GrainFarm()
            {
                Name = "Farma",
                Level = 1,
                GenerateWheatPerTime = 10,
                Time = 40,
                MaxFarmPerTime = 500,
                MaxBuildingLevel = 5,
                VillageId = village.Id
            };
            _context.Add(farma);
        }
        private void AddKopalniaKamieniaToPlayer(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            StoneMine kopalnia = new StoneMine()
            {
                Name = "Kopalnia kamienia",
                Level = 1,
                GenerateStonePerTime = 10,
                MaxStonePerTime = 500,
                Time = 35,
                MaxBuildingLevel = 5,
                VillageId = village.Id
            };
            _context.Add(kopalnia);
        }
        private void AddKopalniaZelazaToPlayer(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            IronMine kopalnia = new IronMine()
            {
                Name = "Kopalnia zelaza",
                Level = 1,
                GenerateIronPerTime = 10,
                MaxIronPerTime = 300,
                Time = 55,
                MaxBuildingLevel = 5,
                VillageId = village.Id
            };
            _context.Add(kopalnia);
        }
        private void AddMuryObronneToPlayer(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            DefensiveWalls mury = new DefensiveWalls()
            {
                Name = "Mury obronne",
                Level = 1,
                DefensiveValue = 10,
                MaxDefensiveValue = 10,
                MaxBuildingLevel = 5,
                VillageId = village.Id
            };
            _context.Add(mury);
        }
        private void AddSiloToPlayer(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            Silo silo = new Silo()
            {
                Name = "Silo",
                Level = 1,
                VillageId = village.Id
            };
            _context.Add(silo);
        }
        private void AddStajniaToPlayer(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            HorseStable stajnia = new HorseStable()
            {
                Name = "Stajnia",
                Level = 1,
                CurrentHorses = 0,
                MaxHorses = 2,
                VillageId = village.Id
            };
            _context.Add(stajnia);
        }
    }
}
