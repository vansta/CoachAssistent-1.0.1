using AutoMapper;
using CoachAssistent.Models.Domain.Permissions;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Permission;
using CoachAssistent.Models.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoachAssistent.Models.ViewModels.License;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    public class LicenseProfile : Profile
    {
        public LicenseProfile()
        {
            CreateMap<License, LicenseOverviewItemViewModel>();

            CreateMap<LicensePermission, RolePermissionOverviewItemViewModel>()
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields.Select(f => f.PermissionFieldId)));
        }
    }
}
