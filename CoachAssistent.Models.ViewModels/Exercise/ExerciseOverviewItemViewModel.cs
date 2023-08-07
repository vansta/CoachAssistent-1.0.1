using CoachAssistent.Common.Enums;
using CoachAssistent.Models.ViewModels.Shareable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Exercise
{
    public class ExerciseOverviewItemViewModel : BaseShareableViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? SharingLevel { get; set; }
        public Guid ShareableId { get; set; }
        public IEnumerable<Guid>? Attachments { get; set; }
        public IEnumerable<string> Tags { get; set; } = null!;
    }
}
