using CoachAssistent.Models.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Group
{
    public class CreateGroupViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<string>? Tags { get; set; }
        public ICollection<CreateMemberViewModel>? Members { get; set; }
    }
}
