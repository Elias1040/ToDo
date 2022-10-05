using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Models;
using ToDo.Repository;

namespace ToDo.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        private readonly IUserRepo _userRepo;
        public LoginModel(IUserRepo repo)
        {
            _userRepo = repo;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            User? user = _userRepo.GetUser(Username, Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserID", user.GUID.ToString());
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
