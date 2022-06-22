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
        public async Task<IActionResult> Create([FromForm]CreateExerciseViewModel exercise)
        {
            await exerciseManager.Create(exercise);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await exerciseManager.Delete(id);
            return NoContent();
        }
    }
}
