using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Member
    {
        public Guid Id { get; set; }

        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public Group? Group { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}
