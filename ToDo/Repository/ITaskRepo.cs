using ToDo.Models;

namespace ToDo.Repository
{
    public interface ITaskRepo
    {
        void AddTask(string title, string description, int priority);
        void CompleteTask(string guid);
        void DeleteTask(string guid);
        void EditTask(string guid, string? title, string? description, int priority, bool isCompleted);
        List<ToDoTask> GetAllTasks();
        ToDoTask? GetTask(string guid);
    }
}
