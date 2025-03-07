using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Notification
{
    public class NotificationOverviewItemViewModel
    {
        public int Id { get; set; }
        public string FromUser { get; set; } = null!;
        public Guid? GroupId { get; set; }
        public string? Group { get; set; }
        public string? NotificationType { get; set; }
        public DateTime? ReadDateTime { get; set; }
    }
}
