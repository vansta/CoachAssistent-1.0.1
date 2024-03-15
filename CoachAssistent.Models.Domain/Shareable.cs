using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Shareable
    {
        public Guid Id { get; set; }
        public SharingLevel SharingLevel { get; set; }
        public Level Level { get; set; }
        public DateTime? VerifiedTS { get; set; }
        public Guid? VerifiedBy { get; set; }
        public ICollection<ShareablesXGroups> ShareablesXGroups { get; set; } = new HashSet<ShareablesXGroups>();
        public ICollection<Editor> Editors { get; set; } = new HashSet<Editor>();
        public ICollection<HistoryLog> HistoryLogs { get; set; } = new HashSet<HistoryLog>();
        public ICollection<HistoryLog> Copies { get; set; } = new HashSet<HistoryLog>();
        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
    }
}
