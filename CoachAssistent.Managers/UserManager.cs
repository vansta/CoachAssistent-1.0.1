using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.Domain.Permissions;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Member;
using CoachAssistent.Models.ViewModels.Permission;
using CoachAssistent.Models.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoachAssistent.Managers
{
    public class UserManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper) : BaseAuthenticatedManager(context, mapper, configuration, authenticationWrapper)
    {
        public bool CheckUserName(string userName)
        {
            return !dbContext.Users
                .Any(u => u.UserName.Equals(userName));
        }

        public IEnumerable<Guid> GetAssignedEditors(Guid id)
        {
            return dbContext.Editors
                .Where(e => e.ShareableId.Equals(id))
                .Select(e => e.UserId);
        }

        public IEnumerable<SelectViewModel> SearchUsers(string? search)
        {
            return dbContext.Users
                .Where(u => string.IsNullOrEmpty(search) || u.UserName.Contains(search))
                .Select(u => new SelectViewModel(u.Id, u.UserName));
        }

        public IEnumerable<SelectViewModel> GetAvailableEditors()
        {
            return dbContext.Members
                .Include(m => m.User)
                .Where(m => authenticationWrapper.User.GroupIds.Contains(m.GroupId))
                .Select(m => m.User).Distinct()
                .Select(u => new SelectViewModel(u!.Id, u.UserName));
        }

        public async Task<ProfileViewModel> GetProfile()
        {
            User user = await dbContext.Users
                .Include(u => u.Tags)
                .Include(u => u.Memberships)
                    .ThenInclude(m => m.Group)
                .Include(u => u.Memberships)
                    .ThenInclude(m => m.Role)
                .Include(u => u.MembershipRequests)
                    .ThenInclude(m => m.Group)
                .SingleAsync(u => u.Id == authenticationWrapper.UserId);

            ProfileViewModel profile = mapper.Map<ProfileViewModel>(user);
            profile.Memberships.AddRange(user.MembershipRequests.Where(mr => !mr.ResponseTimestamp.HasValue).Select(mr => new MembershipOverviewItemViewModel
            {
                GroupId = mr.GroupId,
                Group = mr.Group!.Name
            }));
            return profile;
        }

        public async Task UpdateUser(ProfileViewModel profileViewModel)
        {
            User user = await dbContext.Users
                .Include(u => u.Memberships)
                .Include(u => u.Tags)
                .SingleAsync(u => u.Id == authenticationWrapper.UserId);

            user.UserName = profileViewModel.UserName ?? user.UserName;
            user.Email = profileViewModel.Email;
            user.Memberships = user.Memberships
                .Where(m => profileViewModel.Memberships.Select(ms => ms.Id).Contains(m.Id)).ToHashSet();
            user.Tags = CondenseTags(profileViewModel.Tags);

            foreach (var group in profileViewModel.Memberships.Where(g => !user.Memberships.Select(m => m.GroupId).Contains(g.GroupId)))
            {
                await RequestGroupAccess(new MembershipRequestViewModel
                {
                    UserId = authenticationWrapper.UserId,
                    GroupId = group.GroupId
                });
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
