using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(128)]
        public string Name { get; set; }
        [MaxLength(512)]
        public string FilePath { get; set; }

        public Guid? ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
    }
}
