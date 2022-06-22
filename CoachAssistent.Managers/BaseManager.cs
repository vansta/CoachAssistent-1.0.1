using AutoMapper;
using CoachAssistent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public abstract class BaseManager
    {
        internal readonly CoachAssistentDbContext dbContext;
        internal readonly IMapper mapper;
        public BaseManager(CoachAssistentDbContext context, IMapper _mapper)
        {
            dbContext = context;
            mapper = _mapper;
        }
    }
}
