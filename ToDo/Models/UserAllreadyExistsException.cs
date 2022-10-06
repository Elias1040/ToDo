namespace ToDo.Models
{
    public class UserAllreadyExistsException : Exception
    {
        public UserAllreadyExistsException () : base("The username is not available")
        {
            
        }
    }
}
