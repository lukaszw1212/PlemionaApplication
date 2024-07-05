using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlemionaApplication.Data;
namespace PlemionaApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<PlemionaApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PlemionaApplicationContext") ?? throw new InvalidOperationException("Connection string 'PlemionaApplicationContext' not found.")));
<<<<<<< HEAD
           
=======

>>>>>>> 152dddba05b2968febda3e3864ac705d596c437a
            // Add services to the container.
            builder.Services.AddControllersWithViews();            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();


        }
    }
}
