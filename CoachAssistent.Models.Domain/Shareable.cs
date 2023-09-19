using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public double Score { get {
                return Reviews.Average(r => r.Score);
            } }
        public ICollection<ShareablesXGroups> ShareablesXGroups { get; set; } = new HashSet<ShareablesXGroups>();
        public ICollection<Editor> Editors { get; set; } = new HashSet<Editor>();
        public ICollection<HistoryLog> HistoryLogs { get; set; } = new HashSet<HistoryLog>();
        public ICollection<HistoryLog> Copies { get; set; } = new HashSet<HistoryLog>();
        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}
