using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Notification
    {
        public int Id { get; set; }
        public Guid ToUserId { get; set; }
        public Guid FromUserId { get; set; }
        public Guid? GroupId { get; set; }
        public Guid? ShareableId { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime SentDateTime { get; set; }
        public DateTime? ReadDateTime { get; set; }

        public User? ToUser { get; set; }
        public User? FromUser { get; set; }
        public Group? Group { get; set; }
        public Shareable? Shareable { get; set; }
    }
}
