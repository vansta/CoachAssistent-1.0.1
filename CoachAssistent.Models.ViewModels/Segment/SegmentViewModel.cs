using CoachAssistent.Models.ViewModels.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Segment
{
    public class SegmentViewModel
    {
        public SegmentViewModel()
        {
            Name = string.Empty;
            Exercises = new HashSet<ExerciseOverviewItemViewModel>();
            Editors = new HashSet<Guid>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? SharingLevel { get; set; }
        public ICollection<ExerciseOverviewItemViewModel> Exercises { get; set; }
        public ICollection<Guid> Editors { get; set; }
    }
}
