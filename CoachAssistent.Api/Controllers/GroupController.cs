using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Managers;
using CoachAssistent.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoachAssistent.Models.ViewModels.Group;
using CoachAssistent.Models.ViewModels.Member;

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

        [HttpGet("Overview")]
        public OverviewViewModel<GroupOverviewItemViewModel> GetGroups()
        {
            return groupManager.GetGroups();
        }

        [HttpGet("Details")]
        public Task<EditGroupViewModel> GetGroup(Guid id)
        {
            return groupManager.GetGroup(id);
        }

        [HttpGet("Available")]
        public IEnumerable<SelectViewModel> GetAvailableGroups(string? search, string? action = null)
        {
            return groupManager.GetAvailableGroups(search, action);
        }

        [HttpPost]
        public Task<Guid> CreateGroup(CreateGroupViewModel createGroupViewModel)
        {
            return groupManager.CreateGroup(createGroupViewModel);
        }


        [HttpPost("RequestMembership")]
        public Task RequestMembership([FromBody]Guid groupId)
        {
            return groupManager.RequestMembership(groupId);
        }

        [HttpPut]
        public async Task UpdateGroup(EditGroupViewModel editGroupViewModel)
        {
            await groupManager.UpdateGroup(editGroupViewModel);
        }

        [HttpPut("Request")]
        public async Task RespondToMembershipRequest(MembershipRequestResponseViewModel response)
        {
            await groupManager.RespondToMembershipRequest(response);
        }
    }
}
