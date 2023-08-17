using CoachAssistent.Models.ViewModels.Exercise;
using CoachAssistent.Models.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Group
{
    public class EditGroupViewModel : CreateGroupViewModel
    {
        public Guid Id { get; set; }
        public ICollection<MembershipRequestViewModel> MembershipRequests { get; set; } = new HashSet<MembershipRequestViewModel>();
    }
}
