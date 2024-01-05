using AutoMapper;
using CoachAssistent.Common;
using CoachAssistent.Common.Enums;
using CoachAssistent.Data;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public abstract class BaseManager(CoachAssistentDbContext context, IMapper _mapper)
    {
        internal readonly CoachAssistentDbContext dbContext = context;
        internal readonly IMapper mapper = _mapper;

        internal async Task RequestGroupAccess(MembershipRequestViewModel request)
        {
            MembershipRequest? membershipRequest = dbContext.MembershipRequests
                .FirstOrDefault(mr => mr.UserId.Equals(request.UserId) && mr.GroupId.Equals(request.GroupId) && !mr.ResponseTimestamp.HasValue);

            if (membershipRequest is null)
            {
                membershipRequest = new MembershipRequest
                {
                    UserId = request.UserId,
                    GroupId = request.GroupId,
                    Description = request.Description,
                    RequestTimestamp = DateTime.Now
                };
                await dbContext.MembershipRequests.AddAsync(membershipRequest);

                IQueryable<Guid> administrators = dbContext.Members
                    .Where(m => m.GroupId.Equals(request.GroupId) && m.RoleId.Equals(SeedingLibrary.AdminId))
                    .Select(m => m.UserId);
                await dbContext.Notifications.AddRangeAsync(administrators.Select(a => new Notification
                {
                    FromUserId = request.UserId,
                    ToUserId = a,
                    GroupId = request.GroupId,
                    NotificationType = NotificationType.MembershipRequest,
                    SentDateTime = DateTime.Now
                }));
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
