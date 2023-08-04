using AutoMapper;
using CoachAssistent.Common.Enums;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
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
        public BaseAuthenticatedManager(CoachAssistentDbContext context, IMapper _mapper, IAuthenticationWrapper authenticationWrapper) : base(context, _mapper)
        {
            this.authenticationWrapper = authenticationWrapper;
        }

        //public bool Can<T>(Guid entityId, string subject, string action, string field) where T : ISharable
        //{
        //    switch (subject)
        //    {
        //        case "group":
        //            return dbContext.Members
        //                .Where(m => m.GroupId.Equals(entityId) && m.UserId == authenticationWrapper.UserId)
        //                .Any(m => m.Role.RolePermissions)
        //        default:
        //            break;
        //    }
        //}

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
                Guid userId = authenticationWrapper.UserId;
                IEnumerable<Guid> groupIds = authenticationWrapper.User.GroupIds;
                var test = collection.First();
                collection = collection
                    .Where(c =>
                        c.Shareable != null &&
                        (
                        //public
                        (c.Shareable!.SharingLevel == SharingLevel.Public)
                    ||
                        //private
                        (c.Shareable.SharingLevel == SharingLevel.Private && c.Shareable.Editors.Select(e => e.UserId).Contains(userId))
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

        internal ICollection<Editor> CondenseEditors(IEnumerable<Guid>? editors, Shareable? sharable = null)
        {
            if (editors is null || !editors.Any())
            {
                if (sharable is not null && sharable.Editors.Count > 0)
                {
                    return sharable.Editors;
                }
                else
                {
                    //editors must contain at least one user
                    return new List<Editor> { new Editor { UserId = authenticationWrapper.UserId } };
                }
            };

            return editors.Select(x =>
            {
                Editor? editor = sharable?.Editors.FirstOrDefault(e => e.UserId.Equals(x));
                return editor ?? new Editor { UserId = x };
            }).ToList();
        }
    }
}
