using ToDo.Models;

namespace ToDo.Repository
{
    public interface IUserRepo
    {
        User AddUser(string name, string username, string password);
        User GetUser(string username, string password);
    }
}
