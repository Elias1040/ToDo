using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using ToDo.Models;
using ToDo.Repository;

namespace ToDo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITaskRepo _repo;
        public List<ToDoTask> ToDoTasks { get; set; }

        [BindProperty, Required]
        public string GUID { get; set; }

        [BindProperty, MaxLength(25)]
        public string Title { get; set; }

        [BindProperty, MaxLength(25)]
        public string Description { get; set; }

        [BindProperty, Range(0, 2), Required]
        public int Priority { get; set; }

        [BindProperty, Required]
        public bool IsCompleted { get; set; }

        public string? Error { get; set; }

        public IndexModel(ITaskRepo repo)
        {
            _repo = repo;
            ToDoTasks = repo.GetAllTasks();
        }

        public void OnGet(string? error)
        {
            Error = error;
        }

        public IActionResult OnPostAdd()
        {
            if (!string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Description))
            {

                if (Title.Length <= 25 && Description.Length <= 25)
                {
                    _repo.AddTask(Title, Description, Priority);
                    return RedirectToPage();
                }
            }
            return RedirectToPage("Index", new { error = "The fields Title or Description cannot be empty or contain more than 25 characters when adding a task" });
        }

        public IActionResult OnPostEdit()
        {
            if (!(string.IsNullOrWhiteSpace(Title) | string.IsNullOrWhiteSpace(Description)) && (Title?.Length <= 25 && Description?.Length <= 25))
            {
                _repo.EditTask(GUID, Title, Description, Priority, IsCompleted);
            }
            else if (string.IsNullOrWhiteSpace(Title) && string.IsNullOrWhiteSpace(Description))
            {
                _repo.EditTask(GUID, Title, Description, Priority, IsCompleted);

            }
            else if (!string.IsNullOrWhiteSpace(Title) && Title.Length <= 25)
            {
                _repo.EditTask(GUID, Title, Description, Priority, IsCompleted);
            }
            else if (!string.IsNullOrWhiteSpace(Description) && Description.Length <= 25)
            {
                _repo.EditTask(GUID, Title, Description, Priority, IsCompleted);
            }
            return RedirectToPage();
        }


        public IActionResult OnPostComplete()
        {
            if (!string.IsNullOrWhiteSpace(GUID))
            {
                _repo.CompleteTask(GUID);
                return RedirectToPage();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDelete()
        {
            _repo.DeleteTask(GUID);
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteCompletedTasks()
        {
            _repo.DeleteCompletedTasks();
            return RedirectToPage();
        }
    }
}