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
    public class UserManager : BaseAuthenticatedManager
    {
        readonly IConfiguration configuration;
        public UserManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, authenticationWrapper)
        {
            this.configuration = configuration;
        }

        public IEnumerable<SelectViewModel> GetAvailableEditors()
        {
            return dbContext.Users.Select(u => new SelectViewModel(u.Id, u.UserName));
        }
    }
}
