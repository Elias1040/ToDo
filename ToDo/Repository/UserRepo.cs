using ToDo.Models;
using ToDo.DAL;
using System.Threading.Tasks;

namespace ToDo.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly UserData _data;
        public UserRepo(IConfiguration config)
        {
            _data = new(config);
        }

        public User? GetUser(string username, string password)
        {
            User user = _data.GetUser(username);
            if (user != null && password == user.Password)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public void AddUser(string name, string username, string password)
        {
            _data.AddUser(new(Guid.NewGuid(), name, username, password));
        }

        public void UpdateUser(string guid, string name, string username, string password)
        {
            User user = _data.GetUserByID(guid);
            user.Name = string.IsNullOrWhiteSpace(name) ? user.Name : name;
            user.Username = string.IsNullOrWhiteSpace(username) ? user.Username : username;
            user.Password = string.IsNullOrWhiteSpace(username) ? user.Username : password;
            _data.UpdateUser(user);
        }
    }
}
