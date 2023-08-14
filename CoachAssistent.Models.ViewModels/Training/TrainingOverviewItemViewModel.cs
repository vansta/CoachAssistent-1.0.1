using CoachAssistent.Models.ViewModels.Segment;
using CoachAssistent.Models.ViewModels.Shareable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Training
{
    public class TrainingOverviewItemViewModel : BaseShareableViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<SegmentOverviewItemViewModel> Segments { get; set; }

        public TrainingOverviewItemViewModel()
        {
            Segments = new List<SegmentOverviewItemViewModel>();
        }
    }
}
