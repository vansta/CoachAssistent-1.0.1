﻿using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
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
        public SegmentController(CoachAssistentDbContext context, IMapper mapper)
        {
            segmentManager = new SegmentManager(context, mapper);
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
        public async Task<IActionResult> CreateSegment(SegmentViewModel segment)
        {
            await segmentManager.Create(segment);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSegment(Guid id)
        {
            await segmentManager.Delete(id);
            return NoContent();
        }
    }
}
