using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain.Permissions
{
    public class PermissionAction
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; } = null!;
        [MaxLength(128)]
        public string? Description { get; set; }
    }
}
