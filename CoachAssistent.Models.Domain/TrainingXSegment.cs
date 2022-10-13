using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class TrainingXSegment
    {
        public Guid TrainingId { get; set; }
        public Guid SegmentId { get; set; }

        public int Index { get; set; }

        public Training? Training { get; set; }
        public Segment? Segment { get; set; }
    }
}
