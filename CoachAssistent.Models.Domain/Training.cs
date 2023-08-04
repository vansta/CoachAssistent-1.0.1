using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Training : IShareable
    {
        public Training()
        {
            Name = string.Empty;
            Segments = new HashSet<Segment>();
            Tags = new HashSet<Tag>();
            TrainingsXSegments = new HashSet<TrainingXSegment>();
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        public string? Description { get; set; }

        

        public DateTime? DeletedTS { get; set; }

        public Guid ShareableId { get; set; }
        
        public Shareable? Shareable { get; set; }

        
        public ICollection<Segment> Segments { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<TrainingXSegment> TrainingsXSegments { get; set; }
    }
}
