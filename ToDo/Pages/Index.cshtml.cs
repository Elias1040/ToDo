using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using ToDo.Models;
using ToDo.Repository;

namespace ToDo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITaskRepo _repo;
        public List<ToDoTask> ToDoTasks { get; set; }

        [BindProperty, MaxLength(25, ErrorMessage = "Title can't contain more than 25 characters")]
        public string? Title { get; set; }

        [BindProperty, MaxLength(25, ErrorMessage = "Description can't contain more than 25 characters")]
        public string? Description { get; set; }

        [BindProperty]
        public Guid GUID { get; set; }


        [BindProperty]
        public ToDoTask.Priority Priority { get; set; }

        [BindProperty]
        public bool IsCompleted { get; set; }

        [BindProperty]
        public string? Contributers { get; set; }
        public string? Error { get; set; }

        public IndexModel(ITaskRepo repo, IUserRepo userRepo)
        {
            _repo = repo;
        }

        public IActionResult OnGet(string? error)
        {
            if (String.IsNullOrWhiteSpace(HttpContext.Session.GetString("UserID")))
            {
                return RedirectToPage("Login");
            }
            else
            {
                _repo.LoadTasks(HttpContext.Session.GetString("UserID"));
                ToDoTasks = _repo.GetAllTasks();
                Error = error;
                return Page();
            }
        }

        public IActionResult OnPostAdd()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("AddTask", new { title = Title, description = Description, priority = (int)Priority, contributers = !string.IsNullOrWhiteSpace(Contributers)?Contributers.Trim():string.Empty });
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return RedirectToPage("Index", new { error = messages });
            }

        }

        public IActionResult OnPostEdit(string guid)
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("UpdateTask", new { guid, title = Title, description = Description, contributers = Contributers, priority = (int)Priority, isCompleted = IsCompleted });
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return RedirectToPage("Index", new { error = messages });
            }
        }


        public IActionResult OnPostComplete()
        {
            return RedirectToPage("CompleteTask", new { guid = GUID });
        }

        public IActionResult OnPostDelete()
        {
            return RedirectToPage("DeleteTask", new { guid = GUID });
        }

        public IActionResult OnPostDeleteCompletedTasks()
        {
            _repo.DeleteCompletedTasks(HttpContext.Session.GetString("UserID"));
            return RedirectToPage();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("UserID");
            _repo.ClearTaskList();
            return RedirectToPage("Login");
        }
    }
}