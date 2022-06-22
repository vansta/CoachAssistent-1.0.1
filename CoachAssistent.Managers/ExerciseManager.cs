using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Exercise;
using Microsoft.Extensions.Configuration;

namespace CoachAssistent.Managers
{
    public class ExerciseManager : BaseManager
    {
        readonly IConfiguration configuration;
        public ExerciseManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration) 
            : base(context, mapper)
        {
            this.configuration = configuration;
        }
        public OverviewViewModel<ExerciseOverviewItemViewModel> GetExercises()
        {
            IQueryable<Exercise> exercises = dbContext.Exercises;

            return new OverviewViewModel<ExerciseOverviewItemViewModel>
            {
                Items = exercises.Select(e => mapper.Map<ExerciseOverviewItemViewModel>(e)),
                TotalItems = exercises.Count()
            };
        }

        public async Task<ExerciseOverviewItemViewModel> GetExercise(Guid id)
        {
            Exercise? exercise = await dbContext.Exercises.FindAsync(id);
            return mapper.Map<ExerciseOverviewItemViewModel>(exercise);
        }

        public async Task Create(CreateExerciseViewModel viewModel)
        {
            Exercise exercise = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description
            };

            exercise = (await dbContext.Exercises.AddAsync(exercise)).Entity;

            string basePath = Path.Combine(configuration["AttachmentFolder"], exercise.Id.ToString());
            Directory.CreateDirectory(basePath);
            foreach (var file in viewModel.Attachments)
            {
                string fullPath = Path.Combine(basePath, file.FileName);
                using FileStream fileStream = File.Create(fullPath);// new(fullPath, FileMode.Create);

                exercise.Attachments.Add(new Attachment
                {
                    FilePath = file.FileName,
                    Name = file.Name
                });
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            Exercise? exercise = await dbContext.Exercises.FindAsync(id);

            if (exercise is not null)
            {
                dbContext.Exercises.Remove(exercise);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}