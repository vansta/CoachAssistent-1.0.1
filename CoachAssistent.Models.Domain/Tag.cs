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
    }
}
