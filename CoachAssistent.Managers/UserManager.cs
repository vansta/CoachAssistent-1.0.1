using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.Domain.Permissions;
using CoachAssistent.Models.ViewModels;
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
    public class UserManager : BaseAuthenticatedManager
    {
        //readonly IConfiguration configuration;
        public UserManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, configuration, authenticationWrapper)
        {
            //this.configuration = configuration;
        }

        public IEnumerable<Guid> GetAssignedEditors(Guid id)
        {
            return dbContext.Editors
                .Where(e => e.ShareableId.Equals(id))
                .Select(e => e.UserId);
        }

        public IEnumerable<SelectViewModel> GetAvailableEditors()
        {
            return dbContext.Users.Select(u => new SelectViewModel(u.Id, u.UserName));
        }

        public async Task<ProfileViewModel> GetProfile()
        {
            User user = await dbContext.Users
                .Include(u => u.Memberships)
                .SingleAsync(u => u.Id == authenticationWrapper.UserId);

            return mapper.Map<ProfileViewModel>(user);
        }

        public async Task UpdateUser(ProfileViewModel profileViewModel)
        {
            User user = await dbContext.Users
                .Include(u => u.Memberships)
                .SingleAsync(u => u.Id == authenticationWrapper.UserId);

            user.UserName = profileViewModel.UserName ?? user.UserName;
            user.Email = profileViewModel.Email;

            foreach (var groupId in profileViewModel.Memberships.Where(g => !user.Memberships.Select(m => m.GroupId).Contains(g)))
            {
                await RequestGroupAccess(authenticationWrapper.UserId, groupId);
            }
        }

        private Task RequestGroupAccess(Guid userId, Guid groupId)
        {
            throw new NotImplementedException();
        }
    }
}
