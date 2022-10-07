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
                try
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
                }
                catch (SqlException)
                {

                }
            };
            return user;
        }

        public User GetUserByID(string userID)
        {
            User user = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
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
                }
                catch (SqlException)
                {

                }
            };
            return user;
        }

        public User AddUser(User user)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("AddUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Guid", user.GUID);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Username", user.Username);
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            if (rowsAffected > 0)
            {
                return user;
            }
            else
            {
                throw new UserAllreadyExistsException();
            }
        }

        public void DeleteUser(Guid guid)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new("DeleteUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Guid", guid);
                rowsAffected = cmd.ExecuteNonQuery();
            }
            if (rowsAffected == 0)
            {
                throw new CustomException("Could not delete the user");
            }

        }

        public User UpdateUser(User user)
        {
            int rowsAffected = 0;
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
            if (rowsAffected > 0)
            {
                return user;
            }
            else
            {
                throw new UserAllreadyExistsException();
            }
        }
    }
}
