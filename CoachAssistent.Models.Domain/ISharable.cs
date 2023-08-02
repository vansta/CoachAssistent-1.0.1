using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public interface ISharable
    {
        public Guid Id { get; set; }
        public SharingLevel Shared { get; set; }
        //public Guid UserId { get; set; }
        //public User? User { get; set; }

        public DateTime? DeletedTS { get; set; }
        public ICollection<Editor> Editors { get; set; }
        public ICollection<SharablesXGroups> SharablesXGroups { get; set; }
    }
}
