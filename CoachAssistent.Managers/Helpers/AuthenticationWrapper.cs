using CoachAssistent.Models.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers.Helpers
{
    public class AuthenticationWrapper : IAuthenticationWrapper
    {
        readonly IHttpContextAccessor _contextAccessor;
        public AuthenticationWrapper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid UserId => _contextAccessor.UserId();

        public bool IsLoggedIn => _contextAccessor.IsLoggedIn();

        public LoggedInUserViewModel User => _contextAccessor.GetCurrentUser();
    }

    public interface IAuthenticationWrapper
    {
        public Guid UserId { get; }
        public bool IsLoggedIn { get; }
        public LoggedInUserViewModel User { get; }
    }
}
