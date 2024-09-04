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
                            AddExp(player, 10);
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
                            AddExp(player, 30);
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
                            AddExp(player, 30);
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
                            AddExp(player, 20);
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
                            AddExp(player, 15);
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
                            AddExp(player, 20);
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
                            AddExp(player, 25);
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
                            AddExp(player, 20);
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
                            AddExp(player, 30);
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

        [HttpPost]
        public IActionResult UpgradeBuilding(string buildingName)
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
                    if (_context.Sawmill.Any(b => b.VillageId == village.Id))
                    {
                        var sawmill = _context.Sawmill.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Gold, 100 * sawmill.Level))
                        {
                            DeductResources(player, ResourceType.Gold, 100 * sawmill.Level);
                            AddExp(player, 20);
                            UpgradeTartak(player);
                        }
                    }
                    break;
                case "Koszary":
                    if (_context.Barracks.Any(b => b.VillageId == village.Id))
                    {
                        var barracks = _context.Barracks.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Wood, 80 * barracks.Level) && HasEnoughResources(player, ResourceType.Gold, 150 * barracks.Level) && HasEnoughResources(player, ResourceType.Wheat, 100 * barracks.Level))
                        {
                            DeductResources(player, ResourceType.Wood, 80 * barracks.Level);
                            DeductResources(player, ResourceType.Gold, 150 * barracks.Level);
                            DeductResources(player, ResourceType.Wheat, 100 * barracks.Level);
                            AddExp(player, 20);
                            UpgradeKoszary(player);
                        }
                    }
                    break;
                case "Zbrojownia":
                    if (_context.Armory.Any(b => b.VillageId == village.Id))
                    {
                        var armory = _context.Armory.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Iron, 100 * armory.Level) && HasEnoughResources(player, ResourceType.Stone, 200 * armory.Level) && HasEnoughResources(player, ResourceType.Gold, 250 * armory.Level))
                        {
                            DeductResources(player, ResourceType.Iron, 100 * armory.Level);
                            DeductResources(player, ResourceType.Stone, 200 * armory.Level);
                            DeductResources(player, ResourceType.Gold, 250 * armory.Level);
                            AddExp(player, 20);
                            UpgradeZbrojownia(player);
                        }
                    }
                    break;
                case "Farma":
                    if (_context.GrainFarm.Any(b => b.VillageId == village.Id))
                    {
                        var farm = _context.GrainFarm.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Wood, 80 * farm.Level) && HasEnoughResources(player, ResourceType.Gold, 200 * farm.Level))
                        {
                            DeductResources(player, ResourceType.Wood, 80 * farm.Level);
                            DeductResources(player, ResourceType.Gold, 200 * farm.Level);
                            AddExp(player, 20);
                            UpgradeFarma(player);
                        }
                    }
                    break;
                case "Kopalnia Kamienia":
                    if (_context.StoneMine.Any(b => b.VillageId == village.Id))
                    {
                        var kopalnia = _context.StoneMine.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Gold, 150 * kopalnia.Level))
                        {
                            DeductResources(player, ResourceType.Gold, 150 * kopalnia.Level);
                            AddExp(player, 20);
                            UpgradeKopalniaKamienia(player);
                        }
                    }
                    break;
                case "Kopalnia Żelaza":
                    if (_context.IronMine.Any(b => b.VillageId == village.Id))
                    {
                        var kopalnia = _context.IronMine.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Gold, 150 * kopalnia.Level) && HasEnoughResources(player, ResourceType.Stone, 100 * kopalnia.Level))
                        {
                            DeductResources(player, ResourceType.Stone, 100 * kopalnia.Level);
                            DeductResources(player, ResourceType.Gold, 150 * kopalnia.Level);
                            AddExp(player, 20);
                            UpgradeKopalniaZelaza(player);
                        }
                    }
                    break;
                case "Mury Obronne":
                    if (_context.DefensiveWalls.Any(b => b.VillageId == village.Id))
                    {
                        var mury = _context.DefensiveWalls.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Iron, 100 * mury.Level) && HasEnoughResources(player, ResourceType.Wood, 100 * mury.Level) && HasEnoughResources(player, ResourceType.Stone, 100 * mury.Level))
                        {
                            DeductResources(player, ResourceType.Iron, 100 * mury.Level);
                            DeductResources(player, ResourceType.Wood, 100 * mury.Level);
                            DeductResources(player, ResourceType.Stone, 100 * mury.Level);
                            AddExp(player, 20);
                            UpgradeMury(player);
                        }
                    }
                    break;
                case "Silos":
                    if (_context.Silo.Any(b => b.VillageId == village.Id))
                    {
                        var silos = _context.Silo.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Gold, 250 * silos.Level) && HasEnoughResources(player, ResourceType.Wood, 300 * silos.Level))
                        {
                            DeductResources(player, ResourceType.Gold, 250 * silos.Level);
                            DeductResources(player, ResourceType.Wood, 300 * silos.Level);
                            AddExp(player, 20);
                            UpgradeSilos(player);
                        }
                    }
                    break;
                case "Stajnia":
                    if (_context.HorseStable.Any(b => b.VillageId == village.Id))
                    {
                        var stajnia = _context.HorseStable.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Wheat, 300 * stajnia.Level) && HasEnoughResources(player, ResourceType.Gold, 200 * stajnia.Level) && HasEnoughResources(player, ResourceType.Wood, 200 * stajnia.Level))
                        {
                            DeductResources(player, ResourceType.Wheat, 300 * stajnia.Level);
                            DeductResources(player, ResourceType.Gold, 200 * stajnia.Level);
                            DeductResources(player, ResourceType.Wood, 200 * stajnia.Level);
                            AddExp(player, 20);
                            UpgradeStajnia(player);
                        }
                    }
                    break;
                case "Ratusz":
                    if (_context.TownHall.Any(b => b.VillageId == village.Id))
                    {
                        var ratusz = _context.TownHall.FirstOrDefault(b => b.VillageId == village.Id);
                        if (HasEnoughResources(player, ResourceType.Gold, 200 * ratusz.Level))
                        {
                            DeductResources(player, ResourceType.Gold, 200 * ratusz.Level);
                            AddExp(player, 20);
                            UpgradeRatusz(player);
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

        private void UpgradeTartak(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var tartak = _context.Sawmill.FirstOrDefault(b => b.VillageId == village.Id);
            tartak.Level++;
            _context.Update(tartak);
        }
        private void UpgradeKoszary(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var koszary = _context.Barracks.FirstOrDefault(b => b.VillageId == village.Id);
            koszary.Level++;
            _context.Update(koszary);
        }
        private void UpgradeZbrojownia(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var zbrojownia = _context.Armory.FirstOrDefault(b => b.VillageId == village.Id);
            zbrojownia.Level++;
            _context.Update(zbrojownia);
        }
        private void UpgradeFarma(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var farma = _context.GrainFarm.FirstOrDefault(b => b.VillageId == village.Id);
            farma.Level++;
            _context.Update(farma);
        }
        private void UpgradeKopalniaKamienia(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var kopalnia = _context.StoneMine.FirstOrDefault(b => b.VillageId == village.Id);
            kopalnia.Level++;
            _context.Update(kopalnia);
        }
        private void UpgradeKopalniaZelaza(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var kopalnia = _context.IronMine.FirstOrDefault(b => b.VillageId == village.Id);
            kopalnia.Level++;
            _context.Update(kopalnia);
        }
        private void UpgradeMury(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var mury = _context.DefensiveWalls.FirstOrDefault(b => b.VillageId == village.Id);
            mury.Level++;
            _context.Update(mury);
        }
        private void UpgradeSilos(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var silos = _context.Silo.FirstOrDefault(b => b.VillageId == village.Id);
            silos.Level++;
            _context.Update(silos);
        }
        private void UpgradeStajnia(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var stajnia = _context.HorseStable.FirstOrDefault(b => b.VillageId == village.Id);
            stajnia.Level++;
            _context.Update(stajnia);
        }
        private void UpgradeRatusz(Player player)
        {
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            var ratusz = _context.TownHall.FirstOrDefault(b => b.VillageId == village.Id);
            ratusz.Level++;
            _context.Update(ratusz);
        }
    }
}
