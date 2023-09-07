using AutoMapper;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    internal class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<Training, TrainingOverviewItemViewModel>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.SharingLevel, opt => opt.MapFrom(src => (int)src.Shareable!.SharingLevel))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (int)src.Shareable!.Level))
                .ForMember(dest => dest.GroupIds, opt => opt.MapFrom(src => src.Shareable!.ShareablesXGroups.Select(sg => sg.GroupId)))
                .ForMember(dest => dest.EditorIds, opt => opt.MapFrom(src => src.Shareable!.Editors.Select(sg => sg.UserId)))
                .ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => src.Shareable!.Favorites.Count > 0));

            CreateMap<Training, TrainingViewModel>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.Segments, opt => opt.MapFrom(src => src.TrainingsXSegments.Select(ts => ts.SegmentId)))
                .ForMember(dest => dest.SharingLevel, opt => opt.MapFrom(src => (int)src.Shareable!.SharingLevel))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (int)src.Shareable!.Level))
                .ForMember(dest => dest.GroupIds, opt => opt.MapFrom(src => src.Shareable!.ShareablesXGroups.Select(sg => sg.GroupId)))
                .ForMember(dest => dest.EditorIds, opt => opt.MapFrom(src => src.Shareable!.Editors.Select(sg => sg.UserId)));
        }
    }
}
