using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Models.Domain;
using Microsoft.AspNetCore.Http;
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
            if (attachment != null && attachment.ExerciseId.HasValue)
            {
                string path = Path.Combine(configuration["AttachmentFolder"] ?? string.Empty, attachment.ExerciseId.Value.ToString(), attachment.FilePath);
                return File.ReadAllBytes(path);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        internal static Attachment CreateAttachment(string basePath, IFormFile file)
        {
            string fullPath = Path.Combine(basePath, file.FileName);
            using FileStream fileStream = File.Create(fullPath);
            file.CopyTo(fileStream);

            return new Attachment
            {
                FilePath = file.FileName,
                Name = file.Name
            };
        }

        internal static void CopyAttachment(string fromPath, string toPath, Attachment attachment)
        {
            File.Copy(Path.Combine(fromPath, attachment.FilePath), Path.Combine(toPath, attachment.FilePath));
        }

        internal static void RemoveAttachmentsRange(string basePath, IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                RemoveAttachment(basePath, fileName);
            }
        }
        internal static void RemoveAttachment(string basePath, string fileName)
        {
            string fullPath = Path.Combine(basePath, fileName);
            File.Delete(fullPath);
        }
    }
}
