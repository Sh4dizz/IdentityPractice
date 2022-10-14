using IdentityPractice.Data;
using IdentityPractice.Data.Entities;
using IdentityPractice.Logic;
using IdentityPractice.Logic.SeedData;
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

            builder.Services.AddScoped<IdentityPracticeService>();

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

            builder.Services.AddScoped<IRoleSeedService, RoleSeedService>();
            builder.Services.AddScoped<IUserSeedService, UserSeedService>();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization(options => 
            options.AddPolicy("RequireAdminRole",policy => policy.RequireRole("Admin"))
            );

            builder.Services.AddRazorPages(options =>
            options.Conventions.AuthorizeFolder("/Admin","RequireAdminRole")
            );


            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VVhjQlFaclhJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRd0ViWn5dcXNUTmFYWEQ=");

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

            using (var scope = app.Services.CreateScope())
            {
                using var dbContext = scope.ServiceProvider.GetRequiredService<IdentityPracticeDbContext>();
                var migrations = dbContext.Database.GetMigrations().ToHashSet();
                if (dbContext.Database.GetAppliedMigrations().Any(a => !migrations.Contains(a)))
                    throw new InvalidOperationException("There is already a migration running on the database that has since been deleted from the project. Delete the database or correct the status of the migrations, then restart the application!");
                dbContext.Database.Migrate();

                var roleSeeder = scope.ServiceProvider.GetRequiredService<IRoleSeedService>();
                roleSeeder.SeedRoleAsync().Wait();

                var userSeeder = scope.ServiceProvider.GetRequiredService<IUserSeedService>();
                userSeeder.SeedUserAsync().Wait();
            }

            app.Run();
        }
    }
}