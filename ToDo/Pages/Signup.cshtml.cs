using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ToDo.Repository;

namespace ToDo.Pages
{
    public class SignupModel : PageModel
    {
        [BindProperty, Required]
        public string Username { get; set; }
        [BindProperty, Required]
        public string Name { get; set; }
        [BindProperty, Required]
        public string Password { get; set; }
        [BindProperty, Required, Compare(nameof(Password))]
        public string ConfPassword { get; set; }

        private readonly IUserRepo _userRepo;

        public SignupModel(IUserRepo repo)
        {
            _userRepo = repo;
        }

        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                _userRepo.AddUser(Name, Username, Password);
            }
        }
    }
}
