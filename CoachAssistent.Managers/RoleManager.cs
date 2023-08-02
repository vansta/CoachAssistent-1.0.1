using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class RoleManager : BaseAuthenticatedManager
    {
        readonly IConfiguration configuration;

        public RoleManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, authenticationWrapper)
        {
            this.configuration = configuration;
        }

        public IEnumerable<SelectViewModel> GetRoles()
        {
            return dbContext.Roles.Select(r => new SelectViewModel(r.Id, r.Name));
        }
    }
}
