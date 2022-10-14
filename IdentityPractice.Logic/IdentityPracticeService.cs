using IdentityPractice.Data;
using IdentityPractice.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityPractice.Logic
{
    public class IdentityPracticeService
    {
        private readonly IdentityPracticeDbContext dbcontext;

        public IdentityPracticeService(IdentityPracticeDbContext dbcontext)
        {
            this.dbcontext=dbcontext;
        }

        public List<UserViewModel> GetAllUsers(int? Id)
        {
            var users = dbcontext.ApplicationUsers.Select(x => new UserViewModel { userId=x.Id, Email=x.Email }).ToList();
            if (Id is not null)
            {
                users = users.Where(x => x.userId==Id).ToList();
            }

            return users;
        }
    }
}
