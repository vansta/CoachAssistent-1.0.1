using CoachAssistent.Models.Domain.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class License
    {
        public Guid Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; } = null!;
        [MaxLength(1024)]
        public string? Description { get; set; }
        public int Level { get; set; }

        public ICollection<LicensePermission> LicensePermissions { get; set; } = new HashSet<LicensePermission>();
    }
}
