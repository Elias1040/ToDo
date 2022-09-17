using ToDo.Models;

namespace ToDo.Repository
{
    public class TaskRepo : ITaskRepo
    {
        private readonly List<ToDoTask> _toDoTasks;

        public TaskRepo()
        {
            _toDoTasks = new List<ToDoTask>();
        }

        public void AddTask(string title, string description, int priority) => _toDoTasks.Add(new(title, description, (ToDoTask.Priority)priority));

        public ToDoTask? GetTask(string guid) => _toDoTasks.Find(task => task.GUID.ToString() == guid);

        public List<ToDoTask> GetAllTasks() => _toDoTasks.OrderBy(task => task.Created).ToList();

        public void EditTask(string guid, string? title, string? description, int priority, bool isCompleted)
        {
            ToDoTask? task = GetTask(guid);
            task.Title = !string.IsNullOrWhiteSpace(title) ? title : task.Title;
            task.Description = !string.IsNullOrWhiteSpace(description) ? description : task.Description;
            task.TaskPriority = (ToDoTask.Priority)priority;
            task.IsCompleted = isCompleted;
        }

        public void DeleteTask(string guid) => _toDoTasks.Remove(GetTask(guid));

        public void DeleteCompletedTasks() => _toDoTasks.RemoveAll(task => task.IsCompleted);

        public void CompleteTask(string guid) => GetTask(guid).IsCompleted = true;

    }
}
