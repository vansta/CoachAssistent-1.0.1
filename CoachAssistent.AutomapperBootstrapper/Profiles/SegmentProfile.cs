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
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises.Select(e => e.Name)))
                .ForMember(dest => dest.GroupIds, opt => opt.MapFrom(src => src.Shareable!.ShareablesXGroups.Select(sg => sg.GroupId)));

            CreateMap<Segment, SegmentViewModel>()
                .ForMember(dest => dest.SharingLevel, opt => opt.MapFrom(src => src.Shareable!.SharingLevel))
                .ForMember(dest => dest.GroupIds, opt => opt.MapFrom(src => src.Shareable!.ShareablesXGroups.Select(sg => sg.GroupId)));
        }
    }
}
