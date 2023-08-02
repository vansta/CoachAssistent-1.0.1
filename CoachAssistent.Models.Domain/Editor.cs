using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Editor
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        //public Guid SharableId { get; set; }
        public Guid? ExerciseId { get; set; }
        public Guid? SegmentId { get; set; }
        public Guid? TrainingId { get; set; }

        public User? User { get; set; }

        public Exercise? Exercise { get; set; }
        public Segment? Segment { get; set; }
        public Training? Training { get; set; }
    }
}
