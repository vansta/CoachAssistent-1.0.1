using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class ShareablesXGroups
    {
        public Guid Id { get; set; }
        public Guid ShareableId { get; set; }
        public Guid GroupId { get; set; }

        public Shareable? Shareable { get; set; }
        public Group? Group { get; set; }
    }
}
