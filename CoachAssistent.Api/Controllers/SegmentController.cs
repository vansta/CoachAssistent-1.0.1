using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Segment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegmentController : ControllerBase
    {
        readonly SegmentManager segmentManager;
        public SegmentController(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            IAuthenticationWrapper authenticationWrapper = new AuthenticationWrapper(contextAccessor);
            segmentManager = new SegmentManager(context, mapper, configuration, authenticationWrapper);
        }
        [HttpGet]
        public Task<SegmentViewModel> GetSegment(Guid id)
        {
            return segmentManager.GetSegment(id);
        }

        [HttpGet("Overview")]
        public OverviewViewModel<SegmentOverviewItemViewModel> GetSegments()
        {
            return segmentManager.GetSegments();
        }

        [HttpPost]
        public Task<Guid> Create(CreateSegmentViewModel segment)
        {
            return segmentManager.Create(segment);
        }

        [HttpPut]
        public async Task<IActionResult> Update(SegmentViewModel segment)
        {
            await segmentManager.Update(segment);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await segmentManager.Delete(id);
            return NoContent();
        }
    }
}
