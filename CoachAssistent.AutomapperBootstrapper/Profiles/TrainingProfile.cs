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
            CreateMap<Training, TrainingOverviewItemViewModel>();
                //.ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises.Select(e => e.Name)));

            CreateMap<Training, TrainingViewModel>();
        }
    }
}
