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
    public class TagController(CoachAssistentDbContext context, IMapper mapper) : ControllerBase
    {
        readonly TagManager tagManager = new(context, mapper);

        [HttpGet]
        public IEnumerable<string> GetTags(string? search)
        {
            return tagManager.GetTags(search);
        }
    }
}
