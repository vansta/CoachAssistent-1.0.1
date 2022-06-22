using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Exercise
    {
        public Exercise()
        {
            Name = string.Empty;
            Attachments = new HashSet<Attachment>();
            Tags = new HashSet<Tag>();
            Segments = new HashSet<Segment>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string? Description { get; set; }

        public bool Public { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Segment> Segments { get; set; }
    }
}
