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
    public abstract class BaseManager
    {
        internal readonly CoachAssistentDbContext dbContext;
        internal readonly IMapper mapper;
        public BaseManager(CoachAssistentDbContext context, IMapper _mapper)
        {
            dbContext = context;
            mapper = _mapper;
        }

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
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
