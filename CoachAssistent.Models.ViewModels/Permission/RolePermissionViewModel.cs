using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Permission
{
    public class RolePermissionViewModel
    {
        public RolePermissionViewModel()
        {

        }
        public string? Action { get; set; }
        public string? Subject { get; set; }
        public string? Reason { get; set; }
        public IEnumerable<Guid>? Ids { get; set; }
        public Guid? UserId { get; set; }
        public IEnumerable<string?> Fields { get; set; } = new List<string?>();
        public string? Condition { get; set; }
    }
}
