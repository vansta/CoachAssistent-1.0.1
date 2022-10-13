using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Training;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class TrainingManager : BaseAuthenticatedManager
    {
        public TrainingManager(CoachAssistentDbContext context, IMapper mapper, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, authenticationWrapper)
        {

        }
        public OverviewViewModel<TrainingOverviewItemViewModel> GetTrainings()
        {
            IQueryable<Training> trainings = dbContext.Trainings
                .Include(t => t.Segments)
                .ThenInclude(s => s.Exercises);
            trainings = FilterBySharingLevel(trainings);
            return new OverviewViewModel<TrainingOverviewItemViewModel>
            {
                Items = trainings
                    .Select(s => mapper.Map<TrainingOverviewItemViewModel>(s)),
                TotalItems = trainings.Count()
            };
        }

        public async Task<TrainingViewModel> GetTraining(Guid id)
        {
            Training? training = await dbContext.Trainings
                .Include(t => t.Segments)
                .ThenInclude(s => s.Exercises)
                .ThenInclude(e => e.Attachments)
                .SingleAsync(s => s.Id.Equals(id));
            return mapper.Map<TrainingViewModel>(training);
        }

        public async Task<Guid> Create(TrainingViewModel viewModel)
        {
            //IQueryable<Exercise> exercises = dbContext
            //    .Exercises.Where(e => viewModel.Exercises.Select(x => x.Id).Contains(e.Id));
            Training training = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                //Exercises = exercises.ToHashSet(),
                UserId = authenticationWrapper.UserId
            };
            training = (await dbContext.Trainings.AddAsync(training)).Entity;
            await dbContext.SaveChangesAsync();

            return training.Id;
        }

        public async Task Update(TrainingViewModel viewModel)
        {
            Training training = await dbContext.Trainings
                .Include(t => t.Segments)
                .ThenInclude(s => s.Exercises)
                .SingleAsync(s => s.Id.Equals(viewModel.Id));

            training.Name = viewModel.Name;
            training.Description = viewModel.Description;

            training.Segments = dbContext
                .Segments.Where(s => viewModel.Segments.Select(x => x.Id).Contains(s.Id)).ToHashSet();

            //training.Exercises = dbContext
            //    .Exercises.Where(e => viewModel.Exercises.Select(x => x.Id).Contains(e.Id)).ToHashSet();

            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Training? training = await dbContext.Trainings.FindAsync(id);
            if (training is not null)
            {
                //dbContext.Trainings.Remove(training);
                training.DeletedTS = DateTime.Now;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
