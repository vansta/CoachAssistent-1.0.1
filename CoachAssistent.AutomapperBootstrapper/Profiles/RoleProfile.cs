using AutoMapper;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.Domain.Permissions;
using CoachAssistent.Models.ViewModels.Permission;
using CoachAssistent.Models.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleOverviewItemViewModel>();

            CreateMap<RolePermission, RolePermissionOverviewItemViewModel>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields.Select(f => f.PermissionField!.Name)));
        }
    }
}
