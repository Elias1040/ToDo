using System.Data;
using System.Data.SqlClient;
using ToDo.Models;

namespace ToDo.DAL
{
    public class UserData
    {
        private readonly string connectionString;
        public UserData(IConfiguration config)
        {
            connectionString = config.GetConnectionString("Default");
        }

        public User GetUser(string username)
        {
            User user = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Guid.TryParse(reader.GetString("UserID"), out Guid guid);
                    user = new User()
                    {
                        GUID = guid,
                        Name = reader.GetString("Name"),
                        Username = reader.GetString("Username"),
                        Password = reader.GetString("Password"),
                    };
                }
            };
            return user;
        }

        public User GetUserByID(string userID)
        {
            User user = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetUserByID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Guid", userID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Guid.TryParse(userID, out Guid guid);
                    user = new User()
                    {
                        GUID = guid,
                        Name = reader.GetString("Name"),
                        Username = reader.GetString("Username"),
                        Password = reader.GetString("Password"),
                    };
                }
            };
            return user;
        }

        public void AddUser(User user)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("AddUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Guid", user.GUID);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(Guid guid)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new("DeleteUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Guid", guid);
                cmd.ExecuteNonQuery();
            }
        }

        public User UpdateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new("UpdateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Guid", user.GUID);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.ExecuteNonQuery();
            }
            return user;
        }
    }
}
