using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Training;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        readonly TrainingManager trainingManager;
        public TrainingController(CoachAssistentDbContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            IAuthenticationWrapper authenticationWrapper = new AuthenticationWrapper(contextAccessor);
            trainingManager = new TrainingManager(context, mapper, authenticationWrapper);
        }
        [HttpGet]
        public Task<TrainingViewModel> GetTraining(Guid id)
        {
            return trainingManager.GetTraining(id);
        }

        [HttpGet("Overview")]
        public OverviewViewModel<TrainingOverviewItemViewModel> GetTrainings()
        {
            return trainingManager.GetTrainings();
        }

        [HttpPost]
        public Task<Guid> Create(TrainingViewModel training)
        {
            return trainingManager.Create(training);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TrainingViewModel training)
        {
            await trainingManager.Update(training);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await trainingManager.Delete(id);
            return NoContent();
        }
    }
}
