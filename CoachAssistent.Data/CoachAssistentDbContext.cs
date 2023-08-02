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
        public DbSet<SharablesXGroups> SharablesXGroups => Set<SharablesXGroups>();
        public DbSet<SegmentXExercise> SegmentsXExercises => Set<SegmentXExercise>();
        public DbSet<TrainingXSegment> TrainingsXSegments => Set<TrainingXSegment>();
        public DbSet<Editor> Editors => Set<Editor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Segment>()
                .HasMany(e => e.Exercises)
                .WithMany(u => u.Segments)
                .UsingEntity<SegmentXExercise>();

            modelBuilder
                .Entity<SegmentXExercise>()
                .HasOne(se => se.Segment)
                .WithMany(s => s.SegmentsXExercises)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<SegmentXExercise>()
                .HasOne(se => se.Exercise)
                .WithMany(s => s.SegmentsXExercises)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Segment>()
                .HasMany(e => e.Trainings)
                .WithMany(u => u.Segments)
                .UsingEntity<TrainingXSegment>();

            modelBuilder
                .Entity<TrainingXSegment>()
                .HasOne(se => se.Segment)
                .WithMany(s => s.TrainingsXSegments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<TrainingXSegment>()
                .HasOne(se => se.Training)
                .WithMany(s => s.TrainingsXSegments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Training>()
                .HasMany(t => t.SharablesXGroups)
                .WithOne(sg => sg.Training)
                .HasForeignKey(sg => sg.SharableId);

            modelBuilder
                .Entity<Training>()
                .HasMany(t => t.Editors)
                .WithOne(sg => sg.Training)
                .HasForeignKey(sg => sg.SharableId);

            modelBuilder
                .Entity<Segment>()
                .HasMany(t => t.SharablesXGroups)
                .WithOne(sg => sg.Segment)
                .HasForeignKey(sg => sg.SharableId);

            modelBuilder
                .Entity<Segment>()
                .HasMany(t => t.Editors)
                .WithOne(sg => sg.Segment)
                .HasForeignKey(sg => sg.SharableId);

            modelBuilder
                .Entity<Exercise>()
                .HasMany(t => t.SharablesXGroups)
                .WithOne(sg => sg.Exercise)
                .HasForeignKey(sg => sg.SharableId);

            modelBuilder
                .Entity<Exercise>()
                .HasMany(t => t.Editors)
                .WithOne(sg => sg.Exercise)
                .HasForeignKey(sg => sg.SharableId);
        }
    }
}