using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoachAssistent.Models.ViewModels;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserManager userManager;
        readonly IAuthenticationWrapper authenticationWrapper;
        public UserController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            authenticationWrapper = new AuthenticationWrapper(httpContextAccessor);
            userManager = new UserManager(dbContext, mapper, configuration, authenticationWrapper);
        }

        [HttpGet]
        public IEnumerable<SelectViewModel> GetAvailableEditors()
        {
            return userManager.GetAvailableEditors();
        }

        [HttpGet("AssignedEditors")]
        public IEnumerable<Guid> GetAssignedEditors(Guid id, string? type = null)
        {
            return userManager.GetAssignedEditors(id, type);
        }

        [HttpGet("Permissions")]
        public void GetPermissions()
        {

        }
    }
}
