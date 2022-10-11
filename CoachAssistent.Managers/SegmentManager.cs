using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Segment;
using Microsoft.EntityFrameworkCore;

namespace CoachAssistent.Managers
{
    public class SegmentManager : BaseAuthenticatedManager
    {
        public SegmentManager(CoachAssistentDbContext context, IMapper mapper, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, authenticationWrapper)
        {

        }
        public OverviewViewModel<SegmentOverviewItemViewModel> GetSegments()
        {
            IQueryable<Segment> segments = dbContext.Segments
                .Include(s => s.Exercises);
            segments = FilterBySharingLevel(segments);
            return new OverviewViewModel<SegmentOverviewItemViewModel>
            {
                Items = segments
                    .Select(s => mapper.Map<SegmentOverviewItemViewModel>(s)),
                TotalItems = segments.Count()
            };
        }

        public async Task<SegmentViewModel> GetSegment(Guid id)
        {
            Segment? segemnt = await dbContext.Segments
                .Include(s => s.Exercises).ThenInclude(e => e.Attachments)
                .SingleAsync(s => s.Id.Equals(id));
            return mapper.Map<SegmentViewModel>(segemnt);
        }

        public async Task<Guid> Create(SegmentViewModel viewModel)
        {
            IQueryable<Exercise> exercises = dbContext
                .Exercises.Where(e => viewModel.Exercises.Select(x => x.Id).Contains(e.Id));
            Segment segment = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Exercises = exercises.ToHashSet()
            };
            segment = (await dbContext.Segments.AddAsync(segment)).Entity;
            await dbContext.SaveChangesAsync();

            return segment.Id;
        }

        public async Task Update(SegmentViewModel viewModel)
        {
            Segment segment = await dbContext.Segments
                .Include(s => s.Exercises)
                .SingleAsync(s => s.Id.Equals(viewModel.Id));

            segment.Name = viewModel.Name;
            segment.Description = viewModel.Description;

            segment.Exercises = dbContext
                .Exercises.Where(e => viewModel.Exercises.Select(x => x.Id).Contains(e.Id)).ToHashSet();

            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Segment? segment = await dbContext.Segments.FindAsync(id);
            if (segment is not null)
            {
                dbContext.Segments.Remove(segment);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}