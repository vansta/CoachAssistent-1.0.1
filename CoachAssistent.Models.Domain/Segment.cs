using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Segment
    {
        public Segment()
        {
            Name = string.Empty;
            Exercises = new HashSet<Exercise>();
            Tags = new HashSet<Tag>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string? Description { get; set; }

        public bool Public { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
