using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class SegmentXExercise
    {
        public Guid SegmentId { get; set; }
        public Guid ExerciseId { get; set; }

        public Segment? Segment { get; set; }
        public Exercise? Exercise { get; set; }
    }
}
