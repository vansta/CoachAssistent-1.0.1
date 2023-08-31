using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.ViewModels.Role;
using CoachAssistent.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoachAssistent.Managers;
using CoachAssistent.Models.ViewModels.License;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        readonly LicenseManager licenseManager;
        readonly IAuthenticationWrapper authenticationWrapper;
        public LicenseController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            authenticationWrapper = new AuthenticationWrapper(httpContextAccessor);
            licenseManager = new LicenseManager(dbContext, mapper, configuration, authenticationWrapper);
        }

        [HttpGet("Overview")]
        public OverviewViewModel<LicenseOverviewItemViewModel> GetRolesOverview()
        {
            return licenseManager.GetOverview();
        }

        [HttpPut]
        public Task PutRole(LicenseOverviewItemViewModel license)
        {
            return licenseManager.UpdateLicense(license);
        }
    }
}
