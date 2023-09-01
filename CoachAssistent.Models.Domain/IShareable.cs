using CoachAssistent.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Models.Domain
{
    public interface IShareable
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public DateTime? DeletedTS { get; set; }
        public Guid ShareableId { get; set; }
        public Shareable? Shareable { get; set; }
    }
}
