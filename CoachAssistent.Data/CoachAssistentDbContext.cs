using CoachAssistent.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoachAssistent.Data
{
    public class CoachAssistentDbContext : DbContext
    {
        public CoachAssistentDbContext(DbContextOptions<CoachAssistentDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<Exercise> Exercises => Set<Exercise>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Segment> Segments => Set<Segment>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Training> Trainings => Set<Training>();
        public DbSet<User> Users => Set<User>();
    }
}