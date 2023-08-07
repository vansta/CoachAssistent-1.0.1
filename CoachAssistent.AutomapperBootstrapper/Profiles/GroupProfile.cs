using AutoMapper;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupOverviewItemViewModel>()
                .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.Tags.Select(t => t.Name).Distinct()));

            CreateMap<Group, EditGroupViewModel>()
                .ForMember(dest => dest.Tags, opts => opts.MapFrom(src => src.Tags.Select(t => t.Name).Distinct()));
        }
    }
}
