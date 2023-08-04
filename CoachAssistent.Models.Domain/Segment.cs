using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Segment : IShareable
    {
        public Segment()
        {
            Name = string.Empty;
            Exercises = new HashSet<Exercise>();
            Trainings = new HashSet<Training>();
            Tags = new HashSet<Tag>();
            SegmentsXExercises = new HashSet<SegmentXExercise>();
            TrainingsXSegments = new HashSet<TrainingXSegment>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DeletedTS { get; set; }

        public Guid ShareableId { get; set; }
        public Shareable? Shareable { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
        public ICollection<Training> Trainings { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<SegmentXExercise> SegmentsXExercises { get; set; }
        public ICollection<TrainingXSegment> TrainingsXSegments { get; set; }
    }
}
