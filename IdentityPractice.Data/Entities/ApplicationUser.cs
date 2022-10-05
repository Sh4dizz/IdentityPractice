using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityPractice.Data.Entities
{
    public class ApplicationUser : IEntityTypeConfiguration<ApplicationUser>
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }


        public ICollection<Vehicle>? Vehicles { get; set; }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property("FullName").HasMaxLength(50);
        }
    }
}
