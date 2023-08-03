using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Permission
{
    public class RolePermissionViewModel
    {
        public string? Action { get; set; }
        public string? Subject { get; set; }
        public IEnumerable<Guid>? Ids { get; set; }
        public IEnumerable<string>? Fields { get; set; }
    }
}
