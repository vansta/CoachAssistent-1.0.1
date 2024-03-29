﻿using CoachAssistent.Models.ViewModels.Shareable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Segment
{
    public class SegmentOverviewItemViewModel : BaseShareableViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<string>? Exercises { get; set; }
        public ICollection<string>? Tags { get; set; }
    }
}
