using AutoMapper;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationOverviewItemViewModel>()
                .ForMember(dest => dest.FromUser, opt => opt.MapFrom(src => src.FromUser!.UserName))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group == null ? null : src.Group.Name));
        }
    }
}
