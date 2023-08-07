using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Group
{
    public class EditGroupViewModel : CreateGroupViewModel
    {
        public Guid Id { get; set; }
    }
}
