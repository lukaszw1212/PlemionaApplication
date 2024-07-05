using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlemionaApplication.Entities;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
               .HasMany(e => e.Users)
               .WithOne(e => e.UserRole)
               .HasForeignKey(e => e.RoleId)
               .IsRequired();
        }
    }
}
