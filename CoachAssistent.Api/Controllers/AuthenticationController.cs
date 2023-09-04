using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
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
        public AuthenticationController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            accountManager = new AccountManager(dbContext, mapper, configuration);
        }

        [Authorize]
        [HttpGet("CheckToken")]
        public IActionResult CheckToken()
        {
            //return Unauthorized();
            return Ok("Ok");
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
    }
}
