using AutoMapper;
using Azure.Core;
using CoachAssistent.Common.Enums;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Training;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class TrainingManager : BaseAuthenticatedManager
    {
        public TrainingManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, configuration, authenticationWrapper)
        {

        }
        public OverviewViewModel<TrainingOverviewItemViewModel> GetTrainings(BaseSearchViewModel search)
        {
            IQueryable<Training> trainings = dbContext.Trainings
                .Include(t => t.Shareable!.ShareablesXGroups)
                .Include(t => t.Shareable!.Editors)
                .Include(e => e.Shareable!.Favorites.Where(f => f.UserId == authenticationWrapper.UserId))
                .Include(t => t.Tags)
                .Include(t => t.TrainingsXSegments.OrderBy(ts => ts.Index))
                    .ThenInclude(ts => ts.Segment)
                    .ThenInclude(s => s!.SegmentsXExercises.OrderBy(se => se.Index))
                    .ThenInclude(se => se.Exercise);

            trainings = FilterShareables(trainings, search);
            trainings = FilterBySharingLevel(trainings);
            return new OverviewViewModel<TrainingOverviewItemViewModel>
            {
                Items = trainings
                    .Select(s => mapper.Map<TrainingOverviewItemViewModel>(s)),
                TotalCount = trainings.Count()
            };
        }

        public async Task<TrainingViewModel> GetTraining(Guid id)
        {
            Training? training = await dbContext.Trainings
                .Include(t => t.Shareable!.ShareablesXGroups)
                .Include(t => t.Shareable!.Editors)
                .Include(t => t.Tags!)
                .Include(t => t.TrainingsXSegments.OrderBy(ts => ts.Index))
                    .ThenInclude(ts => ts.Segment)
                .SingleAsync(s => s.Id.Equals(id));
            return mapper.Map<TrainingViewModel>(training);
        }

        public async Task<Guid> Create(TrainingViewModel viewModel)
        {
            Training training = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Tags = CondenseTags(viewModel.Tags),
                Shareable = new Shareable
                {
                    SharingLevel = (SharingLevel)int.Parse(viewModel.SharingLevel),
                    Editors = CondenseEditors(viewModel.Editors),
                    HistoryLogs = new List<HistoryLog> { new HistoryLog(EditActionType.Create, authenticationWrapper.UserId) },
                    ShareablesXGroups = CondenseGroups(viewModel.GroupIds)
                },
                Segments = dbContext
                    .Segments.Where(s => viewModel.Segments.Contains(s.Id)).ToHashSet()
            };
            training = (await dbContext.Trainings.AddAsync(training)).Entity;
            await dbContext.SaveChangesAsync();

            return training.Id;
        }

        public async Task Update(TrainingViewModel viewModel)
        {
            Training training = await dbContext.Trainings
                .Include(t => t.Shareable!.Editors)
                .Include(t => t.Shareable!.ShareablesXGroups)
                .Include(t => t.Tags)
                .Include(t => t.Segments)
                    .ThenInclude(s => s.Exercises)
                .SingleAsync(s => s.Id.Equals(viewModel.Id));

            training.Name = viewModel.Name;
            training.Description = viewModel.Description;
            training.Tags = CondenseTags(viewModel.Tags);

            training.Shareable!.SharingLevel = (SharingLevel)int.Parse(viewModel.SharingLevel);
            training.Shareable.Editors = CondenseEditors(viewModel.Editors, training.Shareable);
            training.Shareable.ShareablesXGroups = CondenseGroups(viewModel.GroupIds, training.Shareable);

            int segmentIndex = 0;
            training.TrainingsXSegments = viewModel.Segments
                .Select(t =>
                {
                    TrainingXSegment? trainingXSegment = training.TrainingsXSegments.FirstOrDefault(ts => ts.SegmentId.Equals(t));
                    if (trainingXSegment is null)
                    {
                        return new TrainingXSegment
                        {
                            SegmentId = t,
                            Index = segmentIndex
                        };
                    }
                    else
                    {
                        trainingXSegment.Index = segmentIndex;
                    }
                    segmentIndex++;
                    return trainingXSegment;
                }).ToList();
            //training.Segments = dbContext
            //    .Segments.Where(s => viewModel.Segments.Select(x => x.Id).Contains(s.Id)).ToHashSet();

            await AddHistoryLog(training.ShareableId, EditActionType.Edit);

            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Training? training = await dbContext.Trainings.FindAsync(id);
            if (training is not null)
            {
                await AddHistoryLog(training.ShareableId, EditActionType.Edit);
                training.DeletedTS = DateTime.Now;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
