using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Repository;

namespace ToDo.Pages
{
    public class EditTaskModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public int Priority { get; set; }
        [BindProperty]
        public bool IsCompleted { get; set; }


        private readonly ITaskRepo _repo;
        public EditTaskModel(ITaskRepo repo)
        {
            _repo = repo;
        }

        public void OnGet()
        {
        }

        public void OnPost(string guid)
        {
            _repo.EditTask(guid, Title, Description, Priority, IsCompleted);
        }
    }
}
