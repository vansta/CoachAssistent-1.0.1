using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
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
            : base(context, mapper, authenticationWrapper)
        {
            //this.configuration = configuration;
        }

        public async Task<IEnumerable<SelectViewModel>> GetGroupsForUser()
        {
            User user = await dbContext.Users
                .Include(u => u.Groups)
                .SingleAsync(u => u.Id == authenticationWrapper.UserId);

            return user.Groups.Select(g => new SelectViewModel(g.Id, g.Name));
        }
    }
}
