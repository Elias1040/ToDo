namespace ToDo.Models
{
    public class ToDoTask
    {
        public Guid GUID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; init; }
        public Priority TaskPriority { get; set; }
        public bool IsCompleted { get; set; }

        public enum Priority { Low, Normal, High}

        public ToDoTask(string title, string description)
        {
            GUID = Guid.NewGuid();
            Created = DateTime.Now;
            Title = title;
            Description = description;
            TaskPriority = Priority.Normal;
            IsCompleted = false;
        }
    }
}
