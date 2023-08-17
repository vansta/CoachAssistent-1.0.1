using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Member
{
    public class MembershipRequestViewModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public string? Description { get; set; }
        public string? UserName { get; set; }
    }
}
