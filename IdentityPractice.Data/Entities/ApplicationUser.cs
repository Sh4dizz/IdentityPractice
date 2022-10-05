using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityPractice.Data.Entities
{
    public class ApplicationUser : IdentityUser<int>, IEntityTypeConfiguration<ApplicationUser>
    {       
        public string? FullName { get; set; }
        public int? Age { get; set; }


        public ICollection<Vehicle>? Vehicles { get; set; }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property("FullName").HasMaxLength(50);
        }
    }
}
