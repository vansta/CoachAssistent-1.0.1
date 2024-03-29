﻿using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Exercise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        readonly ExerciseManager exerciseManager;
        readonly IAuthenticationWrapper authenticationWrapper;
        public ExerciseController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            authenticationWrapper = new AuthenticationWrapper(httpContextAccessor);
            exerciseManager = new ExerciseManager(dbContext, mapper, configuration, authenticationWrapper);
        }

        [HttpGet]
        public Task<ExerciseOverviewItemViewModel> GetExercise(Guid id)
        {
            return exerciseManager.GetExercise(id);
        }

        [HttpGet("Overview")]
        public OverviewViewModel<ExerciseOverviewItemViewModel> GetExercises([FromQuery]BaseSearchViewModel request)
        {
            return exerciseManager.GetExercises(request);
        }
        [Authorize]
        [HttpPost]
        public Task<Guid> Create([FromForm]CreateExerciseViewModel exercise)
        {
            return exerciseManager.Create(exercise);
        }
        [Authorize]
        [HttpPut]
        public Task<Guid> Update([FromForm] UpdateExerciseViewModel exercise)
        {
            return exerciseManager.Update(exercise);
        }
        [Authorize]
        [HttpPost("Copy")]
        public Task<Guid> Copy([FromBody] Guid exerciseId)
        {
            return exerciseManager.Copy(exerciseId);
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await exerciseManager.Delete(id);
            return NoContent();
        }
    }
}
