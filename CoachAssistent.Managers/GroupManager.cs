using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Data.Migrations;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Group;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class GroupManager : BaseAuthenticatedManager
    {
        //readonly IConfiguration configuration;

        public GroupManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, authenticationWrapper)
        {
            //this.configuration = configuration;
        }

        public async Task<IEnumerable<SelectViewModel>> GetGroupsForUser()
        {
            User user = await dbContext.Users
                .Include(u => u.Memberships).ThenInclude(m => m.Group)
                .SingleAsync(u => u.Id == authenticationWrapper.UserId);

            return user.Memberships.Select(m => new SelectViewModel(m.GroupId, m.Group?.Name));
        }

        public async Task<Guid> CreateGroup(CreateGroupViewModel createGroupViewModel)
        {
            Group group = new()
            {
                Name = createGroupViewModel.Name ?? "New group",
                Description = createGroupViewModel.Description,
                Tags = CondenseTags(createGroupViewModel.Tags),
                Members = createGroupViewModel.Members is not null ? createGroupViewModel.Members.Select(m => new Member() { UserId = m.UserId, RoleId = m.RoleId }).ToList()
                : new List<Member>()
            };

            var addGroup = await dbContext.Groups.AddAsync(group);
            await dbContext.SaveChangesAsync();

            return addGroup.Entity.Id;
        }

        public OverviewViewModel<GroupOverviewItemViewModel> GetGroups()
        {
            return new OverviewViewModel<GroupOverviewItemViewModel>
            {
                Items = dbContext.Groups
                    .Include(g => g.Tags)
                    .Where(g => g.Members.Select(m => m.UserId).Contains(authenticationWrapper.UserId))
                    .Select(g => mapper.Map<GroupOverviewItemViewModel>(g))
            };
        }

        public async Task<EditGroupViewModel> GetGroup(Guid id)
        {
            Group group = await dbContext.Groups
                .Include(g => g.Members)
                .Include(g => g.Tags)
                .SingleAsync(g => g.Id.Equals(id));

            return mapper.Map<EditGroupViewModel>(group);
        }

        public async Task UpdateGroup(EditGroupViewModel editGroupViewModel)
        {
            Group group = await dbContext.Groups
                .Include(g => g.Members)
                .Include(g => g.Tags)
                .SingleAsync(g => g.Id.Equals(editGroupViewModel.Id));

            group.Name = editGroupViewModel.Name ?? "New group";
            group.Description = editGroupViewModel.Description;
            group.Tags = CondenseTags(editGroupViewModel.Tags);

            if (editGroupViewModel.Members is not null && editGroupViewModel.Members.Any())
            {
                group.Members = editGroupViewModel.Members.Select(x =>
                {
                    Member? member = group.Members.FirstOrDefault(m => m.UserId.Equals(x.UserId));
                    if (member is not null)
                    {
                        member.RoleId = x.RoleId;
                    }
                    else
                    {
                        member = new Member
                        {
                            UserId = x.UserId,
                            RoleId = x.RoleId
                        };
                    }
                    return member;
                }).ToList();
            };

            await dbContext.SaveChangesAsync();
        }
    }
}
