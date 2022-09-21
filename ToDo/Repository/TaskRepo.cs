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

        public void AddTask(ToDoTask task) => _toDoTasks.Add(task);

        public ToDoTask? GetTask(Guid guid) => _toDoTasks.SingleOrDefault(task => task.GUID == guid);

        public List<ToDoTask> GetAllTasks() => _toDoTasks.OrderBy(task => task.Created).ToList();

        public void EditTask(ToDoTask toDoTask)
        {
            ToDoTask? task = GetTask(toDoTask.GUID);
            task.Title = !string.IsNullOrWhiteSpace(toDoTask.Title) ? toDoTask.Title : task.Title;
            task.Description = !string.IsNullOrWhiteSpace(toDoTask.Description) ? toDoTask.Description : task.Description;
            task.TaskPriority = toDoTask.TaskPriority;
            task.IsCompleted = toDoTask.IsCompleted;
        }

        public void DeleteTask(Guid guid) => _toDoTasks.Remove(GetTask(guid));

        public void DeleteCompletedTasks() => _toDoTasks.RemoveAll(task => task.IsCompleted);

        public void CompleteTask(Guid guid) => GetTask(guid).IsCompleted = true;

    }
}
