using CoachAssistent.Models.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers.Helpers
{
    public class AuthenticationWrapper(IHttpContextAccessor contextAccessor) : IAuthenticationWrapper
    {
        readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        public Guid UserId => _contextAccessor.UserId();
        public Guid LicenseId => _contextAccessor.LicenseId();
        public int LicenseLevel => _contextAccessor.LicenseLevel();

        public bool IsLoggedIn => _contextAccessor.IsLoggedIn();

        public LoggedInUserViewModel User => _contextAccessor.GetCurrentUser();
    }

    public interface IAuthenticationWrapper
    {
        public Guid UserId { get; }
        public Guid LicenseId { get; }
        public int LicenseLevel { get; }
        public bool IsLoggedIn { get; }
        public LoggedInUserViewModel User { get; }
    }
}
