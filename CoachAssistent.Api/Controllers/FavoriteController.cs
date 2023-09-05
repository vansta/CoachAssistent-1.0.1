using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers;
using CoachAssistent.Managers.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        readonly FavoriteManager favoriteManager;
        readonly IAuthenticationWrapper authenticationWrapper;
        public FavoriteController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            authenticationWrapper = new AuthenticationWrapper(httpContextAccessor);
            favoriteManager = new FavoriteManager(dbContext, mapper, configuration, authenticationWrapper);
        }

        [HttpPut]
        public Task SetFavorite([FromBody]Guid shareableId)
        {
            return favoriteManager.SetFavorite(shareableId);
        }
    }
}
