using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.User
{
    public class LoggedInUserViewModel
    {
        public LoggedInUserViewModel()
        {
            GroupIds = new List<Guid>();
        }
        public Guid? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public Guid? LicenseId { get; set; }
        public string? LicenseLevel { get; set; }

        public IEnumerable<Guid> GroupIds { get; set; }
        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
}
