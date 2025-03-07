using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public class Tag
    {
        public Tag()
        {
            Name = string.Empty;
        }
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }

        public ICollection<Group> Groups { get; set; } = new HashSet<Group>();
        public ICollection<Segment> Segments { get; set; } = new HashSet<Segment>();
        public ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
        public ICollection<Training> Trainings { get; set; } = new HashSet<Training>();
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
