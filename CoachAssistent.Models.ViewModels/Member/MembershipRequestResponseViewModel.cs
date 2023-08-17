using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Member
{
    public class MembershipRequestResponseViewModel
    {
        public int Id { get; set; }
        public Guid? RoleId { get; set; }
        public bool Response { get; set; }
    }
}
