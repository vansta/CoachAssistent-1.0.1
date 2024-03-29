﻿using CoachAssistent.Models.ViewModels.Segment;
using CoachAssistent.Models.ViewModels.Shareable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Training
{
    public class TrainingViewModel : BaseShareableViewModel
    {
        //public TrainingViewModel()
        //{
        //    Segments = new HashSet<SegmentOverviewItemViewModel>();
        //}
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Guid> Segments { get; set; } = new HashSet<Guid>();
        public ICollection<Guid> Editors { get; set; } = new HashSet<Guid>();
        public ICollection<string> Tags { get; set; } = new HashSet<string>();
    }
}
