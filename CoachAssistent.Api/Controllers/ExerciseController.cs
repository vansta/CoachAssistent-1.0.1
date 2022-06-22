using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Exercise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        readonly ExerciseManager exerciseManager;
        public ExerciseController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            exerciseManager = new ExerciseManager(dbContext, mapper, configuration);
        }

        [HttpGet]
        public Task<ExerciseOverviewItemViewModel> GetExercise(Guid id)
        {
            return exerciseManager.GetExercise(id);
        }

        [HttpGet("Overview")]
        public OverviewViewModel<ExerciseOverviewItemViewModel> GetExercises()
        {
            return exerciseManager.GetExercises();
        }

        [HttpPost]
        public Task<Guid> Create([FromForm]CreateExerciseViewModel exercise)
        {
            return exerciseManager.Create(exercise);
        }

        [HttpPut]
        public Task<Guid> Update([FromForm] UpdateExerciseViewModel exercise)
        {
            return exerciseManager.Update(exercise);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await exerciseManager.Delete(id);
            return NoContent();
        }
    }
}
