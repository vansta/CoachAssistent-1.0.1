using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Exercise;
using Microsoft.EntityFrameworkCore;
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
            IQueryable<Exercise> exercises = dbContext.Exercises
                .Include(e => e.Attachments);

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

        public async Task<Guid> Create(CreateExerciseViewModel viewModel)
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
                exercise.Attachments.Add(AttachmentManager.CreateAttachment(basePath, file));
            }
            await dbContext.SaveChangesAsync();

            return exercise.Id;
        }

        public async Task<Guid> Update(UpdateExerciseViewModel viewModel)
        {
            Exercise? exercise = await dbContext.Exercises
                .Include(e => e.Attachments)
                .SingleAsync(e => e.Id.Equals(viewModel.Id));

            exercise.Name = viewModel.Name;
            exercise.Description = viewModel.Description;

            string basePath = Path.Combine(configuration["AttachmentFolder"], exercise.Id.ToString());
            Directory.CreateDirectory(basePath);

            var filesToRemove = exercise.Attachments.Where(a => !viewModel.Attachments.Any(x => x.FileName.Equals(a.FilePath)));
            foreach(var attachment in filesToRemove)
            {
                AttachmentManager.RemoveAttachment(basePath, attachment.FilePath);
                exercise.Attachments.Remove(attachment);
            }
            
            foreach (var file in viewModel.Attachments)
            {
                Attachment attachment = AttachmentManager.CreateAttachment(basePath, file);
                if (!exercise.Attachments.Any(x => x.FilePath.Equals(file.FileName)))
                {
                    exercise.Attachments.Add(attachment);
                }
            }
            await dbContext.SaveChangesAsync();

            return exercise.Id;
        }

        public async Task Delete(Guid id)
        {
            Exercise? exercise = await dbContext.Exercises
                .Include(e => e.Attachments)
                .SingleOrDefaultAsync(e => e.Id.Equals(id));

            if (exercise is not null)
            {
                string basePath = Path.Combine(configuration["AttachmentFolder"], exercise.Id.ToString());
                Directory.CreateDirectory(basePath);
                AttachmentManager.RemoveAttachmentsRange(basePath, exercise.Attachments.Select(a => a.Name));

                dbContext.Exercises.Remove(exercise);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}