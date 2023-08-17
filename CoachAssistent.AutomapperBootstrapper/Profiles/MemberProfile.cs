using AutoMapper;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<Member, CreateMemberViewModel>();

            CreateMap<MembershipRequest, MembershipRequestViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User!.UserName));

            CreateMap<Member, MembershipOverviewItemViewModel>()
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group!.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role!.Name));
        }
    }
}
