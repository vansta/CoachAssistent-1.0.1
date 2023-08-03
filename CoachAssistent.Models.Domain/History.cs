using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class History
    {
        public History()
        {
            HistoryLogs = new HashSet<HistoryLog>();
        }
        public int Id { get; set; }
        public ICollection<HistoryLog> HistoryLogs { get; set; }
    }
}
