using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ToDo.Models;
using ToDo.Repository;

namespace ToDo.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty, Required]
        public string Username { get; set; }
        [BindProperty, Required]
        public string Password { get; set; }

        public string? Error { get; set; }

        private readonly IUserRepo _userRepo;
        public LoginModel(IUserRepo repo)
        {
            _userRepo = repo;
        }
        public void OnGet(string? error)
        {
            Error = error;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                User? user = _userRepo.GetUser(Username, Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("UserID", user.GUID.ToString());
                    return RedirectToPage("Index");
                }
                else
                {
                    return RedirectToPage("Login", new { error = "Incorrect username or password" });
                }
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return RedirectToPage("Login", new { error = messages});
            }
        }
    }
}
