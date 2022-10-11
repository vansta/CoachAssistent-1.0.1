using CoachAssistent.Models.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CoachAssistent.Managers.Helpers
{
    public static class HttpContextExtensions
    {
        public static bool IsLoggedIn(this IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier);
        }
        public static Guid UserId(this IHttpContextAccessor contextAccessor)
        {
            var claims = contextAccessor.HttpContext.User.Claims;
            string? userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userId, out Guid id) ? id : Guid.Empty;
        }
        public static LoggedInUserViewModel GetCurrentUser(this IHttpContextAccessor contextAccessor)
        {
            var claims = contextAccessor.HttpContext.User.Claims;
            string? userId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            string? userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            string? email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            Guid groupId;
            List<Guid> groupIds = claims
                .Where(c => c.Type == CustomClaimTypes.Group)
                //.Select(c => c.Value)
                .AsQueryable()
                .Select(g => Guid.TryParse(g.Value, out groupId) ? groupId : Guid.Empty)
                .Where(g => !g.Equals(Guid.Empty))
                .ToList();

            return new LoggedInUserViewModel
            {
                Id = Guid.TryParse(userId, out Guid id) ? id : null,
                UserName = userName,
                Email = email,
                GroupIds = groupIds
            };
        }
    }

    public static class CustomClaimTypes
    {
        public static string Id { get { return "id"; } }
        public static string Name { get { return "name"; } }
        public static string Email { get { return "email"; } }
        public static string Group { get { return "Group"; } }
    }
}