namespace ToDo.Models
{
    public record User
    {
        public Guid GUID { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public User() { }
        public User(Guid guid, string name, string username, string password)
        {
            GUID = guid;
            Name = name;
            Username = username;
            Password = password;
        }
    }
}
