﻿using ToDo.Models;

namespace ToDo.Repository
{
    public interface ITaskRepo
    {
        void AddTask(ToDoTask task, string? contributers, string userID);
        void ClearTaskList();
        void CompleteTask(Guid guid);
        void DeleteCompletedTasks(string userID);
        void DeleteTask(Guid guid);
        void EditTask(ToDoTask task, string? contributers);
        List<ToDoTask> GetAllTasks();
        ToDoTask? GetTask(Guid guid);
        void LoadTasks(string userID);
    }
}
