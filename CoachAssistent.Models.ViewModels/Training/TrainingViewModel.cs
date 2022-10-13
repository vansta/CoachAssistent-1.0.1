using CoachAssistent.Models.ViewModels.Segment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Training
{
    public class TrainingViewModel
    {
        public TrainingViewModel()
        {
            Segments = new HashSet<SegmentOverviewItemViewModel>();
        }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<SegmentOverviewItemViewModel> Segments { get; set; }
    }
}
