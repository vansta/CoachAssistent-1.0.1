using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.ViewModels.Exercise
{
    public class CreateExerciseViewModel
    {
        public CreateExerciseViewModel()
        {
            Name = string.Empty;
            Attachments = new HashSet<IFormFile>();
            Tags = new HashSet<SelectViewModel>();
        }
        public string Name { get; set; }
        public string? Description { get; set; }

        public bool Public { get; set; }

        public ICollection<IFormFile> Attachments { get; set; }
        public ICollection<SelectViewModel> Tags { get; set; }
    }
}
