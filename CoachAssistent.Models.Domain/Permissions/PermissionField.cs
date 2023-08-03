using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain.Permissions
{
    public class PermissionField
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; } = null!;
        [MaxLength(128)]
        public string? Description { get; set; }

        public int SubjectId { get; set; }
        public PermissionSubject? Subject { get; set; }

        public ICollection<RolePermissionXPermissionField> RolePermissions { get; set; } = new HashSet<RolePermissionXPermissionField>();
    }
}
