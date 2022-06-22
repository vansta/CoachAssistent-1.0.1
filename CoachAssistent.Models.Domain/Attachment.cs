using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Attachment
    {
        public Attachment()
        {
            Name = string.Empty;
            FilePath = string.Empty;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }

        public Guid? ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
