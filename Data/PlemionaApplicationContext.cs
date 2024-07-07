using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlemionaApplication.Entities;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniProjekt;
using PlemionaApplication.Models;
using PlemionaApplication.Models.Building;


namespace PlemionaApplication.Data
{
    public class PlemionaApplicationContext : DbContext
    {
        public PlemionaApplicationContext (DbContextOptions<PlemionaApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<PlemionaApplication.Entities.Role> Role { get; set; } = default!;
        public DbSet<PlemionaApplication.Entities.User> User { get; set; } = default!;

        public DbSet<Archer> Archer { get; set; } = default!;
        public DbSet<Armory> Armory { get; set; } = default!;
        public DbSet<Barracks> Barracks { get; set; } = default!;
        public DbSet<Catapult> Catapult { get; set; } = default!;
        public DbSet<DefensiveWalls> DefensiveWalls { get; set; } = default!;
        public DbSet<Resource> Resource { get; set; } = default!;
        public DbSet<MiniProjekt.Expedition> Expedition { get; set; } = default!;

        public DbSet<Player> Players { get; set; }
        public DbSet<Fraction> Fractions { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Village> Villages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracja relacji między Player a Fraction
            modelBuilder.Entity<Fraction>()
                .HasOne(f => f.GuildMaster)
                .WithMany()
                .HasForeignKey(f => f.GuildMasterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.Fraction)
                .WithMany(f => f.Players)
                .HasForeignKey(p => p.FractionId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Role>()
               .HasMany(e => e.Users)
               .WithOne(e => e.UserRole)
               .HasForeignKey(e => e.RoleId)
               .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Konfiguracja połączenia z bazą danych (przykładowe ustawienia)
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PlemionaApplicationContext-a300a3fd-7cab-4ada-b8fe-63d48bddc173;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        public DbSet<MiniProjekt.GrainFarm> GrainFarm { get; set; } = default!;
        public DbSet<MiniProjekt.HorseStable> HorseStable { get; set; } = default!;
        public DbSet<MiniProjekt.Hussar> Hussar { get; set; } = default!;
        public DbSet<MiniProjekt.IronMine> IronMine { get; set; } = default!;
        public DbSet<MiniProjekt.Kamikadze> Kamikadze { get; set; } = default!;
        public DbSet<MiniProjekt.Sawmill> Sawmill { get; set; } = default!;
        public DbSet<MiniProjekt.Silo> Silo { get; set; } = default!;
        public DbSet<MiniProjekt.StoneMine> StoneMine { get; set; } = default!;
        public DbSet<MiniProjekt.TownHall> TownHall { get; set; } = default!;
        public DbSet<MiniProjekt.Trojan> Trojan { get; set; } = default!;
        public DbSet<MiniProjekt.Warrior> Warrior { get; set; } = default!;
    }
}
