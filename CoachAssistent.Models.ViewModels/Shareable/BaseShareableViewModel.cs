using CoachAssistent.Models.ViewModels.Exercise;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Shareable
{
    public class BaseShareableViewModel : BaseAbilityViewModel
    {
        public BaseShareableViewModel() : base ("shareable")
        {

        }
        public IEnumerable<Guid> GroupIds { get; set; } = new List<Guid>();
    }
}
