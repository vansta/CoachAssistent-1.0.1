using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Exercise
{
    public class UpdateExerciseViewModel : CreateExerciseViewModel
    {
        public Guid Id { get; set; }
    }
}
