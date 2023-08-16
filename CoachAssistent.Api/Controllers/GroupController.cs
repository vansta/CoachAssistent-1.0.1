﻿using AutoMapper;
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
        public IEnumerable<SelectViewModel> GetAvailableGroups()
        {
            return groupManager.GetAvailableGroups();
        }

        [HttpPost]
        public Task<Guid> CreateGroup(CreateGroupViewModel createGroupViewModel)
        {
            return groupManager.CreateGroup(createGroupViewModel);
        }

        [HttpPut]
        public async Task UpdateGroup(EditGroupViewModel editGroupViewModel)
        {
            await groupManager.UpdateGroup(editGroupViewModel);
        }
    }
}
