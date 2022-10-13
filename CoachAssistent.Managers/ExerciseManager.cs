﻿using AutoMapper;
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
        public OverviewViewModel<ExerciseOverviewItemViewModel> GetExercises()
        {
            IQueryable<Exercise> exercises = dbContext.Exercises
                .Include(e => e.Attachments);

            exercises = FilterBySharingLevel(exercises);

            var test = exercises.ToList();

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
                Shared = Common.Enums.SharingLevel.Public,
                UserId = authenticationWrapper.UserId,
                VersionTS = DateTime.Now
            };

            return Create(exercise, viewModel.AddedAttachments);
        }

        public async Task<Guid> Create(Exercise exercise, ICollection<IFormFile> attachments)
        {
            exercise = (await dbContext.Exercises.AddAsync(exercise)).Entity;

            string basePath = Path.Combine(configuration["AttachmentFolder"], exercise.Id.ToString());
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
                .SingleAsync(e => e.Id.Equals(viewModel.Id));

            if (exercise.UserId == authenticationWrapper.UserId)
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
                UserId = authenticationWrapper.UserId,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Shared = Common.Enums.SharingLevel.Private,
                VersionTS = DateTime.Now,
                OriginalId = exercise.Id,
                OriginalVersionTS = exercise.OriginalVersionTS
            };

            Guid newId = await Create(copy, viewModel.AddedAttachments);

            foreach (var attachmentId in viewModel.SelectedAttachments)
            {
                Attachment? originalAttachment = await dbContext.Attachments.SingleAsync(a => a.Id == attachmentId);
                string fromBasePath = Path.Combine(configuration["AttachmentFolder"], exercise.Id.ToString());
                string toBasePath = Path.Combine(configuration["AttachmentFolder"], newId.ToString());
                AttachmentManager.CopyAttachment(fromBasePath, toBasePath, originalAttachment);
            }

            return newId;
        }

        public async Task<Guid> Update(UpdateExerciseViewModel viewModel)
        {
            Exercise? exercise = await dbContext.Exercises
                .Include(e => e.Attachments)
                .SingleAsync(e => e.Id.Equals(viewModel.Id));

            exercise.Name = viewModel.Name;
            exercise.Description = viewModel.Description;
            exercise.VersionTS = DateTime.Now;

            string basePath = Path.Combine(configuration["AttachmentFolder"], exercise.Id.ToString());
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
                //string basePath = Path.Combine(configuration["AttachmentFolder"], exercise.Id.ToString());
                //Directory.CreateDirectory(basePath);
                //AttachmentManager.RemoveAttachmentsRange(basePath, exercise.Attachments.Select(a => a.Name));

                //dbContext.Exercises.Remove(exercise);

                exercise.DeletedTS = DateTime.Now;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}