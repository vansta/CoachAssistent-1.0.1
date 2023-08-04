using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class HistoryLog
    {
        public HistoryLog()
        {

        }
        public HistoryLog(EditActionType editAction, Guid userId, Guid? originId = null)
        {
            Timestamp = DateTime.Now;
            EditActionType = editAction;
            UserId = userId;
            OriginId = originId;
        }

        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public EditActionType EditActionType { get; set; }
        public Guid ShareableId { get; set; }
        public Guid? OriginId { get; set; }
        public Guid UserId { get; set; }

        public Shareable? Shareable { get; set; }
        public Shareable? Origin { get; set; }
        public User? User { get; set; }
    }
}
