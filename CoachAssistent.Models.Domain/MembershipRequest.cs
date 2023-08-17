using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class MembershipRequest
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public DateTime? ResponseTimestamp { get; set; }
        [MaxLength(512)]
        public string? Description { get; set; }

        public User? User { get; set; }
        public Group? Group { get; set; }
    }
}
