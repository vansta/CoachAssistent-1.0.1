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
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public EditActionType EditActionType { get; set; }
        public Guid TrainingId { get; set; }
        public Guid UserId { get; set; }
    }
}
