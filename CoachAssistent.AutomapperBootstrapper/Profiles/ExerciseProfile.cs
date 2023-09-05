using AutoMapper;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<Exercise, ExerciseOverviewItemViewModel>()
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments.Select(a => a.Id)))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.GroupIds, opt => opt.MapFrom(src => src.Shareable!.ShareablesXGroups.Select(sg => sg.GroupId)))
                .ForMember(dest => dest.EditorIds, opt => opt.MapFrom(src => src.Shareable!.Editors.Select(sg => sg.UserId)))
                .ForMember(dest => dest.SharingLevel, opt => opt.MapFrom(src => ((int)src.Shareable!.SharingLevel).ToString()))
                .ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => src.Shareable!.Favorites.Count > 0));
        }
    }
}
