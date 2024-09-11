using Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using PlemionaApplication.Data;
using PlemionaApplication.Entities;
using PlemionaApplication.Models;
using MiniProjekt;
using MiniProjekt.Enumerable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
namespace PlemionaApplication.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PlemionaApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PlemionaApplicationContext>>()))
            {
                if (context.Role.Any())
                {
                }
                else
                {
                    context.Role.AddRange(
                        new Role
                        {
                            Name = "User"
                        },
                        new Role
                        {
                            Name = "Admin"
                        },
                        new Role
                        {
                            Name = "Tester"
                        }

                    );
                    context.SaveChanges();
                }
                if (!context.Expedition.Any())
                {
                    // gnomy
                    var wioskaGnomow = new Expedition()
                    {
                        Name = "Wioska Gnomów",
                        Level = 1,
                        ExperienceGained = 50,
                        Cooldown = 60,
                        Image = "gnomevillage.jpg"
                    };

                    context.Expedition.Add(wioskaGnomow);
                    context.SaveChanges();
                    var entities = new List<Entity>
                    {
                    new Warrior()
                    {
                        Name = "Gnom",
                        CurrentHP = 20,
                        MaxHP = 20,
                        AttackSpeed = 3,
                        DamageType = Damage.Physical,
                        Damage = 10,
                        PhysicalResistance = 1,
                        RangeResistance = 1,
                        Level = 1,
                        ExpeditionId = wioskaGnomow.Id
                    },
                    new Warrior()
                    {
                        Name = "Gnom",
                        CurrentHP = 20,
                        MaxHP = 20,
                        AttackSpeed = 3,
                        DamageType = Damage.Physical,
                        Damage = 10,
                        PhysicalResistance = 1,
                        RangeResistance = 1,
                        Level = 1,
                        ExpeditionId = wioskaGnomow.Id
                    },
                    new Warrior()
                    {
                        Name = "Gnom",
                        CurrentHP = 20,
                        MaxHP = 20,
                        AttackSpeed = 3,
                        DamageType = Damage.Physical,
                        Damage = 10,
                        PhysicalResistance = 1,
                        RangeResistance = 1,
                        Level = 1,
                        ExpeditionId = wioskaGnomow.Id
                    },
                    new Warrior()
                    {
                        Name = "Gnom",
                        CurrentHP = 20,
                        MaxHP = 20,
                        AttackSpeed = 3,
                        DamageType = Damage.Physical,
                        Damage = 10,
                        PhysicalResistance = 1,
                        RangeResistance = 1,
                        Level = 1,
                        ExpeditionId = wioskaGnomow.Id
                    },
                    new Warrior()
                    {
                        Name = "Gnom",
                        CurrentHP = 20,
                        MaxHP = 20,
                        AttackSpeed = 3,
                        DamageType = Damage.Physical,
                        Damage = 10,
                        PhysicalResistance = 1,
                        RangeResistance = 1,
                        Level = 1,
                        ExpeditionId = wioskaGnomow.Id
                    },
                    new Archer()
                     {
                        Name = "Gnom Łucznik",
                        CurrentHP = 15,
                        MaxHP = 15,
                        AttackSpeed = 1.5,
                        DamageType = Damage.Range,
                        Damage = 15,
                        PhysicalResistance = 1,
                        RangeResistance = 1,
                        Level = 1,
                        ExpeditionId = wioskaGnomow.Id
                    },
                    new Archer()
                     {
                        Name = "Gnom Łucznik",
                        CurrentHP = 15,
                        MaxHP = 15,
                        AttackSpeed = 1.5,
                        DamageType = Damage.Range,
                        Damage = 15,
                        PhysicalResistance = 1,
                        RangeResistance = 1,
                        Level = 1,
                        ExpeditionId = wioskaGnomow.Id
                    }
                };
                    context.AddRange(entities);
                    var resources = new List<MiniProjekt.Resource>
                    {
                        new MiniProjekt.Resource("Gold", 200)
                        { ExpeditionId = wioskaGnomow.Id },
                        new MiniProjekt.Resource("Stone", 100)
                        { ExpeditionId = wioskaGnomow.Id }
                    };

                    context.AddRange(resources);

                    // lvl 4
                    context.SaveChanges();
                    var tajemniczyGaj = new Expedition()
                    {
                        Name = "Tajemniczy Gaj",
                        Level = 4,
                        ExperienceGained = 150,
                        Cooldown = 90,
                        Image = "gaj.jpg"
                    };
                    context.Expedition.Add(tajemniczyGaj);
                    context.SaveChanges();
                    entities = new List<Entity>
                    {
                        new Archer()
                        {
                            Name = "Elf",
                            CurrentHP = 25,
                            MaxHP = 25,
                            AttackSpeed = 1.8,
                            DamageType = Damage.Range,
                            Damage = 25,
                            PhysicalResistance = 1.4,
                            RangeResistance = 1.8,
                            Level = 4,
                            ExpeditionId = tajemniczyGaj.Id
                        },
                        new Archer()
                        {
                            Name = "Elf",
                            CurrentHP = 25,
                            MaxHP = 25,
                            AttackSpeed = 1.8,
                            DamageType = Damage.Range,
                            Damage = 25,
                            PhysicalResistance = 1.4,
                            RangeResistance = 1.8,
                            Level = 4,
                            ExpeditionId = tajemniczyGaj.Id
                        },
                        new Archer()
                        {
                            Name = "Elf",
                            CurrentHP = 25,
                            MaxHP = 25,
                            AttackSpeed = 1.8,
                            DamageType = Damage.Range,
                            Damage = 25,
                            PhysicalResistance = 1.4,
                            RangeResistance = 1.8,
                            Level = 4,
                            ExpeditionId = tajemniczyGaj.Id
                        },
                        new Archer()
                        {
                            Name = "Elf",
                            CurrentHP = 25,
                            MaxHP = 25,
                            AttackSpeed = 1.8,
                            DamageType = Damage.Range,
                            Damage = 25,
                            PhysicalResistance = 1.4,
                            RangeResistance = 1.8,
                            Level = 4,
                            ExpeditionId = tajemniczyGaj.Id
                        },
                        new Archer()
                        {
                            Name = "Elf",
                            CurrentHP = 25,
                            MaxHP = 25,
                            AttackSpeed = 1.8,
                            DamageType = Damage.Range,
                            Damage = 25,
                            PhysicalResistance = 1.4,
                            RangeResistance = 1.8,
                            Level = 4,
                            ExpeditionId = tajemniczyGaj.Id
                        },
                        new Archer()
                        {
                            Name = "Elf",
                            CurrentHP = 25,
                            MaxHP = 25,
                            AttackSpeed = 1.8,
                            DamageType = Damage.Range,
                            Damage = 25,
                            PhysicalResistance = 1.4,
                            RangeResistance = 1.8,
                            Level = 4,
                            ExpeditionId = tajemniczyGaj.Id
                        },
                        new Archer()
                        {
                            Name = "Elf",
                            CurrentHP = 25,
                            MaxHP = 25,
                            AttackSpeed = 1.8,
                            DamageType = Damage.Range,
                            Damage = 25,
                            PhysicalResistance = 1.4,
                            RangeResistance = 1.8,
                            Level = 4,
                            ExpeditionId = tajemniczyGaj.Id
                        },
                        new Catapult()
                        {
                            Name = "Elfia katapulta",
                            Level = 5,
                            ExpeditionId = tajemniczyGaj.Id,
                            CurrentHP = 30,
                            MaxHP = 30,
                            AttackSpeed = 0.4,
                            DamageType = Damage.Range,
                            Damage = 60,
                            PhysicalResistance = 1,
                            RangeResistance = 1.5,
                        },
                        new Catapult()
                        {
                            Name = "Elfia katapulta",
                            Level = 5,
                            ExpeditionId = tajemniczyGaj.Id,
                            CurrentHP = 30,
                            MaxHP = 30,
                            AttackSpeed = 0.4,
                            DamageType = Damage.Range,
                            Damage = 60,
                            PhysicalResistance = 1,
                            RangeResistance = 1.5,
                        },
                        new Hussar()
                        {
                            ExpeditionId = tajemniczyGaj.Id,
                            Name = "Elf szarżysta",
                            Level = 1,
                            CurrentHP = 40,
                            MaxHP = 40,
                            AttackSpeed = 3,
                            DamageType = Damage.Physical,
                            Damage = 40,
                            PhysicalResistance = 2.25,
                            RangeResistance = 0.5,
                        },
                        new Warrior()
                        {
                            Name = "Elf mniejszy",
                            CurrentHP = 25,
                            MaxHP = 25,
                            AttackSpeed = 5,
                            DamageType = Damage.Physical,
                            Damage = 20,
                            PhysicalResistance = 1,
                            RangeResistance = 1,
                            Level = 3,
                            ExpeditionId = tajemniczyGaj.Id
                        }
                    };

                    context.AddRange(entities);
                    resources = new List<MiniProjekt.Resource>
                        {
                        new MiniProjekt.Resource("Gold", 300)
                        { ExpeditionId = tajemniczyGaj.Id },
                        new MiniProjekt.Resource("Wood", 300)
                        { ExpeditionId = tajemniczyGaj.Id }
                        };

                    context.AddRange(resources);
                    context.SaveChanges();
                    // lvl 6
                    var kamiennyGolem = new Expedition()
                    {
                        Name = "Kamienny Golem",
                        Level = 6,
                        ExperienceGained = 200,
                        Cooldown = 400,
                        Image = "golem.jpg"
                    };
                    context.Add(kamiennyGolem);
                    context.SaveChanges();
                    var golem = new Warrior()
                    {
                        Name = "Kamienny Golem [Boss]",
                        CurrentHP = 400,
                        MaxHP = 400,
                        AttackSpeed = 2,
                        DamageType = Damage.Physical,
                        Damage = 80,
                        PhysicalResistance = 10,
                        RangeResistance = 5,
                        Level = 10,
                        ExpeditionId = kamiennyGolem.Id
                    };
                    context.Add(golem);
                    resources = new List<MiniProjekt.Resource>
                        {
                        new MiniProjekt.Resource("Iron", 600)
                        { ExpeditionId = kamiennyGolem.Id },
                        new MiniProjekt.Resource("Stone", 600)
                        { ExpeditionId = kamiennyGolem.Id }
                        };
                    context.AddRange(resources);
                    context.SaveChanges();
                    // lvl 10
                    var upiornaPolana = new Expedition()
                    {
                        Name = "Upiorna Polana",
                        Level = 10,
                        ExperienceGained = 300,
                        Cooldown = 120,
                        Image = "polana.jpg"
                    };
                    context.Add(upiornaPolana);
                    context.SaveChanges();
                    entities = new List<Entity>
                    {
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Warrior()
                        {
                            Name = "Nieumarły farmer",
                            CurrentHP = 50,
                            MaxHP = 50,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 35,
                            PhysicalResistance = 1.6,
                            RangeResistance = 2,
                            Level = 10,
                            ExpeditionId = upiornaPolana.Id
                        },
                        new Hussar()
                        {
                            ExpeditionId = upiornaPolana.Id,
                            Name = "Koń na biegunach",
                            Level = 10,
                            CurrentHP = 80,
                            MaxHP = 80,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 60,
                            PhysicalResistance = 3.25,
                            RangeResistance = 0.5,
                        },
                        new Hussar()
                        {
                            ExpeditionId = upiornaPolana.Id,
                            Name = "Koń na biegunach",
                            Level = 10,
                            CurrentHP = 80,
                            MaxHP = 80,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 60,
                            PhysicalResistance = 3.25,
                            RangeResistance = 0.5,
                        },
                        new Kamikadze()
                        {
                            ExpeditionId = upiornaPolana.Id,
                            Name = "Styrta",
                            CurrentHP = 15,
                            MaxHP = 15,
                            AttackSpeed = 0.8,
                            DamageType = Damage.Destruction,
                            Damage = 150,
                            PhysicalResistance = 0.6,
                            RangeResistance = 0.5,
                            Level = 10,
                        },
                        new Trojan()
                        {
                            ExpeditionId = upiornaPolana.Id,
                            Name = "Straszak na wróble",
                            Level = 10,
                            CurrentHP = 300,
                            MaxHP = 300,
                            AttackSpeed = 1,
                            DamageType = Damage.Physical,
                            Damage = 0,
                            PhysicalResistance = 5.25,
                            RangeResistance = 3.25,
                        },
                    };
                    resources = new List<MiniProjekt.Resource>
                        {
                        new MiniProjekt.Resource("Gold", 400)
                        { ExpeditionId = upiornaPolana.Id },
                        new MiniProjekt.Resource("Grain", 500)
                        { ExpeditionId = upiornaPolana.Id }
                        };

                    context.AddRange(resources);
                    context.AddRange(entities);
                    context.SaveChanges();
                    // 13 lvl
                    var mroczneLochy = new Expedition()
                    {
                        Name = "Mroczne Lochy",
                        Level = 13,
                        ExperienceGained = 350,
                        Cooldown = 140,
                        Image = "lochy.jpg"
                    };
                    context.Add(mroczneLochy);
                    context.SaveChanges();
                    entities = new List<Entity>
                    {
                        new Warrior()
                        {
                            Name = "Upiór Zmierzchu",
                            CurrentHP = 80,
                            MaxHP = 80,
                            AttackSpeed = 2,
                            DamageType = Damage.Physical,
                            Damage = 100,
                            PhysicalResistance = 3.5,
                            RangeResistance = 4.2,
                            Level = 13,
                            ExpeditionId = mroczneLochy.Id
                        },
                        new Warrior()
                        {
                            Name = "Upiór Zmierzchu",
                            CurrentHP = 80,
                            MaxHP = 80,
                            AttackSpeed = 2,
                            DamageType = Damage.Physical,
                            Damage = 100,
                            PhysicalResistance = 3.5,
                            RangeResistance = 4.2,
                            Level = 13,
                            ExpeditionId = mroczneLochy.Id
                        },
                        new Warrior()
                        {
                            Name = "Upiór Zmierzchu",
                            CurrentHP = 80,
                            MaxHP = 80,
                            AttackSpeed = 2,
                            DamageType = Damage.Physical,
                            Damage = 100,
                            PhysicalResistance = 3.5,
                            RangeResistance = 4.2,
                            Level = 13,
                            ExpeditionId = mroczneLochy.Id
                        },
                        new Warrior()
                        {
                            Name = "Upiór Zgnilizny",
                            CurrentHP = 60,
                            MaxHP = 60,
                            AttackSpeed = 7,
                            DamageType = Damage.Physical,
                            Damage = 130,
                            PhysicalResistance = 2.5,
                            RangeResistance = 2.4,
                            Level = 14,
                            ExpeditionId = mroczneLochy.Id
                        },
                        new Warrior()
                        {
                            Name = "Upiór Zgnilizny",
                            CurrentHP = 60,
                            MaxHP = 60,
                            AttackSpeed = 7,
                            DamageType = Damage.Physical,
                            Damage = 130,
                            PhysicalResistance = 2.5,
                            RangeResistance = 2.4,
                            Level = 14,
                            ExpeditionId = mroczneLochy.Id
                        },
                        new Hussar()
                        {
                            ExpeditionId = mroczneLochy.Id,
                            Name = "Demon cienia",
                            Level = 13,
                            CurrentHP = 120,
                            MaxHP = 120,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 90,
                            PhysicalResistance = 3.25,
                            RangeResistance = 0.5,
                        },
                        new Hussar()
                        {
                            ExpeditionId = mroczneLochy.Id,
                            Name = "Demon cienia",
                            Level = 13,
                            CurrentHP = 120,
                            MaxHP = 120,
                            AttackSpeed = 6,
                            DamageType = Damage.Physical,
                            Damage = 90,
                            PhysicalResistance = 3.25,
                            RangeResistance = 0.5,
                        },
                        new Archer()
                        {
                            Name = "Szkieletowy Łucznik",
                            CurrentHP = 80,
                            MaxHP = 80,
                            AttackSpeed = 4,
                            DamageType = Damage.Range,
                            Damage = 100,
                            PhysicalResistance = 2.2,
                            RangeResistance = 2.5,
                            Level = 13,
                            ExpeditionId = mroczneLochy.Id
                        },
                        new Archer()
                        {
                            Name = "Szkieletowy Łucznik",
                            CurrentHP = 80,
                            MaxHP = 80,
                            AttackSpeed = 4,
                            DamageType = Damage.Range,
                            Damage = 100,
                            PhysicalResistance = 2.2,
                            RangeResistance = 2.5,
                            Level = 13,
                            ExpeditionId = mroczneLochy.Id
                        },
                        new Kamikadze()
                        {
                            ExpeditionId = mroczneLochy.Id,
                            Name = "Zgnilizna mięsa",
                            CurrentHP = 30,
                            MaxHP = 30,
                            AttackSpeed = 0.6,
                            DamageType = Damage.Destruction,
                            Damage = 300,
                            PhysicalResistance = 0.6,
                            RangeResistance = 0.5,
                            Level = 13,
                        }
                    };
                    context.AddRange(entities);
                    resources = new List<MiniProjekt.Resource>
                        {
                        new MiniProjekt.Resource("Iron", 500)
                        { ExpeditionId = mroczneLochy.Id },
                        new MiniProjekt.Resource("Stone", 500)
                        { ExpeditionId = mroczneLochy.Id },
                        new MiniProjekt.Resource("Gold", 650)
                        { ExpeditionId = mroczneLochy.Id }
                        };

                    context.AddRange(resources);
                    context.SaveChanges();
                    // lvl 15
                    var lowczyniGlow = new Expedition()
                    {
                        Name = "Łowczyni głów",
                        Level = 15,
                        ExperienceGained = 380,
                        Cooldown = 250,
                        Image = "lowczyni.jpg"
                    };
                    context.Add(lowczyniGlow);
                    context.SaveChanges();
                    var lowczyni = new Archer()
                    {
                        Name = "Łowczyni Głów [Boss]",
                        CurrentHP = 1500,
                        MaxHP = 1500,
                        AttackSpeed = 10,
                        DamageType = Damage.Range,
                        Damage = 500,
                        PhysicalResistance = 3,
                        RangeResistance = 7,
                        Level = 15,
                        ExpeditionId = lowczyniGlow.Id
                    };
                    context.Add(lowczyni);
                    resources = new List<MiniProjekt.Resource>
                        {
                        new MiniProjekt.Resource("Gold", 1500)
                        { ExpeditionId = lowczyniGlow.Id },
                        new MiniProjekt.Resource("Wood", 1200)
                        { ExpeditionId = lowczyniGlow.Id },
                        new MiniProjekt.Resource("Wheat", 1000)
                        { ExpeditionId = lowczyniGlow.Id },
                        };
                    context.AddRange(resources);
                    context.SaveChanges();
                    var eldtrichTyran = new Expedition()
                    {
                        Name = "Eldtrich Tyran",
                        Level = 20,
                        ExperienceGained = 1000,
                        Cooldown = 500,
                        Image = "tyran.jpg"
                    };
                    context.Add(eldtrichTyran);
                    context.SaveChanges();
                    var eldtrich = new Hussar()
                    {
                        Name = "Eldtrich Tyran [Boss]",
                        CurrentHP = 3000,
                        MaxHP = 3000,
                        AttackSpeed = 20,
                        DamageType = Damage.Physical,
                        Damage = 1100,
                        PhysicalResistance = 7,
                        RangeResistance = 7,
                        Level = 20,
                        ExpeditionId = eldtrichTyran.Id
                    };
                    context.Add(eldtrich);
                    context.SaveChanges();
                    resources = new List<MiniProjekt.Resource>
                    {
                        new MiniProjekt.Resource("Gold", 2000)
                        { ExpeditionId = eldtrichTyran.Id},
                        new MiniProjekt.Resource("Wood", 2000)
                        { ExpeditionId = eldtrichTyran.Id},
                        new MiniProjekt.Resource("Stone", 2000)
                        { ExpeditionId = eldtrichTyran.Id},
                        new MiniProjekt.Resource("Iron", 2000)
                        { ExpeditionId = eldtrichTyran.Id
                        },
                        new MiniProjekt.Resource("Wheat", 2000)
                        { ExpeditionId = eldtrichTyran.Id
                        }
                    };
                    context.AddRange(resources);
                    context.SaveChanges();
                }
            }
        }
    }
}
