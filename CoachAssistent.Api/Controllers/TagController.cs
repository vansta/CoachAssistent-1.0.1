using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
using CoachAssistent.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        readonly TagManager tagManager;
        public TagController(CoachAssistentDbContext context, IMapper mapper)
        {
            tagManager = new TagManager(context, mapper);
        }
        [HttpGet]
        public IEnumerable<string> GetTags(string? search)
        {
            return tagManager.GetTags(search);
        }
    }
}
