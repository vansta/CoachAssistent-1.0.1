using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Permission
{
    public class RolePermissionOverviewItemViewModel
    {
        public int Id { get; set; }
        public string? ActionId { get; set; }
        public string? SubjectId { get; set; }
        public string? Reason { get; set; }

        public ICollection<string>? Fields { get; set; }
    }
}
