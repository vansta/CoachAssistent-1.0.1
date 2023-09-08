using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Group;
using CoachAssistent.Models.ViewModels.Member;
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
            : base(context, mapper, configuration, authenticationWrapper)
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
                ParentGroupId = createGroupViewModel.ParentGroupId,
                Name = createGroupViewModel.Name ?? "New group",
                Description = createGroupViewModel.Description,
                Tags = CondenseTags(createGroupViewModel.Tags),
                Members = createGroupViewModel.Members is not null ? createGroupViewModel.Members.Select(m => new Member() { UserId = m.UserId, RoleId = m.RoleId }).ToList()
                : new List<Member>(),
                SubGroups = dbContext.Groups.Where(g => createGroupViewModel.SubGroups.Select(sg => sg.Id).Contains(g.Id)).ToHashSet()
        };

            Can("create", "group");
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
                    .Include(g => g.Members)
                    .Include(g => g.MembershipRequests.Where(mr => !mr.ResponseTimestamp.HasValue))
                    .Where(g => g.Members.Select(m => m.UserId).Contains(authenticationWrapper.UserId))
                    .Select(g => mapper.Map<GroupOverviewItemViewModel>(g))
            };
        }

        public async Task<EditGroupViewModel> GetGroup(Guid id)
        {
            Group group = await dbContext.Groups
                .Include(g => g.Members)
                .Include(g => g.Tags)
                .Include(g => g.SubGroups)
                .Include(g => g.MembershipRequests)
                    .ThenInclude(mr => mr.User)
                .SingleAsync(g => g.Id.Equals(id));

            return mapper.Map<EditGroupViewModel>(group);
        }

        public async Task UpdateGroup(EditGroupViewModel editGroupViewModel)
        {
            Group group = await dbContext.Groups
                .Include(g => g.Members)
                .Include(g => g.Tags)
                .Include(g => g.SubGroups)
                .SingleAsync(g => g.Id.Equals(editGroupViewModel.Id));

            Can("update", group);
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
            group.SubGroups = dbContext.Groups.Where(g => editGroupViewModel.SubGroups.Select(sg => sg.Id).Contains(g.Id)).ToHashSet();

            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<SelectViewModel> GetAvailableGroups(string? search, string? action)
        {
            IQueryable<Group> groups;
            if (string.IsNullOrEmpty(action))
            {
                groups = dbContext.Groups;
            }
            else
            {
                groups = dbContext.Members
                    .Include(m => m.Role!.RolePermissions).ThenInclude(rp => rp.Action)
                    .Include(m => m.Role!.RolePermissions).ThenInclude(rp => rp.Subject)
                    .Include(m => m.Group)
                    .Where(m => m.UserId == authenticationWrapper.UserId && m.Role!.RolePermissions.Any(rp => rp.Action!.Name.Equals(action) && rp.Subject!.Name.Equals("group")))
                    .Select(m => m.Group!);
            }
            return groups
                .Where(g => string.IsNullOrEmpty(search) || g.Name.Contains(search))
                .Select(g => new SelectViewModel(g.Id, g.Name))
                    .ToList();
        }

        public async Task RespondToMembershipRequest(MembershipRequestResponseViewModel response)
        {
            MembershipRequest? membershipRequest = await dbContext.MembershipRequests
                .Include(mr => mr.Group)
                .FirstOrDefaultAsync(mr => mr.Id == response.Id);
            if (membershipRequest is not null)
            {
                Can("update", membershipRequest.Group!);
                if (response.Response)
                {
                    Guid roleId = response.RoleId ?? dbContext.Roles.OrderBy(r => r.Index).First().Id;
                    Member member = new()
                    {
                        GroupId = membershipRequest.GroupId,
                        UserId = membershipRequest.UserId,
                        RoleId = roleId
                    };
                    await dbContext.Members.AddAsync(member);
                }
                membershipRequest.ResponseTimestamp = DateTime.Now;
                await dbContext.SaveChangesAsync();           }
        }

        public Task RequestMembership(Guid groupId)
        {
            return RequestGroupAccess(new MembershipRequestViewModel
            {
                GroupId = groupId,
                UserId = authenticationWrapper.UserId
            });
        }

        public IEnumerable<SelectViewModel> GetMembers(Guid groupId)
        {
            return dbContext.Members
                .Include(m => m.User)
                .Where(m => m.GroupId.Equals(groupId))
                .Select(m => new SelectViewModel(m.UserId, m.User!.UserName));
        }

        public async Task<GroupMinimalViewModel> GetGroupMinimal(Guid id)
        {
            Group group = await dbContext.Groups
                .SingleAsync(g => g.Id.Equals(id));

            return mapper.Map<GroupMinimalViewModel>(group);
        }
    }
}
