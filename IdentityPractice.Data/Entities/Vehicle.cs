using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityPractice.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public bool IsActive { get; set; }


        public ApplicationUser owner { get; set; }
    }
}
