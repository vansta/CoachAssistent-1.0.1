using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Permission;
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
        readonly IConfiguration configuration;
        public UserManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, authenticationWrapper)
        {
            this.configuration = configuration;
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

        public IEnumerable<RolePermissionViewModel> GetPermissions()
        {
            var groupPermissions = dbContext.Members
                .Include(m => m.Role!.RolePermissions)
                    .ThenInclude(rp => rp.Action)
                .Include(m => m.Role!.RolePermissions)
                    .ThenInclude(rp => rp.Subject)
                .Include(m => m.Role!.RolePermissions)
                    .ThenInclude(rp => rp.Fields).ThenInclude(f => f.PermissionField)
                .Where(m => m.UserId.Equals(authenticationWrapper.UserId))
                .SelectMany(m => m.Role!.RolePermissions.Select(rp => new RolePermissionViewModel
                {
                    Action = rp.Action!.Name,
                    Subject = rp.Subject!.Name,
                    Fields = rp.Fields.Select(f => f.PermissionField!.Name),
                    GroupId = m.GroupId
                })).ToList();

            var test = dbContext.Members
                .Include(m => m.Role!.RolePermissions)
                    .ThenInclude(rp => rp.Action)
                .Include(m => m.Role!.RolePermissions)
                    .ThenInclude(rp => rp.Subject)
                .Include(m => m.Role!.RolePermissions)
                    .ThenInclude(rp => rp.Fields).ThenInclude(f => f.PermissionField)
                    .Where(m => m.UserId.Equals(authenticationWrapper.UserId));

            return groupPermissions;

            //var editorPermissions = dbContext.Editors
            //    .Where(e => e.UserId.Equals(authenticationWrapper.UserId))
        }
    }
}
