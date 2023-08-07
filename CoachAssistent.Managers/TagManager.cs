using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class TagManager : BaseManager
    {
        public TagManager(CoachAssistentDbContext context, IMapper mapper) : base (context, mapper)
        {

        }

        public IEnumerable<string> GetTags(string? search)
        {
            IQueryable<Tag> tags = dbContext.Tags;
            if (!string.IsNullOrEmpty(search))
            {
                tags = tags.Where(t => t.Name.Contains(search));
            }
            return tags
                .Select(t => t.Name);   
        }
    }
}
