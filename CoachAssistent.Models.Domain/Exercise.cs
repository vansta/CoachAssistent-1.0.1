using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Exercise : ISharable
    {
        public Exercise()
        {
            Name = string.Empty;
            Attachments = new HashSet<Attachment>();
            Tags = new HashSet<Tag>();
            Segments = new HashSet<Segment>();
            SharablesXGroups = new HashSet<SharablesXGroups>();
            SegmentsXExercises = new HashSet<SegmentXExercise>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string? Description { get; set; }

        public SharingLevel Shared { get; set; }
        public DateTime VersionTS { get; set; }
        public Guid? OriginalId { get; set; }
        public DateTime? OriginalVersionTS { get; set; }

        public DateTime? DeletedTS { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Exercise? Original { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Segment> Segments { get; set; }
        public ICollection<SharablesXGroups> SharablesXGroups { get; set; }
        public ICollection<SegmentXExercise> SegmentsXExercises { get; set; }
    }
}
