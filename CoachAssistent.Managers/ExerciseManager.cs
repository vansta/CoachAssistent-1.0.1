using AutoMapper;
using CoachAssistent.Common.Enums;
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
            : base(context, mapper, configuration, authenticationWrapper)
        {
            this.configuration = configuration;
        }
        public OverviewViewModel<ExerciseOverviewItemViewModel> GetExercises(BaseSearchViewModel search)
        {
            IQueryable<Exercise> exercises = dbContext.Exercises
                .Include(e => e.Attachments)
                .Include(e => e.Tags)
                .Include(e => e.Shareable!.ShareablesXGroups)
                .Include(e => e.Shareable!.Editors)
                .Include(e => e.Shareable!.Favorites.Where(f => f.UserId == authenticationWrapper.UserId));

            exercises = FilterShareables(exercises, search);
            exercises = FilterBySharingLevel(exercises);

            int totalCount = exercises.Count();
            return new OverviewViewModel<ExerciseOverviewItemViewModel>
            {
                Items = PaginateShareables(exercises, search)
                    .Select(e => mapper.Map<ExerciseOverviewItemViewModel>(e)),
                TotalCount = totalCount
            };
        }

        public async Task<ExerciseOverviewItemViewModel> GetExercise(Guid id)
        {
            Exercise? exercise = await dbContext.Exercises
                .Include(e => e.Attachments)
                .Include(e => e.Tags)
                .Include(e => e.Shareable!.ShareablesXGroups)
                .Include(e => e.Shareable!.Editors)
                .SingleAsync(e => e.Id == id);
            return mapper.Map<ExerciseOverviewItemViewModel>(exercise);
        }

        public async Task<Guid> Create(CreateExerciseViewModel viewModel)
        {
            Exercise exercise = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Tags = CondenseTags(viewModel.Tags),
                Shareable = new Shareable
                {
                    SharingLevel = viewModel.SharingLevel,
                    Editors = CondenseEditors(viewModel.Editors),
                    HistoryLogs = new List<HistoryLog> { new HistoryLog(EditActionType.Create, authenticationWrapper.UserId) },
                    ShareablesXGroups = CondenseGroups(viewModel.GroupIds)
                }
            };

            return await Create(exercise, viewModel.AddedAttachments);
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

        public async Task<Guid> Copy(Guid exerciseId)
        {
            Exercise? exercise = await dbContext.Exercises
                .Include(e => e.Attachments)
                .Include(e => e.Shareable!.Editors)
                .SingleAsync(e => e.Id.Equals(exerciseId));

            Exercise copy = new()
            {
                Name = exercise.Name,
                Description = exercise.Description,
                Tags = exercise.Tags,
                Shareable = new Shareable
                {
                    SharingLevel = SharingLevel.Public,
                    Editors = CondenseEditors(null),
                    HistoryLogs = new List<HistoryLog> { new HistoryLog(EditActionType.Copy, authenticationWrapper.UserId, exercise.ShareableId) }
                },
                Attachments = exercise.Attachments.Select(a => new Attachment
                {
                    Name = a.Name,
                    FilePath = a.FilePath
                }).ToList()
            };
            copy = (await dbContext.Exercises.AddAsync(copy)).Entity;

            foreach (var originalAttachment in exercise.Attachments)
            {
                //Attachment? originalAttachment = await dbContext.Attachments.SingleAsync(a => a.Id == attachmentId);
                string fromBasePath = Path.Combine(configuration["AttachmentFolder"] ?? string.Empty, exercise.Id.ToString());
                string toBasePath = Path.Combine(configuration["AttachmentFolder"] ?? string.Empty, copy.Id.ToString());
                AttachmentManager.CopyAttachment(fromBasePath, toBasePath, originalAttachment);
            }
            await dbContext.SaveChangesAsync();
            return copy.Id;
        }

        public async Task<Guid> Update(UpdateExerciseViewModel viewModel)
        {
            Exercise? exercise = await dbContext.Exercises
                .Include(e => e.Attachments)
                .Include(e => e.Tags)
                .Include(e => e.Shareable!.Editors)
                .Include(e => e.Shareable!.ShareablesXGroups)
                .SingleAsync(e => e.Id.Equals(viewModel.Id));

            exercise.Name = viewModel.Name;
            exercise.Description = viewModel.Description;
            exercise.Tags = CondenseTags(viewModel.Tags);

            exercise.Shareable!.SharingLevel = viewModel.SharingLevel;
            exercise.Shareable.Editors = CondenseEditors(viewModel.Editors, exercise.Shareable);
            exercise.Shareable.ShareablesXGroups = CondenseGroups(viewModel.GroupIds, exercise.Shareable);

            await AddHistoryLog(exercise.ShareableId, EditActionType.Edit);

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
                await AddHistoryLog(exercise.ShareableId, EditActionType.Delete);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}