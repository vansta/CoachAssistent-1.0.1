using CoachAssistent.Models.ViewModels.Exercise;
using CoachAssistent.Models.ViewModels.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Role
{
    public class RoleOverviewItemViewModel : BaseAbilityViewModel
    {
        public RoleOverviewItemViewModel() : base ("role")
        {

        }
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<RolePermissionOverviewItemViewModel>? RolePermissions { get; set; }
    }
}
