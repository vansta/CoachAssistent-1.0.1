using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain.Permissions
{
    public class LicensePermission
    {
        public int Id { get; set; }
        public Guid LicenseId { get; set; }
        public int ActionId { get; set; }
        public int? SubjectId { get; set; }
        [MaxLength(256)]
        public string? Reason { get; set; }

        public License? License { get; set; }
        public PermissionAction? Action { get; set; }
        public PermissionSubject? Subject { get; set; }

        public ICollection<LicensePermissionXPermissionField> Fields { get; set; } = new HashSet<LicensePermissionXPermissionField>();
    }
}
