using CoachAssistent.Models.Domain;
using CoachAssistent.Models.Domain.Permissions;
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
        public DbSet<ShareablesXGroups> SharablesXGroups => Set<ShareablesXGroups>();
        public DbSet<SegmentXExercise> SegmentsXExercises => Set<SegmentXExercise>();
        public DbSet<TrainingXSegment> TrainingsXSegments => Set<TrainingXSegment>();
        public DbSet<Editor> Editors => Set<Editor>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<HistoryLog> HistoryLogs => Set<HistoryLog>();
        public DbSet<Shareable> Shareables => Set<Shareable>();
        public DbSet<PermissionAction> PermissionActions => Set<PermissionAction>();
        public DbSet<PermissionSubject> PermissionSubjects => Set<PermissionSubject>();
        public DbSet<PermissionField> PermissionFields => Set<PermissionField>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<RolePermissionXPermissionField> RolePermissionsXPermissionFields => Set<RolePermissionXPermissionField>();

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
                .Entity<RolePermissionXPermissionField>()
                .HasOne(se => se.RolePermission)
                .WithMany(s => s.Fields)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<RolePermissionXPermissionField>()
                .HasOne(se => se.PermissionField)
                .WithMany(s => s.RolePermissions)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<HistoryLog>()
                .HasOne(hl => hl.Origin)
                .WithMany(s => s.Copies);

            Guid adminId = Guid.NewGuid();
            Guid writerId = Guid.NewGuid();
            Guid readerId = Guid.NewGuid();
            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = adminId,
                    Name = "Administrator",
                    Description = "An administrator can edit everything"
                },
                new Role
                {
                    Id = writerId,
                    Name = "Writer",
                    Description = "A writer can edit and create"
                },
                new Role
                {
                    Id = readerId,
                    Name = "Reader",
                    Description = "A reader can read"
                });
            modelBuilder.Entity<PermissionAction>()
                .HasData(new PermissionAction
                {
                    Id = 1,
                    Name = "create"
                }, new PermissionAction
                {
                    Id = 2,
                    Name = "read"
                },
                new PermissionAction
                {
                    Id = 3,
                    Name = "update"
                },
                new PermissionAction
                {
                    Id = 4,
                    Name = "delete"
                });
            modelBuilder.Entity<PermissionSubject>()
                .HasData(new PermissionSubject
                {
                    Id = 1,
                    Name = "exercise"
                },
                new PermissionSubject
                {
                    Id = 2,
                    Name = "segment"
                },
                new PermissionSubject
                {
                    Id = 3,
                    Name = "training"
                },
                new PermissionSubject
                {
                    Id = 4,
                    Name = "group"
                });

            modelBuilder.Entity<RolePermission>()
                .HasData(new RolePermission
                {
                    Id = 1,
                    RoleId = adminId,
                    ActionId = 1,
                    SubjectId = 1
                });
        }
    }
}