using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Models.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class AttachmentManager : BaseManager
    {
        readonly IConfiguration configuration;
        public AttachmentManager(CoachAssistentDbContext context, IMapper mapper, IConfiguration configuration)
            : base(context, mapper)
        {
            this.configuration = configuration;
        }

        public async Task<byte[]> GetAttachment(Guid id)
        {
            Attachment? attachment = await dbContext.Attachments.FindAsync(id);
            if (attachment != null)
            {
                string path = Path.Combine(configuration["AttachmentFolder"], attachment.ExerciseId?.ToString(), attachment.FilePath);
                return File.ReadAllBytes(path);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
