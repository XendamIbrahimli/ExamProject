using ExamProject.BL.Extensions;
using ExamProject.Core.Models;
using ExamProject.DAL.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(x=>x.UseSqlServer
                (builder.Configuration.GetConnectionString("local")));
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<User, IdentityRole>(x =>
            {
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireDigit = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireLowercase = false;
                x.Password.RequiredLength = 3;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.ConfigureApplicationCookie(x =>
            {
                x.AccessDeniedPath = "/Home/AccessDenied";
            });

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

            app.UseUserSeed();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
