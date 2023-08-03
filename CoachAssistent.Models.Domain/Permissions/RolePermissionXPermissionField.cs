using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain.Permissions
{
    public class RolePermissionXPermissionField
    {
        public int Id { get; set; }
        public int RolePermissionId { get; set; }
        public int PermissionFieldId { get; set; }

        public RolePermission? RolePermission { get; set; }
        public PermissionField? PermissionField { get; set; }
    }
}
