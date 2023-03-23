using CoachAssistent.Common.Enums;
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
            AddedAttachments = new HashSet<IFormFile>();
            Tags = new HashSet<string>();
        }
        public string Name { get; set; }
        public string? Description { get; set; }

        public SharingLevel Shared { get; set; }

        public ICollection<IFormFile> AddedAttachments { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}
