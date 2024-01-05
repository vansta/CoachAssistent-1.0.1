using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class NotificationManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper) : BaseAuthenticatedManager(context, mapper, configuration, authenticationWrapper)
    {
        public IEnumerable<NotificationOverviewItemViewModel> GetNotifications(DateTime? lastCheck)
        {
            return dbContext.Notifications
                .Include(n => n.FromUser)
                .Include(n => n.Group)
                .Where(n => 
                    n.ToUserId.Equals(authenticationWrapper.UserId)
                    && (!lastCheck.HasValue || lastCheck.Value < n.SentDateTime)
                    && (!n.ReadDateTime.HasValue || n.ReadDateTime.Value > DateTime.Now.AddDays(-1)))
                .Select(n => mapper.Map<NotificationOverviewItemViewModel>(n));
        }

        public async Task MarkAsRead(int? id)
        {
            if (id.HasValue)
            {
                Notification? notification = await dbContext.Notifications.FindAsync(id);
                if (notification is not null)
                {
                    notification.ReadDateTime = DateTime.Now;
                }
            }
            else
            {
                List<Notification> notifications = await dbContext.Notifications
                    .Where(n => n.ToUserId.Equals(authenticationWrapper.UserId) && (!n.ReadDateTime.HasValue || n.ReadDateTime.Value < DateTime.Now.AddDays(-1)))
                    .ToListAsync();

                foreach (var notification in notifications)
                {
                    notification.ReadDateTime = DateTime.Now;
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
