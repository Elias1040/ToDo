using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using ToDo.Models;
using ToDo.Repository;

namespace ToDo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITaskRepo _repo;
        public List<ToDoTask> ToDoTasks { get; set; }
        [BindProperty]
        public string GUID { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public int Priority { get; set; }
        [BindProperty]
        public bool IsCompleted { get; set; }

        public IndexModel(ITaskRepo repo)
        {
            _repo = repo;
            ToDoTasks = repo.GetAllTasks();
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostEdit()
        {
            _repo.EditTask(GUID, Title, Description, Priority, IsCompleted);
            return RedirectToPage();
        }

        public IActionResult OnPostAdd()
        {
            _repo.AddTask(Title, Description, Priority);
            return RedirectToPage();
        }

        public IActionResult OnPostComplete()
        {
            _repo.CompleteTask(GUID);
            return RedirectToPage();
        }
    }
}