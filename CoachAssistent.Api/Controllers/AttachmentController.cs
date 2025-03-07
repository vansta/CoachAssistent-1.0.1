using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration) : ControllerBase
    {
        readonly AttachmentManager attachmentManager = new(context, mapper, configuration);

        [HttpGet]
        public async Task<FileContentResult> GetAttachment(Guid id)
        {
            return File(await attachmentManager.GetAttachment(id), "image/png");
        }
    }
}
