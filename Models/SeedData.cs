using Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using PlemionaApplication.Data;
using PlemionaApplication.Entities;
using PlemionaApplication.Models;
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
            }
        }
    }
}
