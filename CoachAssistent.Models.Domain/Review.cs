using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public string? Comment { get; set; }
        public double Score { get; set; }
        public DateTime DateTime { get; set; }
        public Guid ShareableId { get; set; }
        public Guid UserId { get; set; }

        public Shareable? Shareable { get; set; }
        public User? User { get; set; }
    }
}
