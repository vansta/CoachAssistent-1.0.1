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
                collection = collection.Where(c => c.Shared == SharingLevel.Public);
            }
            else
            {
                Guid userId = authenticationWrapper.UserId;
                IEnumerable<Guid> groupIds = authenticationWrapper.User.GroupIds;
                collection = collection
                    .Where(c =>
                        //public
                        (c.Shared == SharingLevel.Public)
                    ||
                        //private
                        (c.Shared == SharingLevel.Private && c.UserId == userId)
                    ||
                        //group
                        (c.Shared == SharingLevel.Group
                            && c.User != null
                            && c.User.Groups.Select(ug => ug.Id).Any(ugi => groupIds.Contains(ugi))
                        )
                    );
            }

            return collection;
        }
    }
}
