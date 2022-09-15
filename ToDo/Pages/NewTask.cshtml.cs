using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Models;
using ToDo.Repository;

namespace ToDo.Pages
{
    public class NewTaskModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public int Priority { get; set; }

        private readonly ITaskRepo _repo;
        public NewTaskModel(ITaskRepo repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            _repo.AddTask(Title, Description, Priority);
        }
    }
}
