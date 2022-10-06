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
        [BindProperty, Required, Compare(nameof(Password), ErrorMessage = "The Confirm Password field is required.")]
        public string ConfirmPassword { get; set; }

        private readonly IUserRepo _userRepo;

        public SignupModel(IUserRepo repo)
        {
            _userRepo = repo;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("UserID", _userRepo.AddUser(Name, Username, Password).GUID.ToString());
                return RedirectToPage("Index");
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                return RedirectToPage("Signup", new { error = messages });
            }
        }
    }
}
