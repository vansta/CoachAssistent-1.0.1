using CoachAssistent.Models.ViewModels.Exercise;
using CoachAssistent.Models.ViewModels.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.License
{
    public class LicenseOverviewItemViewModel() : BaseAbilityViewModel("license")
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<RolePermissionOverviewItemViewModel>? LicensePermissions { get; set; }
    }
}
