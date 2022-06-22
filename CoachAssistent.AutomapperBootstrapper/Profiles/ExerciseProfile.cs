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
                .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments.Select(a => $"Attachment?id={a.Id}")));
        }
    }
}
