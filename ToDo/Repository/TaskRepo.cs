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

        public void AddTask(string title, string description) => _toDoTasks.Add(new(title, description));

        public ToDoTask? GetTask(string guid) => _toDoTasks.Find(task => task.GUID.ToString() == guid);

        public void EditTask(string guid, string title, string description, int priority, bool isCompleted)
        {
            ToDoTask? task = GetTask(guid);
            task.Title = !string.IsNullOrEmpty(title) ? title : task.Title;
            task.Description = !string.IsNullOrEmpty(description) ? description : task.Description;
            task.TaskPriority = (ToDoTask.Priority)priority;
            task.IsCompleted = isCompleted;
        }

        public void DeleteTask(string guid) => _toDoTasks.Remove(GetTask(guid));
    }
}
