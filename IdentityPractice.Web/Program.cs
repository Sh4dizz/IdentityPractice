using IdentityPractice.Data;
using IdentityPractice.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityPractice.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<IdentityPracticeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityPracticeDb")));
            
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>().AddEntityFrameworkStores<IdentityPracticeDbContext>().AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options => 
            { 
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
            });

            builder.Services.ConfigureApplicationCookie(options => 
            {
             options.Cookie.HttpOnly = true;
             options.Cookie.IsEssential=true;
             options.LoginPath="/Identity/Account/Login";
             options.AccessDeniedPath="/Identity/Account/AccessDenied";
             options.SlidingExpiration=true;
            });

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}