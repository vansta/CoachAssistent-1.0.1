using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Managers;
using CoachAssistent.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoachAssistent.Models.ViewModels.Group;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        readonly GroupManager groupManager;
        readonly IAuthenticationWrapper authenticationWrapper;
        public GroupController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            authenticationWrapper = new AuthenticationWrapper(httpContextAccessor);
            groupManager = new GroupManager(dbContext, mapper, configuration, authenticationWrapper);
        }
        [HttpGet]
        public Task<IEnumerable<SelectViewModel>> GetGroupsForUser()
        {
            return groupManager.GetGroupsForUser();
        }

        [HttpPost]
        public Task<Guid> CreateGroup(CreateGroupViewModel createGroupViewModel)
        {
            return groupManager.CreateGroup(createGroupViewModel);
        }
    }
}
