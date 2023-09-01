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
        public Guid ShareableId { get; set; }
        public string SharingLevel { get; set; } = null!;
        public IEnumerable<Guid> GroupIds { get; set; } = new List<Guid>();
        public IEnumerable<Guid> EditorIds { get; set; } = new List<Guid>();
        public bool IsFavorite { get; set; }
    }
}
