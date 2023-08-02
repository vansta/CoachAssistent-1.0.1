using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Exercise;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoachAssistent.Managers
{
    public class ExerciseManager : BaseAuthenticatedManager
    {
        readonly IConfiguration configuration;
        
        public ExerciseManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper) 
            : base(context, mapper, authenticationWrapper)
        {
            this.configuration = configuration;
        }
        public OverviewViewModel<ExerciseOverviewItemViewModel> GetExercises(BaseSearchViewModel search)
        {
            IQueryable<Exercise> exercises = dbContext.Exercises
                .Include(e => e.Attachments)
                .Include(e => e.Tags);

            if (search is not null)
            {
                if (!string.IsNullOrEmpty(search.Search))
                {
                    exercises = exercises
                        .Where(e => e.Name.Contains(search.Search)
                            || (!string.IsNullOrEmpty(e.Description) && e.Description.Contains(search.Search)));
                }
                if (search.TagIds is not null)
                {
                    exercises = exercises
                        .Where(e => e.Tags.Select(t => t.Id).Any(t => search.TagIds.Contains(t)));
                }
            }

            exercises = FilterBySharingLevel(exercises);

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

        public Task<Guid> Create(CreateExerciseViewModel viewModel)
        {
            Exercise exercise = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                SharingLevel = viewModel.SharingLevel,
                //UserId = authenticationWrapper.UserId,
                //Editors = viewModel.Editors.Select(e => new Editor { UserId = e }).ToHashSet(),// new List<Editor> { new Editor { UserId = authenticationWrapper.UserId } },
                VersionTS = DateTime.Now,
                Tags = CondenseTags(viewModel.Tags)
            };

            exercise.Editors = CondenseEditors(exercise, viewModel.Editors);

            return Create(exercise, viewModel.AddedAttachments);
        }

        public async Task<Guid> Create(Exercise exercise, ICollection<IFormFile> attachments)
        {
            exercise = (await dbContext.Exercises.AddAsync(exercise)).Entity;

            string basePath = Path.Combine(configuration["AttachmentFolder"] ?? string.Empty, exercise.Id.ToString());
            Directory.CreateDirectory(basePath);
            foreach (var file in attachments)
            {
                exercise.Attachments.Add(AttachmentManager.CreateAttachment(basePath, file));
            }
            await dbContext.SaveChangesAsync();

            return exercise.Id;
        }

        public async Task<Guid> UpdateOrCopy(UpdateExerciseViewModel viewModel)
        {
            Exercise? exercise = await dbContext.Exercises
                .Include(e => e.Attachments)
                .Include(e => e.Editors)
                .SingleAsync(e => e.Id.Equals(viewModel.Id));

            if (exercise.Editors.Select(e => e.UserId).Contains(authenticationWrapper.UserId))
            {
                return await Update(viewModel);
            }
            else
            {
                return await Copy(exercise, viewModel);
            }
        }

        private async Task<Guid> Copy(Exercise exercise, UpdateExerciseViewModel viewModel)
        {
            Exercise copy = new()
            {
                //UserId = authenticationWrapper.UserId,
                //Editors = new List<Editor> { new Editor { UserId = authenticationWrapper.UserId } },
                Name = viewModel.Name,
                Description = viewModel.Description,
                SharingLevel = viewModel.SharingLevel,// Common.Enums.SharingLevel.Private,
                VersionTS = DateTime.Now,
                //OriginalId = exercise.Id,
                //OriginalVersionTS = exercise.OriginalVersionTS
            };

            copy.Editors = CondenseEditors(copy, viewModel.Editors);

            Guid newId = await Create(copy, viewModel.AddedAttachments);

            foreach (var attachmentId in viewModel.SelectedAttachments)
            {
                Attachment? originalAttachment = await dbContext.Attachments.SingleAsync(a => a.Id == attachmentId);
                string fromBasePath = Path.Combine(configuration["AttachmentFolder"] ?? string.Empty, exercise.Id.ToString());
                string toBasePath = Path.Combine(configuration["AttachmentFolder"] ?? string.Empty, newId.ToString());
                AttachmentManager.CopyAttachment(fromBasePath, toBasePath, originalAttachment);
            }

            return newId;
        }

        public async Task<Guid> Update(UpdateExerciseViewModel viewModel)
        {
            Exercise? exercise = await dbContext.Exercises
                .Include(e => e.Attachments)
                .Include(e => e.Tags)
                .Include(e => e.Editors)
                .SingleAsync(e => e.Id.Equals(viewModel.Id));

            exercise.Name = viewModel.Name;
            exercise.Description = viewModel.Description;
            exercise.VersionTS = DateTime.Now;
            exercise.SharingLevel = viewModel.SharingLevel;
            exercise.Tags = CondenseTags(viewModel.Tags);

            exercise.Editors = CondenseEditors(exercise, viewModel.Editors);

            string basePath = Path.Combine(configuration["AttachmentFolder"] ?? string.Empty, exercise.Id.ToString());
            Directory.CreateDirectory(basePath);

            var filesToRemove = exercise.Attachments.Where(a => !viewModel.SelectedAttachments.Any(x => x == a.Id));
            foreach (var attachment in filesToRemove)
            {
                AttachmentManager.RemoveAttachment(basePath, attachment.FilePath);
                exercise.Attachments.Remove(attachment);
            }

            foreach (var file in viewModel.AddedAttachments)
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
                exercise.DeletedTS = DateTime.Now;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}