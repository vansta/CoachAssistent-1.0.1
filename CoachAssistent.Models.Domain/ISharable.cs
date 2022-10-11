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
        public SharingLevel Shared { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
