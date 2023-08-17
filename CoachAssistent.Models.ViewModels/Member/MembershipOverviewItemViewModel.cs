using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Member
{
    public class MembershipOverviewItemViewModel
    {
        public Guid? Id { get; set; }

        public Guid GroupId { get; set; }

        public string? Group { get; set; }
        public string? Role { get; set; }
    }
}
