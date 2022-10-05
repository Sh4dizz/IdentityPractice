using IdentityPractice.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityPractice.Data.Entities
{
    public class Vehicle:IEntityTypeConfiguration<Vehicle>
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public VehicleTypeEnum Type { get; set; }
        public bool IsActive { get; set; }


        public ApplicationUser Owner { get; set; }

        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.Property("Manufacturer").HasMaxLength(50);
            builder.Property("Model").HasMaxLength(50);
        }
    }
}
