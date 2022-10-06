namespace ToDo.Models
{
    public class CustomException : Exception
    {
        public CustomException(string err) : base(err) { }
    }
}
