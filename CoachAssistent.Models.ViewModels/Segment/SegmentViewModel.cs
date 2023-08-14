using CoachAssistent.Common.Enums;
using CoachAssistent.Models.ViewModels.Exercise;
using CoachAssistent.Models.ViewModels.Shareable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Segment
{
    public class SegmentViewModel : CreateSegmentViewModel
    {
        public Guid Id { get; set; }
    }
}
