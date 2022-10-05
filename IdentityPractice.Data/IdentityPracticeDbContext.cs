using IdentityPractice.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdentityPractice.Data
{
    public class IdentityPracticeDbContext : DbContext
    {
        public IdentityPracticeDbContext(DbContextOptions<IdentityPracticeDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Vehicles).WithOne(v => v.Owner).HasForeignKey(v => v.Id).HasPrincipalKey(u => u.Id);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
