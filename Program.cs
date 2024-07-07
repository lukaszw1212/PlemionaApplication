using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlemionaApplication.Data;
using PlemionaApplication.Models;

namespace PlemionaApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<PlemionaApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PlemionaApplicationContext") ?? throw new InvalidOperationException("Connection string 'PlemionaApplicationContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Dodajemy obs³ugê uwierzytelniania za pomoc¹ ciasteczek
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.LogoutPath = "/Home/Logout";
                });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas trwania sesji
                options.Cookie.HttpOnly = true; // Bezpieczeñstwo ciasteczek
                options.Cookie.IsEssential = true; // Konieczne dla pracy aplikacji
            });

            var app = builder.Build();


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedData.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication(); // Dodajemy middleware uwierzytelniania
            app.UseAuthorization();

            



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
