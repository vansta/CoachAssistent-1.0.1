﻿using AutoMapper;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Segment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.AutomapperBootstrapper.Profiles
{
    internal class SegmentProfile : Profile
    {
        public SegmentProfile()
        {
            CreateMap<Segment, SegmentOverviewItemViewModel>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.SegmentsXExercises.OrderBy(se => se.Index).Select(se => se.Exercise!.Name)))
                .ForMember(dest => dest.SharingLevel, opt => opt.MapFrom(src => (int)src.Shareable!.SharingLevel))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (int)src.Shareable!.Level))
                .ForMember(dest => dest.GroupIds, opt => opt.MapFrom(src => src.Shareable!.ShareablesXGroups.Select(sg => sg.GroupId)))
                .ForMember(dest => dest.EditorIds, opt => opt.MapFrom(src => src.Shareable!.Editors.Select(sg => sg.UserId)))
                .ForMember(dest => dest.IsFavorite, opt => opt.MapFrom(src => src.Shareable!.Favorites.Count > 0));

            CreateMap<Segment, SegmentViewModel>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name)))
                .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.SegmentsXExercises.OrderBy(se => se.Index).Select(se => se.Exercise)))
                .ForMember(dest => dest.SharingLevel, opt => opt.MapFrom(src => (int)src.Shareable!.SharingLevel))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => (int)src.Shareable!.Level))
                .ForMember(dest => dest.GroupIds, opt => opt.MapFrom(src => src.Shareable!.ShareablesXGroups.Select(sg => sg.GroupId)))
                .ForMember(dest => dest.EditorIds, opt => opt.MapFrom(src => src.Shareable!.Editors.Select(sg => sg.UserId)));
        }
    }
}
