using System.ComponentModel.DataAnnotations;

namespace CoachAssistent.Models.Domain
{
    public class User
    {
        public User()
        {
            UserName = string.Empty;
            Email = string.Empty;
            Salt = new byte[32];
            Key = new byte[32];

            Memberships = new HashSet<Member>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string UserName { get; set; }
        [MaxLength(64)]
        public string? Email { get; set; }
        [MaxLength(128)]
        public string? FirstName { get; set; }
        [MaxLength(128)]
        public string? LastName { get; set; }

        public byte[] Salt { get; set; }
        public byte[] Key { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdate { get; set; }

        public Guid LicenseId { get; set; }
        public License? License { get; set; }
        public ICollection<Member> Memberships { get; set; }
    }
}