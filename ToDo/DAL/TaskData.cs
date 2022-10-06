using System.Data.SqlClient;
using System.Data;
using System;
using ToDo.Models;
using System.Threading.Tasks;

namespace ToDo.DAL
{
    public class TaskData
    {
        private readonly string connectionString;
        public TaskData(IConfiguration config)
        {
            connectionString = config.GetConnectionString("Default");
        }

        public List<ToDoTask> GetTasks(string userID)
        {
            List<ToDoTask> taskList = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Guid.TryParse(userID, out Guid guid);
                SqlCommand cmd = new SqlCommand("GetTasks", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Guid", guid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Guid.TryParse(reader.GetString("TaskID"), out Guid taskID);
                    ToDoTask task = new ToDoTask()
                    {
                        GUID = taskID,
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        IsCompleted = reader.GetBoolean("Completed"),
                        Created = reader.GetDateTime("Created"),
                        TaskPriority = (ToDoTask.Priority)reader.GetInt32("Priority")
                    };
                    taskList.Add(task);
                }
            };
            return taskList;
        }

        public ToDoTask GetTask(string userID, string taskID)
        {
            ToDoTask task = new ToDoTask();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTask", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                Guid.TryParse(userID, out Guid guid);
                cmd.Parameters.AddWithValue("@UserID", guid);
                Guid.TryParse(taskID, out guid);
                cmd.Parameters.AddWithValue("@TaskID", guid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    task = new ToDoTask()
                    {
                        GUID = guid,
                        Title = reader.GetString("Title"),
                        Description = reader.GetString("Description"),
                        IsCompleted = reader.GetBoolean("Completed"),
                        Created = reader.GetDateTime("Created"),
                        TaskPriority = (ToDoTask.Priority)reader.GetInt32("Priority")
                    };
                }
            };
            return task;
        }

        public ToDoTask UpdateTask(ToDoTask task)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new("UpdateTask", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskID", task.GUID);
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@Priority", (int)task.TaskPriority);
                cmd.Parameters.AddWithValue("@Completed", task.IsCompleted);
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            if (rowsAffected > 0)
            {
                return task;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }

        public ToDoTask AddTask(ToDoTask task, string userID)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Guid.TryParse(userID, out Guid guid);
                SqlCommand cmd = new("AddTask", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Guid", guid);
                cmd.Parameters.AddWithValue("@TaskID", task.GUID);
                cmd.Parameters.AddWithValue("@Title", task.Title);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@Priority", (int)task.TaskPriority);
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            if (rowsAffected > 0)
            {
                return task;
            }
            else
            {
                throw new Exception("Something went wrong");
            }
        }

        public ToDoTask DeleteTask(ToDoTask task)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new("DeleteTask", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskID", task?.GUID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            if (rowsAffected > 0)
            {
                return task;
            }
            else
            {
                throw new CustomException("Something went wrong");
            }
        }

        public ToDoTask CompleteTask(ToDoTask task)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new("CompleteTask", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskID", task?.GUID);
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            if (rowsAffected < 0)
            {
                throw new CustomException("Something went wrong");
            }
            else
            {
                return task;
            }
        }

        public void DeleteCompletedTasks(string userID)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Guid.TryParse(userID, out Guid guid);
                SqlCommand cmd = new("DeleteCompletedTasks", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userID", guid);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            if (rowsAffected == 0)
            {
                throw new CustomException("Something went wrong");
            }
        }
    }
}
