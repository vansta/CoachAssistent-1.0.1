namespace CoachAssistent.Models.Domain
{
    public class User
    {
        public User()
        {
            UserName = string.Empty;
            Email = string.Empty;
            Groups = new HashSet<Group>();
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<Group> Groups { get; set; }
    }
}