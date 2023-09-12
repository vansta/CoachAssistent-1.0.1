using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class PasswordResetRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime RequestDateTime { get; set; }
        public DateTime? ResetDateTime { get; set; }

        public User? User { get; set; }
    }
}
