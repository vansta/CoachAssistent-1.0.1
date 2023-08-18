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
    public class CreateSegmentViewModel : BaseShareableViewModel
    {
        public CreateSegmentViewModel()
        {
            Name = string.Empty;
            Exercises = new HashSet<ExerciseOverviewItemViewModel>();
            Editors = new HashSet<Guid>();
        }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<ExerciseOverviewItemViewModel> Exercises { get; set; }
        public ICollection<Guid> Editors { get; set; }
        public ICollection<string> Tags { get; set; } = new List<string>();
    }
}
