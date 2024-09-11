using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PlemionaApplication.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PlemionaApplication.Data;
using PlemionaApplication.Entities;
using MiniProjekt;
using System.Security.Permissions;
using MiniProjekt.Enumerable;
namespace PlemionaApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly PlemionaApplicationContext _context;

        public HomeController(PlemionaApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string VillageName = "";
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            else
            {
                string userName = User.Identity.Name;
                var user = _context.User.FirstOrDefault(u => u.UserName == userName);
                var player = _context.Players.FirstOrDefault(u => u.UserId == user.Id);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
                ViewBag.VillageName = village.Name;
                var gold = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Gold);
                var wood = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Wood);
                var stone = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Stone);
                var iron = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Iron);
                var wheat = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Wheat);
                ViewBag.GoldAmount = gold.Amount;
                ViewBag.WoodAmount = wood.Amount;
                ViewBag.StoneAmount = stone.Amount;
                ViewBag.IronAmount = iron.Amount;
                ViewBag.WheatAmount = wheat.Amount;
                //predkosci generowania i ustawienie informacji o levelu
                var townHall = _context.TownHall.FirstOrDefault(t => t.VillageId == village.Id);
                ViewBag.TownHallSpeed = townHall.Time;
                ViewBag.RatuszLevel = townHall.Level;
                var sawmill = _context.Sawmill.FirstOrDefault(t => t.VillageId == village.Id);
                if (sawmill != null)
                {
                    ViewBag.SawmillSpeed = sawmill.Time;
                    ViewBag.TartakLevel = sawmill.Level;
                }
                else
                {
                    ViewBag.SawmillSpeed = 30;
                    ViewBag.TartakLevel = 0;
                }
                var ironmine = _context.IronMine.FirstOrDefault(t => t.VillageId == village.Id);
                if (ironmine != null)
                {
                    ViewBag.IronMineSpeed = ironmine.Time;
                    ViewBag.ZelazoLevel = ironmine.Level;
                }
                else
                {
                    ViewBag.IronMineSpeed = 55;
                    ViewBag.ZelazoLevel = 0;
                }
                var stonemine = _context.StoneMine.FirstOrDefault(t => t.VillageId == village.Id);
                if (stonemine != null)
                {
                    ViewBag.StoneMineSpeed = stonemine.Time;
                    ViewBag.KamienLevel = stonemine.Level;
                }
                else
                {
                    ViewBag.StoneMineSpeed = 35;
                    ViewBag.KamienLevel = 0;
                }
                var grainfarm = _context.GrainFarm.FirstOrDefault(t => t.VillageId == village.Id);
                if (grainfarm != null)
                {
                    ViewBag.FarmaLevel = grainfarm.Level;
                    ViewBag.GrainFarmSpeed = grainfarm.Time;
                }
                else
                {
                    ViewBag.FarmaLevel = 0;
                    ViewBag.GrainFarmSpeed = 40;
                }

                //dalsze budynki i przypisywanie levelu
                var barracks = _context.Barracks.FirstOrDefault(t => t.VillageId == village.Id);
                if (barracks != null)
                    ViewBag.KoszaryLevel = barracks.Level;
                else
                    ViewBag.KoszaryLevel = 0;

                var armory = _context.Armory.FirstOrDefault(t => t.VillageId == village.Id);
                if (barracks != null)
                    ViewBag.ZbrojowniaLevel = barracks.Level;
                else ViewBag.ZbrojowniaLevel = 0;

                var walls = _context.DefensiveWalls.FirstOrDefault(t => t.Village.Id == village.Id);
                if (walls != null)
                    ViewBag.MuryLevel = walls.Level;
                else ViewBag.MuryLevel = 0;

                var silo = _context.Silo.FirstOrDefault(t => t.Village.Id == village.Id);
                if (silo != null)
                    ViewBag.SilosLevel = silo.Level;
                else
                    ViewBag.SilosLevel = 0;

                var horsestable = _context.HorseStable.FirstOrDefault(t => t.Village.Id == village.Id);
                if (horsestable != null)
                    ViewBag.StajniaLevel = horsestable.Level;
                else
                    ViewBag.StajniaLevel = 0;

                //sprawdzenie czy budynek jest wybudowany
                ViewBag.IsSawmillBuilt = _context.Sawmill.Any(b => b.VillageId == village.Id) ? 1 : 0;
                ViewBag.IsBarracksBuilt = _context.Barracks.Any(b => b.VillageId == village.Id) ? 1 : 0;
                ViewBag.IsArmoryBuilt = _context.Armory.Any(b => b.VillageId == village.Id) ? 1 : 0;
                ViewBag.IsGrainFarmBuilt = _context.GrainFarm.Any(b => b.VillageId == village.Id) ? 1 : 0;
                ViewBag.IsStoneMineBuilt = _context.StoneMine.Any(b => b.VillageId == village.Id) ? 1 : 0;
                ViewBag.IsIronMineBuilt = _context.IronMine.Any(b => b.VillageId == village.Id) ? 1 : 0;
                ViewBag.IsDefensiveWallsBuilt = _context.DefensiveWalls.Any(b => b.VillageId == village.Id) ? 1 : 0;
                ViewBag.IsSiloBuilt = _context.Silo.Any(b => b.VillageId == village.Id) ? 1 : 0;
                ViewBag.IsHorseStableBuilt = _context.HorseStable.Any(b => b.VillageId == village.Id) ? 1 : 0;
                ViewBag.IsTownhallBuilt = _context.TownHall.Any(b => b.VillageId == village.Id) ? 1 : 0;
                // sprawdzenie poziomu gracza
                ViewBag.PlayerLevel = player.Level;
                ViewBag.PlayerExp = player.CurrentExperience;
                // sprawdzenie iloœci jednostek
                ViewBag.ArcherAmount = _context.Archer.Count(a => a.VillageId == village.Id);
                ViewBag.CatapultAmount = _context.Catapult.Count(a => a.VillageId == village.Id);
                ViewBag.HussarAmount = _context.Hussar.Count(a => a.VillageId == village.Id);
                ViewBag.KamikadzeAmount = _context.Kamikadze.Count(a => a.VillageId == village.Id);
                ViewBag.TrojanAmount = _context.Trojan.Count(a => a.VillageId == village.Id);
                ViewBag.WarriorAmount = _context.Warrior.Count(a => a.VillageId == village.Id);
            }
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CollectGold()
        {
            string userName = User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);
            var player = _context.Players.FirstOrDefault(u => u.UserId == user.Id);
            var playerId = player.Id;
            var village = _context.Villages.FirstOrDefault(v => v.Id == playerId);
            var goldResource = _context.Resources.FirstOrDefault(r => r.PlayerId == playerId && r.Type == MiniProjekt.Enumerable.ResourceType.Gold);
            if (goldResource != null)
            {
                var townHall = _context.TownHall.FirstOrDefault(t => t.VillageId == village.Id);
                if (townHall != null)
                {
                    if (goldResource.Amount < townHall.MaxGoldPerTime)
                    {
                        goldResource.Amount += townHall.GenerateGoldPerTime;
                        _context.SaveChanges();
                    }
                }
            }

            return PartialView("_GoldAmountPartial", goldResource?.Amount ?? 0);
        }
        [HttpPost]
        public async Task<IActionResult> CollectWood()
        {
            string userName = User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);
            var player = _context.Players.FirstOrDefault(u => u.UserId == user.Id);
            var playerId = player.Id;
            var village = _context.Villages.FirstOrDefault(v => v.Id == playerId);
            var woodResource = _context.Resources.FirstOrDefault(r => r.PlayerId == playerId && r.Type == MiniProjekt.Enumerable.ResourceType.Wood);
            if (woodResource != null)
            {
                var sawmill = _context.Sawmill.FirstOrDefault(t => t.VillageId == village.Id);
                var silo = _context.Silo.FirstOrDefault(t => t.VillageId == village.Id);
                if (sawmill != null)
                {
                    if (silo != null)
                    {
                        if (woodResource.Amount < sawmill.MaxWoodPerTime + (silo.Level * 400))
                        {
                            woodResource.Amount += sawmill.GenerateWoodPerTime;
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (woodResource.Amount < sawmill.MaxWoodPerTime)
                        {
                            woodResource.Amount += sawmill.GenerateWoodPerTime;
                            _context.SaveChanges();
                        }
                    }
                }
            }
            return PartialView("_WoodAmountPartial", woodResource?.Amount ?? 0);
        }
        [HttpPost]
        public async Task<IActionResult> CollectIron()
        {
            string userName = User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);
            var player = _context.Players.FirstOrDefault(u => u.UserId == user.Id);
            var playerId = player.Id;
            var village = _context.Villages.FirstOrDefault(v => v.Id == playerId);
            var ironResource = _context.Resources.FirstOrDefault(r => r.PlayerId == playerId && r.Type == MiniProjekt.Enumerable.ResourceType.Iron);
            if (ironResource != null)
            {
                var ironmine = _context.IronMine.FirstOrDefault(t => t.VillageId == village.Id);
                var silo = _context.Silo.FirstOrDefault(t => t.VillageId == village.Id);
                if (ironmine != null)
                {
                    if (silo != null)
                    {
                        if (ironResource.Amount < ironmine.MaxIronPerTime + (silo.Level * 400))
                        {
                            ironResource.Amount += ironmine.GenerateIronPerTime;
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (ironResource.Amount < ironmine.MaxIronPerTime)
                        {
                            ironResource.Amount += ironmine.GenerateIronPerTime;
                            _context.SaveChanges();
                        }
                    }
                }
            }
            return PartialView("_IronAmountPartial", ironResource?.Amount ?? 0);
        }
        [HttpPost]
        public async Task<IActionResult> CollectStone()
        {
            string userName = User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);
            var player = _context.Players.FirstOrDefault(u => u.UserId == user.Id);
            var playerId = player.Id;
            var village = _context.Villages.FirstOrDefault(v => v.Id == playerId);
            var stoneResource = _context.Resources.FirstOrDefault(r => r.PlayerId == playerId && r.Type == MiniProjekt.Enumerable.ResourceType.Stone);
            if (stoneResource != null)
            {
                var stonemine = _context.StoneMine.FirstOrDefault(t => t.VillageId == village.Id);
                var silo = _context.Silo.FirstOrDefault(t => t.VillageId == village.Id);
                if (stonemine != null)
                {
                    if (silo != null)
                    {
                        if (stoneResource.Amount < stonemine.MaxStonePerTime + (silo.Level * 400))
                        {
                            stoneResource.Amount += stonemine.GenerateStonePerTime;
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (stoneResource.Amount < stonemine.MaxStonePerTime)
                        {
                            stoneResource.Amount += stonemine.GenerateStonePerTime;
                            _context.SaveChanges();
                        }
                    }
                }
            }
            return PartialView("_StoneAmountPartial", stoneResource?.Amount ?? 0);
        }
        [HttpPost]
        public async Task<IActionResult> CollectWheat()
        {
            string userName = User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);
            var player = _context.Players.FirstOrDefault(u => u.UserId == user.Id);
            var playerId = player.Id;
            var village = _context.Villages.FirstOrDefault(v => v.Id == playerId);
            var wheatResource = _context.Resources.FirstOrDefault(r => r.PlayerId == playerId && r.Type == MiniProjekt.Enumerable.ResourceType.Wheat);
            if (wheatResource != null)
            {
                var grainfarm = _context.GrainFarm.FirstOrDefault(t => t.VillageId == village.Id);
                var silo = _context.Silo.FirstOrDefault(t => t.VillageId == village.Id);
                if (grainfarm != null)
                {
                    if (silo != null)
                    {
                        if (wheatResource.Amount < grainfarm.MaxFarmPerTime + (silo.Level * 400))
                        {
                            wheatResource.Amount += grainfarm.GenerateWheatPerTime;
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (wheatResource.Amount < grainfarm.MaxFarmPerTime)
                        {
                            wheatResource.Amount += grainfarm.GenerateWheatPerTime;
                            _context.SaveChanges();
                        }
                    }
                }
            }
            return PartialView("_WheatAmountPartial", wheatResource?.Amount ?? 0);
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.User
                    .FirstOrDefaultAsync(u => u.UserName == model.Username && u.PasswordHash == model.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.Id.ToString());

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "User")
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Append("UserId", user.Id.ToString(), option);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete("UserId");

            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Username,
                    Email = model.Email,
                    PasswordHash = model.Password,  // In a real application, you should hash the password
                    RoleId = 1  // Assign a default role (e.g., 2 for regular users)
                };

                _context.Add(user);
                await _context.SaveChangesAsync();
                var player = new Player
                {
                    Name = model.Username,
                    Level = 1,
                    CurrentExperience = 0,
                    UserId = _context.User.Where(u => u.UserName == model.Username).FirstOrDefault().Id
                };
                _context.Add(player);
                await _context.SaveChangesAsync();
                int villageId = 1;
                if (model.Username.EndsWith('a') || model.Username.EndsWith('A'))
                {
                    var village = new Village
                    {
                        Name = "Wioska " + model.Username.Substring(0, model.Username.Length - 1) + "y",
                        PlayerId = _context.Players.Where(u => u.Name == model.Username).FirstOrDefault().Id
                    };
                    _context.Add(village);
                    await _context.SaveChangesAsync();
                    villageId = village.Id;
                }
                else
                {
                    var village = new Village
                    {
                        Name = "Wioska " + model.Username + "a",
                        PlayerId = _context.Players.Where(u => u.Name == model.Username).FirstOrDefault().Id
                    };
                    _context.Add(village);
                    await _context.SaveChangesAsync();
                    villageId = village.Id;
                }
                int PlayerId = player.Id;
                var gold = new Resource("Gold", 100);
                gold.PlayerId = PlayerId;
                var wood = new Resource("Wood", 0);
                wood.PlayerId = PlayerId;
                var iron = new Resource("Iron", 0);
                iron.PlayerId = PlayerId;
                var stone = new Resource("Stone", 0);
                stone.PlayerId = PlayerId;
                var wheat = new Resource("Wheat", 0);
                wheat.PlayerId = PlayerId;
                _context.Add(gold);
                _context.Add(wood);
                _context.Add(stone);    
                _context.Add(wheat);    
                _context.Add(iron);
                await _context.SaveChangesAsync();
                var TownHall = new TownHall
                {
                    Name = "Ratusz",
                    Level = 1,
                    VillageId = villageId,
                    MaxBuildingLevel = 8,
                    GenerateGoldPerTime = 10,
                    MaxGoldPerTime = 1000,
                    Time = 20
                 };
                _context.Add(TownHall);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> MyCharacter()
        {
            string userIdString = Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Home");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "Home");
            }
            var player = await _context.Players.FirstOrDefaultAsync(p => p.UserId == userId);
            var fraction = await _context.Fractions.FirstOrDefaultAsync(p => p.Id == player.FractionId);
            if (player == null)
            {
                return NotFound();
            }
            if (fraction != null)
            {
                ViewBag.FractionName = fraction.Name;
            }
            else
            {
                ViewBag.FractionName = " ";
            }
            
            return View(player);
        }



        public IActionResult Expeditions()
        {
            string userName = User.Identity.Name;
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                throw new Exception("Nie znaleziono u¿ytkownika o podanej nazwie.");
            }

            // Pobierz gracza na podstawie Id u¿ytkownika
            var player = _context.Players.FirstOrDefault(p => p.UserId == user.Id);

            if (player == null)
            {
                throw new Exception("Nie znaleziono gracza powi¹zanego z u¿ytkownikiem.");
            }
            var playerLevel = player.Level;
            var village = _context.Villages.FirstOrDefault(v => v.Id == player.Id);
            ViewBag.VillageName = village.Name;
            var gold = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Gold);
            var wood = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Wood);
            var stone = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Stone);
            var iron = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Iron);
            var wheat = _context.Resources.FirstOrDefault(r => r.PlayerId == player.Id && r.Type == MiniProjekt.Enumerable.ResourceType.Wheat);
            ViewBag.GoldAmount = gold.Amount;
            ViewBag.WoodAmount = wood.Amount;
            ViewBag.StoneAmount = stone.Amount;
            ViewBag.IronAmount = iron.Amount;
            ViewBag.WheatAmount = wheat.Amount;
            //predkosci generowania i ustawienie informacji o levelu
            var townHall = _context.TownHall.FirstOrDefault(t => t.VillageId == village.Id);
            ViewBag.TownHallSpeed = townHall.Time;
            ViewBag.RatuszLevel = townHall.Level;
            var sawmill = _context.Sawmill.FirstOrDefault(t => t.VillageId == village.Id);
            if (sawmill != null)
            {
                ViewBag.SawmillSpeed = sawmill.Time;
                ViewBag.TartakLevel = sawmill.Level;
            }
            else
            {
                ViewBag.SawmillSpeed = 30;
                ViewBag.TartakLevel = 0;
            }
            var ironmine = _context.IronMine.FirstOrDefault(t => t.VillageId == village.Id);
            if (ironmine != null)
            {
                ViewBag.IronMineSpeed = ironmine.Time;
                ViewBag.ZelazoLevel = ironmine.Level;
            }
            else
            {
                ViewBag.IronMineSpeed = 55;
                ViewBag.ZelazoLevel = 0;
            }
            var stonemine = _context.StoneMine.FirstOrDefault(t => t.VillageId == village.Id);
            if (stonemine != null)
            {
                ViewBag.StoneMineSpeed = stonemine.Time;
                ViewBag.KamienLevel = stonemine.Level;
            }
            else
            {
                ViewBag.StoneMineSpeed = 35;
                ViewBag.KamienLevel = 0;
            }
            var grainfarm = _context.GrainFarm.FirstOrDefault(t => t.VillageId == village.Id);
            if (grainfarm != null)
            {
                ViewBag.FarmaLevel = grainfarm.Level;
                ViewBag.GrainFarmSpeed = grainfarm.Time;
            }
            else
            {
                ViewBag.FarmaLevel = 0;
                ViewBag.GrainFarmSpeed = 40;
            }
            var horsestable = _context.HorseStable.FirstOrDefault(t => t.Village.Id == village.Id);
            if (horsestable != null)
                ViewBag.StajniaLevel = horsestable.Level;
            else
                ViewBag.StajniaLevel = 0;
            ViewBag.ArcherAmount = _context.Archer.Count(a => a.VillageId == village.Id);
            ViewBag.CatapultAmount = _context.Catapult.Count(a => a.VillageId == village.Id);
            ViewBag.HussarAmount = _context.Hussar.Count(a => a.VillageId == village.Id);
            ViewBag.KamikadzeAmount = _context.Kamikadze.Count(a => a.VillageId == village.Id);
            ViewBag.TrojanAmount = _context.Trojan.Count(a => a.VillageId == village.Id);
            ViewBag.WarriorAmount = _context.Warrior.Count(a => a.VillageId == village.Id);
            var expeditions = _context.Expedition.ToList();
            ViewBag.PlayerLevel = playerLevel;
            ViewBag.PlayerExp = player.CurrentExperience;
            return View(expeditions);
        }

        public async Task<IActionResult> MyTribe()
        {
            string userIdString = Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Home");
            }

            if (!int.TryParse(userIdString, out int userId))
            {
                return RedirectToAction("Login", "Home");
            }

            var player = await _context.Players
                                       .FirstOrDefaultAsync(p => p.UserId == userId);
            if (player == null)
            {
                return NotFound();
            }

            var fraction = await _context.Fractions
                                         .Include(f => f.GuildMaster)
                                         .Include(f => f.Players)
                                         .FirstOrDefaultAsync(f => f.Id == player.FractionId);
            if (fraction == null)
            {
                return NotFound();
            }

            return View(fraction);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Home/JoinFraction
        public async Task<IActionResult> JoinFraction()
        {
            var fractions = await _context.Fractions.ToListAsync();
            ViewBag.Fractions = fractions;
            return View();
        }

        // POST: Home/JoinFraction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinFraction(int fractionId)
        {
            // Get the current user's ID from session or cookie
            var userId = Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Handle if user is not logged in
            }

            // Find the user by ID
            var user = await _context.User.FindAsync(int.Parse(userId));
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Handle if user not found
            }

            // Find the player associated with the user
            var player = await _context.Players
                .Include(p => p.Fraction) // Include fraction data
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            if (player == null)
            {
                return RedirectToAction("Error"); // Handle if player not found
            }

            // Find the selected fraction
            var fraction = await _context.Fractions.FindAsync(fractionId);
            if (fraction == null)
            {
                return NotFound(); // Handle if fraction not found
            }

            // Update player's fraction
            player.Fraction = fraction; // Assign selected fraction to player
            player.FractionId = fraction.Id; // Assign fraction ID to player
            _context.Players.Update(player);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home"); // Redirect to home or appropriate page
        }

        // ... inne akcje kontrolera
    }

    // ... inne akcje kontrolera
}
