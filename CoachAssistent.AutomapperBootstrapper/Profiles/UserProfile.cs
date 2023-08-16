using AutoMapper;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoggedInUserViewModel>()
                .ForMember(dest => dest.LicenseLevel, opts => opts.MapFrom(src => src.License!.Level))
                .ForMember(dest => dest.GroupIds, opts => opts.MapFrom(src => src.Memberships.Select(m => m.GroupId).Distinct()));

            CreateMap<User, ProfileViewModel>()
                .ForMember(dest => dest.Memberships, opts => opts.MapFrom(src => src.Memberships.Select(m => m.GroupId).Distinct()));
        }
    }
}
