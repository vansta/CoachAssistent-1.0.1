using AutoMapper;
using CoachAssistent.Common.Enums;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Segment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace CoachAssistent.Managers
{
    public class SegmentManager : BaseAuthenticatedManager
    {
        public SegmentManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, configuration, authenticationWrapper)
        {

        }
        public OverviewViewModel<SegmentOverviewItemViewModel> GetSegments(BaseSearchViewModel search)
        {
            IQueryable<Segment> segments = dbContext.Segments
                .Include(s => s.SegmentsXExercises.OrderBy(se => se.Index))
                    .ThenInclude(se => se.Exercise)
                .Include(s => s.Shareable!.ShareablesXGroups)
                .Include(s => s.Shareable!.Editors);
            if (search is not null)
            {
                if (!string.IsNullOrEmpty(search.Search))
                {
                    segments = segments
                        .Where(e => e.Name.Contains(search.Search)
                            || (!string.IsNullOrEmpty(e.Description) && e.Description.Contains(search.Search)));
                }
                if (search.Tags is not null && search.Tags.Any())
                {
                    segments = segments
                        .Where(e => e.Tags.Select(t => t.Name).Any(t => search.Tags.Contains(t)));
                }
            }

            segments = FilterBySharingLevel(segments);
            return new OverviewViewModel<SegmentOverviewItemViewModel>
            {
                Items = segments
                    .Select(s => mapper.Map<SegmentOverviewItemViewModel>(s)),
                TotalCount = segments.Count()
            };
        }

        public async Task<SegmentViewModel> GetSegment(Guid id)
        {
            Segment? segment = await dbContext.Segments
                .Include(s => s.Exercises)
                    .ThenInclude(e => e.Attachments)
                .Include(s => s.SegmentsXExercises.OrderBy(se => se.Index))
                    .ThenInclude(se => se.Exercise)
                    .ThenInclude(e => e!.Shareable)
                .Include(s => s.Shareable!.ShareablesXGroups)
                .Include(s => s.Shareable!.Editors)
                .SingleAsync(s => s.Id.Equals(id));
            return mapper.Map<SegmentViewModel>(segment);
        }

        public async Task<Guid> Create(CreateSegmentViewModel viewModel)
        {
            IQueryable<Exercise> exercises = dbContext
                .Exercises.Where(e => viewModel.Exercises.Select(x => x.Id).Contains(e.Id));
            Segment segment = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Exercises = exercises.ToHashSet(),
                Shareable = new Shareable
                {
                    SharingLevel = (SharingLevel)int.Parse(viewModel.SharingLevel),
                    Editors = CondenseEditors(viewModel.Editors),
                    HistoryLogs = new List<HistoryLog> { new HistoryLog(EditActionType.Create, authenticationWrapper.UserId) },
                    ShareablesXGroups = CondenseGroups(viewModel.GroupIds)
                }
            };
            segment = (await dbContext.Segments.AddAsync(segment)).Entity;
            await dbContext.SaveChangesAsync();

            return segment.Id;
        }

        public async Task Update(SegmentViewModel viewModel)
        {
            Segment segment = await dbContext.Segments
                //.Include(s => s.Exercises)
                .Include(s => s.SegmentsXExercises)
                .Include(s => s.Shareable!.Editors)
                .Include(s => s.Shareable!.ShareablesXGroups)
                .SingleAsync(s => s.Id.Equals(viewModel.Id));

            segment.Name = viewModel.Name;
            segment.Description = viewModel.Description;

            await AddHistoryLog(segment.ShareableId, EditActionType.Edit);

            int exerciseIndex = 0;
            segment.SegmentsXExercises = viewModel.Exercises
                .Select(e =>
                {
                    SegmentXExercise? segmentXExercise = segment.SegmentsXExercises.FirstOrDefault(se => se.ExerciseId.Equals(e.Id));
                    if (segmentXExercise is null)
                    {
                        return new SegmentXExercise
                        {
                            ExerciseId = e.Id,
                            Index = exerciseIndex
                        };
                    }
                    else
                    {
                        segmentXExercise.Index = exerciseIndex;
                    }
                    exerciseIndex++;
                    return segmentXExercise;
                }).ToList();
            //segment.Exercises = dbContext
            //    .Exercises.Where(e => viewModel.Exercises.Select(x => x.Id).Contains(e.Id)).ToHashSet();

            segment.Shareable!.Editors = CondenseEditors(viewModel.Editors, segment.Shareable);
            segment.Shareable!.SharingLevel = (SharingLevel)int.Parse(viewModel.SharingLevel);
            segment.Shareable!.ShareablesXGroups = CondenseGroups(viewModel.GroupIds);

            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Segment? segment = await dbContext.Segments.FindAsync(id);
            if (segment is not null)
            {
                await AddHistoryLog(segment.ShareableId, EditActionType.Delete);
                segment.DeletedTS = DateTime.Now;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}