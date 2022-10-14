using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityPractice.Logic.SeedData
{
    public interface IUserSeedService
    {
        Task SeedUserAsync();
    }
}
