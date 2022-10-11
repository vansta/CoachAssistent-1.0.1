using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Segment : ISharable
    {
        public Segment()
        {
            Name = string.Empty;
            Exercises = new HashSet<Exercise>();
            Tags = new HashSet<Tag>();
            SharablesXGroups = new HashSet<SharablesXGroups>();
            SegmentsXExercises = new HashSet<SegmentXExercise>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string? Description { get; set; }

        public SharingLevel Shared { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<SharablesXGroups> SharablesXGroups { get; set; }
        public ICollection<SegmentXExercise> SegmentsXExercises { get; set; }
    }
}
