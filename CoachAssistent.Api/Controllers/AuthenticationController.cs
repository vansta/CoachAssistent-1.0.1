using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly AccountManager accountManager;
        public AuthenticationController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            IAuthenticationWrapper authenticationWrapper = new AuthenticationWrapper(httpContextAccessor);
            accountManager = new AccountManager(dbContext, mapper, configuration, authenticationWrapper);
        }

        [Authorize]
        [HttpGet("CheckToken")]
        public IActionResult CheckToken()
        {
            //return Unauthorized();
            return Ok("Ok");
        }

        [Authorize]
        [HttpGet("RefreshToken")]
        public Task<string> RefreshToken()
        {
            return accountManager.RefreshToken();
        }

        [HttpGet("ResetRequest")]
        public Task<ResetPasswordViewModel> GetResetRequest(Guid id)
        {
            return accountManager.ResetRequest(id);
        }

        [HttpPost]
        public Task<string> Login(CredentialsViewModel credentials)
        {
            return accountManager.Login(credentials);
        }

        [HttpPost("Register")]
        public Task<string> Register(RegisterViewModel registerData)
        {
            return accountManager.Register(registerData);
        }

        [HttpPost("RequestResetPassword")]
        public Task ResetPassword([FromBody]string userName)
        {
            return accountManager.RequestPasswordReset(userName);
        }

        [HttpPost("ResetPassword")]
        public Task<string> ResetPassword(ResetPasswordViewModel credentials)
        {
            return accountManager.ResetPassword(credentials);
        }
    }
}
