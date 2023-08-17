using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Permission
{
    public class PermissionSubjectSelectViewModel : SelectViewModel
    {
        public PermissionSubjectSelectViewModel(int id, string? title) : base(id, title)
        {

        }
        public IEnumerable<SelectViewModel> Fields { get; set; } = new List<SelectViewModel>();
    }
}
