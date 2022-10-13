using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Exercise
{
    public class UpdateExerciseViewModel : CreateExerciseViewModel
    {
        public UpdateExerciseViewModel()
        {
            SelectedAttachments = new HashSet<Guid>();
        }
        public Guid Id { get; set; }
        public IEnumerable<Guid> SelectedAttachments { get; set; }
    }
}
