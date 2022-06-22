using AutoMapper;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Segment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    internal class SegmentProfile : Profile
    {
        public SegmentProfile()
        {
            CreateMap<Segment, SegmentOverviewItemViewModel>()
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises.Select(e => e.Name)));

            CreateMap<Segment, SegmentViewModel>();
        }
    }
}
