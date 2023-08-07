using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain.Permissions
{
    public class RolePermission
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public int ActionId { get; set; }
        public int? SubjectId { get; set; }
        [MaxLength(256)]
        public string? Reason { get; set; }

        public Role? Role { get; set; }
        public PermissionAction? Action { get; set; }
        public PermissionSubject? Subject { get; set; }

        public ICollection<RolePermissionXPermissionField> Fields { get; set; } = new HashSet<RolePermissionXPermissionField>();
    }
}
