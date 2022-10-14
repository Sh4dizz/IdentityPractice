using IdentityPractice.Data;
using IdentityPractice.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityPractice.Logic.SeedData
{
    public class UserSeedService : IUserSeedService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IdentityPracticeDbContext dbContext;

        public UserSeedService(UserManager<ApplicationUser> userManager,IdentityPracticeDbContext dbContext)
        {
            this.userManager=userManager;
            this.dbContext=dbContext;
        }

        public async Task SeedUserAsync()
        {
            if (!(await userManager.GetUsersInRoleAsync("Admin")).Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@identitypractice.hu",
                    Email="admin@identitypractice.hu",
                    SecurityStamp=Guid.NewGuid().ToString()
                };

                var createResult = await userManager.CreateAsync(user,"123456aA");

                if (userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var result = await userManager.ConfirmEmailAsync(user, code);
                }

                var addtoRole = await userManager.AddToRoleAsync(user, "Admin");

                if (!createResult.Succeeded || !addtoRole.Succeeded)
                {
                    throw new ApplicationException("Cannot create user with: "+String.Join(",", createResult.Errors.Concat(addtoRole.Errors).Select(e => e.Description)));
                }
            }

            if (!(await userManager.GetUsersInRoleAsync("User")).Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "user@identitypractice.hu",
                    Email="user@identitypractice.hu",
                    SecurityStamp=Guid.NewGuid().ToString()
                };

                var createResult = await userManager.CreateAsync(user, "123456aA");

                if (userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var result = await userManager.ConfirmEmailAsync(user, code);
                }

                var addtoRole = await userManager.AddToRoleAsync(user, "User");

                if (!createResult.Succeeded || !addtoRole.Succeeded)
                {
                    throw new ApplicationException("Cannot create user with: "+String.Join(",", createResult.Errors.Concat(addtoRole.Errors).Select(e => e.Description)));
                }
            }
        }

        
    }
}
