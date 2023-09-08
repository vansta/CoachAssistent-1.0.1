using CoachAssistent.Models.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.User
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid LicenseId { get; set; }
        public List<MembershipOverviewItemViewModel> Memberships { get; set; } = new List<MembershipOverviewItemViewModel>();
        public List<string> Tags { get; set; } = new List<string>();
    }
}
