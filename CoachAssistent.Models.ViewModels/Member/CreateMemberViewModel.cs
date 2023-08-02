using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Member
{
    public class CreateMemberViewModel
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
