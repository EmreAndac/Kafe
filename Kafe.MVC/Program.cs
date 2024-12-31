using Kafe.MVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Kafe.DAL.DbContexts;
using Kafe.DAL.Repositories.Abstract;
using Kafe.DAL.Repositories.Concrete;
using Kafe.BL.Manager.Abstract;
using Kafe.BL.Manager.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Kafe.Entities.Entity.Concrete;

namespace Kafe.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region DbContext Registration
            var constr = builder.Configuration.GetConnectionString("Kafe");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(constr));
            #endregion

            #region Notify Service Configuration
            builder.Services.AddNotyf(config =>
            {
                config.Position = NotyfPosition.BottomRight;
                config.DurationInSeconds = 7;
                config.IsDismissable = true;
            });
            #endregion

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.Cookie.Name = "Kafe";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/ErisimHatasi";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.SlidingExpiration = true;
            });

            // Custom service registrations
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();
            builder.Services.AddScoped<IRepository<Table>, Repository<Table>>();

            // Register IManager services
            builder.Services.AddScoped<IManager<MyUser>, Manager<MyUser>>();
            builder.Services.AddScoped<IManager<Table>, Manager<Table>>();
            builder.Services.AddScoped<IManager<Category>, Manager<Category>>(); // Eksik kayýt eklendi
            builder.Services.AddScoped<IManager<Product>, Manager<Product>>();
            builder.Services.AddScoped<IManager<Order>, Manager<Order>>();
            builder.Services.AddScoped<IManager<OrderItem>, Manager<OrderItem>>();
            


            builder.Services.AddNotyf(config =>
            {
                config.Position = NotyfPosition.BottomRight;
                config.DurationInSeconds = 7;
                config.IsDismissable = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNotyf();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
