using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        readonly RoleManager roleManager;
        readonly IAuthenticationWrapper authenticationWrapper;
        public RoleController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            authenticationWrapper = new AuthenticationWrapper(httpContextAccessor);
            roleManager = new RoleManager(dbContext, mapper, configuration, authenticationWrapper);
        }

        [HttpGet]
        public IEnumerable<SelectViewModel> GetRoles()
        {
            return roleManager.GetRoles();
        }
    }
}
