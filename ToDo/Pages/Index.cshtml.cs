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

        [BindProperty, MaxLength(25, ErrorMessage = "Max 25 characters"), Required]
        public string Title { get; set; }

        [BindProperty, MaxLength(25, ErrorMessage = "Max 25 characters"), Required]
        public string Description { get; set; }

        [BindProperty, Range(0, 2), Required]
        public int Priority { get; set; }

        [BindProperty, Required]
        public bool IsCompleted { get; set; }

        public IndexModel(ITaskRepo repo)
        {
            _repo = repo;
            ToDoTasks = repo.GetAllTasks();
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostAdd()
        {
            if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Description))
            {

                if (Title.Length <= 25 && Description.Length <= 25)
                {
                    _repo.AddTask(Title, Description, Priority);
                    return RedirectToPage();
                }
            }
            return Page();
        }

        public IActionResult OnPostEdit()
        {
            if (Title.Length <= 25 && Description.Length <= 25 && !string.IsNullOrEmpty(GUID))
            {
                _repo.EditTask(GUID, Title, Description, Priority, IsCompleted);
                return RedirectToPage();
            }
            return Page();
        }


        public IActionResult OnPostComplete()
        {
            if (!string.IsNullOrEmpty(GUID))
            {
                _repo.CompleteTask(GUID);
                return RedirectToPage();
            }
            return Page();
        }
    }
}