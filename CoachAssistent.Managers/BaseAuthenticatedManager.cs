using AutoMapper;
using CoachAssistent.Common.Enums;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.Domain.Permissions;
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
    public abstract class BaseAuthenticatedManager : BaseManager
    {
        internal readonly IAuthenticationWrapper authenticationWrapper;
        readonly IConfiguration _configuration;
        public BaseAuthenticatedManager(CoachAssistentDbContext context, IMapper _mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper) : base(context, _mapper)
        {
            this.authenticationWrapper = authenticationWrapper;
            this._configuration = configuration;
        }

        public void Can(string action, string subject)
        {
            IEnumerable<RolePermissionViewModel> rolePermissions = GetPermissions();
            bool can = rolePermissions.Any(rp =>
                rp.Subject!.Equals(subject)
                && rp.Action!.Equals(action));
            if (!can)
            {
                throw new UnauthorizedAccessException();
            }
        }

        public void Can(string action, IShareable shareable, string? field = null)
        {
            IEnumerable<RolePermissionViewModel> rolePermissions = GetPermissions();
            bool can = rolePermissions.Any(rp =>
                rp.Subject!.Equals("shareable")
                && rp.Action!.Equals(action)
                && (string.IsNullOrEmpty(field) || rp.Fields.Contains(field))
                && (rp.Condition != "groupIds" || rp.Ids == null || rp.Ids.Any(i => authenticationWrapper.User.GroupIds.Contains(i)) )
                && (rp.Condition != "editors" || shareable.Shareable!.Editors.Select(e => e.UserId).Contains(authenticationWrapper.UserId))
                );
            if (!can)
            {
                throw new UnauthorizedAccessException();
            }
        }

        public void Can(string action, Group group, string? field = null)
        {
            IEnumerable<RolePermissionViewModel> rolePermissions = GetPermissions();
            bool can = rolePermissions.Any(rp =>
                                            rp.Subject!.Equals("group")
                                            && rp.Action!.Equals(action)
                                            && (string.IsNullOrEmpty(field) || rp.Fields.Contains(field))
                                            && rp.Ids!.Contains(group.Id));

            if (!can)
            {
                throw new UnauthorizedAccessException();
            }
        }

        public IQueryable<T> FilterShareables<T>(IQueryable<T> collection, BaseSearchViewModel search) where T : IShareable
        {
            if (search is not null)
            {
                if (!string.IsNullOrEmpty(search.Search))
                {
                    collection = collection
                        .Where(e => e.Name.Contains(search.Search)
                            || (!string.IsNullOrEmpty(e.Description) && e.Description.Contains(search.Search)));
                }
                if (search.Tags is not null && search.Tags.Any())
                {
                    collection = collection
                        .Where(e => e.Tags.Select(t => t.Name).Any(t => search.Tags.Contains(t)));
                }
                if (search.OnlyFavorites.HasValue && search.OnlyFavorites.Value)
                {
                    collection = collection
                        .Where(e => e.Shareable!.Favorites.Select(f => f.UserId).Contains(authenticationWrapper.UserId));
                }
                if (!string.IsNullOrEmpty(search.Level) && int.TryParse(search.Level, out int level))
                {
                    collection = collection
                        .Where(c => c.Shareable!.Level == (Level)level);
                }
                if (search.OnlyOwned.HasValue && search.OnlyOwned.Value)
                {
                    collection = collection
                        .Where(c => c.Shareable!.Editors.Select(e => e.UserId).Contains(authenticationWrapper.UserId));
                }
            }
            return collection;
        }
        public IQueryable<T> PaginateShareables<T>(IQueryable<T> collection, BaseSearchViewModel search) where T : IShareable
        {
            if (!search.ShowAll)
            {
                collection = collection
                    .Skip(search.Skip)
                    .Take(search.ItemsPerPage);
            }
            return collection;
        }
        public IQueryable<T> FilterBySharingLevel<T>(IQueryable<T> collection) where T : IShareable
        {
            collection = collection
                .Where(c => !c.DeletedTS.HasValue);
            if (!authenticationWrapper.IsLoggedIn)
            {
                collection = collection.Where(c => c.Shareable!.SharingLevel == SharingLevel.Public);
            }
            else
            {
                var test = collection.ToList();
                Guid userId = authenticationWrapper.UserId;
                IEnumerable<Guid> groupIds = authenticationWrapper.User.GroupIds;
                collection = collection
                    .Where(c =>
                        c.Shareable != null &&
                        (
                        //public
                        (c.Shareable!.SharingLevel == SharingLevel.Public)
                    ||
                        //private
                        (/*c.Shareable.SharingLevel == SharingLevel.Private && */c.Shareable.Editors.Select(e => e.UserId).Contains(userId))
                    ||
                        //group
                        (c.Shareable.SharingLevel == SharingLevel.Group
                            && c.Shareable.ShareablesXGroups.Any(sg => groupIds.Contains(sg.GroupId))
                        )
                    ));
            }

            return collection;
        }

        internal async Task<Guid> AddHistoryLog(Guid shareableId, EditActionType editActionType, Guid? originId = null)
        {
            Shareable? shareable = await dbContext.Shareables.FindAsync(shareableId);
            shareable ??= (await dbContext.Shareables.AddAsync(new Shareable())).Entity;
            shareable.HistoryLogs.Add(new HistoryLog(editActionType, authenticationWrapper.UserId, originId));
            await dbContext.SaveChangesAsync();

            return shareable.Id;
        }

        internal ICollection<Tag> CondenseTags(IEnumerable<string>? tags)
        {
            return tags?.Where(x => !string.IsNullOrEmpty(x)).Select(x =>
            {
                Tag? tag = dbContext.Tags.FirstOrDefault(t => t.Name.ToUpper().Equals(x.ToUpper()));
                return tag ?? new Tag { Name = x };
            }).ToList() ?? new List<Tag>();
        }

        internal ICollection<Editor> CondenseEditors(IEnumerable<Guid>? editors, Shareable? shareable = null)
        {
            if (editors is null || !editors.Any())
            {
                if (shareable is not null && shareable.Editors.Count > 0)
                {
                    return shareable.Editors;
                }
                else
                {
                    //editors must contain at least one user
                    return new List<Editor> { new Editor { UserId = authenticationWrapper.UserId } };
                }
            };

            return editors.Select(x =>
            {
                Editor? editor = shareable?.Editors.FirstOrDefault(e => e.UserId.Equals(x));
                return editor ?? new Editor { UserId = x };
            }).ToList();
        }

        internal ICollection<ShareablesXGroups> CondenseGroups(IEnumerable<Guid>? groupIds, Shareable? shareable = null)
        {
            if (groupIds is null || !groupIds.Any())
            {
                return shareable?.ShareablesXGroups ?? new List<ShareablesXGroups>();
            };

            return groupIds.Select(x =>
            {
                ShareablesXGroups? shareableXGroup = shareable?.ShareablesXGroups.FirstOrDefault(e => e.GroupId.Equals(x));
                return shareableXGroup ?? new ShareablesXGroups { GroupId = x };
            }).ToList();
        }

        public IEnumerable<RolePermissionViewModel> GetPermissions()
        {
            List<RolePermissionViewModel> permissions = new List<RolePermissionViewModel>();
            Guid licenseId;
            if (authenticationWrapper.IsLoggedIn)
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
                    Reason = rp.Reason,
                    Fields = rp.Fields.Select(f => f.PermissionField!.Name),
                    Ids = new List<Guid> { m.GroupId },
                    Condition = rp.Subject!.Name == "group" ? "id" : "groupIds"
                })).ToList();
                permissions = groupPermissions;
                licenseId = authenticationWrapper.LicenseId;
            }
            else
            {
                permissions = new List<RolePermissionViewModel>();
                licenseId = dbContext.Licenses
                    .OrderBy(l => l.Level)
                    .First().Id;
            }

            

            var licensePermissions = dbContext.LicensePermissions
                .Where(lp => lp.LicenseId.Equals(licenseId))
                .Select(lp => new RolePermissionViewModel
                {
                    Action = lp.Action!.Name,
                    Subject = lp.Subject!.Name,
                    Reason = lp.Reason,
                    Fields = lp.Fields.Select(f => f.PermissionField!.Name),
                    Ids = new List<Guid> { authenticationWrapper.UserId },
                    UserId = authenticationWrapper.UserId,
                    Condition = lp.Subject!.Name == "shareable" ? "editors" : "none"
                }).ToList();

            permissions.AddRange(licensePermissions);

            return permissions;
        }
    }
}
