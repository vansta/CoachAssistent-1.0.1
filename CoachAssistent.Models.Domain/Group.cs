using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Group
    {
        public Group()
        {
            Name = string.Empty;
            Users = new HashSet<User>();
            Tags = new HashSet<Tag>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
