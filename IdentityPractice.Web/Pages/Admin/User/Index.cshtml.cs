using IdentityPractice.Logic;
using IdentityPractice.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityPractice.Web.Pages.Admin.User
{
    public class IndexModel : PageModel
    {
        private readonly IdentityPracticeService service;

        public IndexModel(IdentityPracticeService service)
        {
            this.service=service;
        }
        public void OnGet()
        {
            Users = service.GetAllUsers(null);
        }

        public List<UserViewModel> Users { get; set; }
    }
}
