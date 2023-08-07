using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoachAssistent.Models.ViewModels;
using CoachAssistent.Models.ViewModels.Permission;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        readonly PermissionManager permissionManager;
        //readonly IAuthenticationWrapper authenticationWrapper;
        public PermissionController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            var authenticationWrapper = new AuthenticationWrapper(httpContextAccessor);
            permissionManager = new PermissionManager(dbContext, mapper, configuration, authenticationWrapper);
        }

        [HttpGet("Actions")]
        public IEnumerable<SelectViewModel> GetActions()
        {
            return permissionManager.GetActions();
        }

        [HttpGet("Subjects")]
        public IEnumerable<PermissionSubjectSelectViewModel> GetSubjects()
        {
            return permissionManager.GetSubjects();
        }
    }
}
