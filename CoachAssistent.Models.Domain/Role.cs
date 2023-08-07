using CoachAssistent.Models.Domain.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Role
    {
        public Role()
        {
            Name = string.Empty;
        }
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }
        public int Index { get; set; }
        public int MinimalLicenseLevel { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    }
}
