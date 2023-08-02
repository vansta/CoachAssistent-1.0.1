using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class UserManager : BaseAuthenticatedManager
    {
        readonly IConfiguration configuration;
        public UserManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper)
            : base(context, mapper, authenticationWrapper)
        {
            this.configuration = configuration;
        }

        public IEnumerable<Guid> GetAssignedEditors(Guid id, string? type)
        {
            IQueryable<Editor> assingedEditors = dbContext.Editors;
            switch (type)
            {
                case "exercise":
                    assingedEditors = assingedEditors.Where(e => e.ExerciseId.HasValue && e.ExerciseId.Equals(id));
                    break;
                case "segment":
                    assingedEditors = assingedEditors.Where(e => e.SegmentId.HasValue && e.SegmentId.Equals(id));
                    break;
                case "training":
                    assingedEditors = assingedEditors.Where(e => e.TrainingId.HasValue && e.TrainingId.Equals(id));
                    break;
                default:
                    assingedEditors = assingedEditors.Where(e => 
                        e.ExerciseId.HasValue && e.ExerciseId.Equals(id)
                        || e.SegmentId.HasValue && e.SegmentId.Equals(id)
                        || e.TrainingId.HasValue && e.TrainingId.Equals(id)
                    );
                    break;
            };
            return assingedEditors
                .Select(e => e.UserId);
        }

        public IEnumerable<SelectViewModel> GetAvailableEditors()
        {
            return dbContext.Users.Select(u => new SelectViewModel(u.Id, u.UserName));
        }
    }
}
