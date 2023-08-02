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

        public IQueryable<T> FilterBySharingLevel<T>(IQueryable<T> collection) where T : ISharable
        {
            collection = collection
                .Where(c => !c.DeletedTS.HasValue);
            if (!authenticationWrapper.IsLoggedIn)
            {
                collection = collection.Where(c => c.SharingLevel == SharingLevel.Public);
            }
            else
            {
                Guid userId = authenticationWrapper.UserId;
                IEnumerable<Guid> groupIds = authenticationWrapper.User.GroupIds;
                collection = collection
                    .Where(c =>
                        //public
                        (c.SharingLevel == SharingLevel.Public)
                    ||
                        //private
                        (c.SharingLevel == SharingLevel.Private && c.Editors.Select(e => e.UserId).Contains(userId))
                    ||
                        //group
                        (c.SharingLevel == SharingLevel.Group
                            && c.SharablesXGroups.Any(sg => groupIds.Contains(sg.GroupId))
                            //&& c.User != null
                            //&& c.User.Groups.Select(ug => ug.Id).Any(ugi => groupIds.Contains(ugi))
                        )
                    );
            }

            return collection;
        }

        internal ICollection<Tag> CondenseTags(IEnumerable<string>? tags)
        {
            return tags?.Where(x => !string.IsNullOrEmpty(x)).Select(x =>
            {
                Tag? tag = dbContext.Tags.FirstOrDefault(t => t.Name.ToUpper().Equals(x.ToUpper()));
                return tag ?? new Tag { Name = x };
            }).ToList() ?? new List<Tag>();
        }

        internal ICollection<Editor> CondenseEditors(ISharable sharable, IEnumerable<Guid>? editors)
        {
            if (editors is null || !editors.Any())
            {
                if (sharable.Editors.Count > 0)
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
                Editor? editor = sharable.Editors.FirstOrDefault(e => e.UserId.Equals(x));
                return editor ?? new Editor { UserId = x };
            }).ToList();
        }
    }
}
