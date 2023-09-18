using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoachAssistent.Models.ViewModels.Notification;

namespace CoachAssistent.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        readonly NotificationManager notificationManager;
        readonly IAuthenticationWrapper authenticationWrapper;
        public NotificationController(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            authenticationWrapper = new AuthenticationWrapper(httpContextAccessor);
            notificationManager = new NotificationManager(dbContext, mapper, configuration, authenticationWrapper);
        }

        [HttpGet]
        public IEnumerable<NotificationOverviewItemViewModel> GetNotifications(DateTime? lastCheck)
        {
            return notificationManager.GetNotifications(lastCheck);
        }

        [HttpPut]
        public Task MarkAsRead([FromBody]int? id)
        {
            return notificationManager.MarkAsRead(id);
        }
    }
}
