using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Exercise : IShareable
    {
        public Exercise()
        {
            Name = string.Empty;
            Attachments = new HashSet<Attachment>();
            Tags = new HashSet<Tag>();
            Segments = new HashSet<Segment>();
            SegmentsXExercises = new HashSet<SegmentXExercise>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DeletedTS { get; set; }

        public Guid ShareableId { get; set; }
        public Shareable? Shareable { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Segment> Segments { get; set; }
        public ICollection<SegmentXExercise> SegmentsXExercises { get; set; }
    }
}
