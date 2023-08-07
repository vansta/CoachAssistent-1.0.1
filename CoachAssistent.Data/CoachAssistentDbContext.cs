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
        public DbSet<ShareablesXGroups> ShareablesXGroups => Set<ShareablesXGroups>();
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

            Guid adminId = new Guid("5E8876EE-A3F0-4714-9566-22411FAA32D4");
            Guid writerId = new Guid("4948B3BA-6061-4995-AC82-2DA4885839E5");
            Guid readerId = new Guid("AE0AFD1F-4667-41A9-BEF5-0EE9328BE9CA");
            Guid editorId = new Guid("BE0AFD1F-4667-41A9-BEF5-0EE9328BE9CA");
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
                },
                new Role
                {
                    Id = editorId,
                    Name = "Editor",
                    Description = "An editor owns an exercise, segment or training"
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
                },
                new PermissionAction
                {
                    Id = 5,
                    Name = "editShareability"
                });
            modelBuilder.Entity<PermissionSubject>()
                .HasData(new PermissionSubject
                {
                    Id = 1,
                    Name = "shareable"
                },
                new PermissionSubject
                {
                    Id = 2,
                    Name = "group"
                });

            modelBuilder.Entity<RolePermission>()
                .HasData(new RolePermission
                {
                    Id = 1,
                    RoleId = adminId,
                    ActionId = 1,
                    SubjectId = 1
                },
                new RolePermission
                {
                    Id = 2,
                    RoleId = adminId,
                    ActionId = 2,
                    SubjectId = 1
                },
                new RolePermission
                {
                    Id = 3,
                    RoleId = adminId,
                    ActionId = 3,
                    SubjectId = 1
                },
                new RolePermission
                {
                    Id = 4,
                    RoleId = adminId,
                    ActionId = 4,
                    SubjectId = 1
                },
                new RolePermission
                {
                    Id = 5,
                    RoleId = adminId,
                    ActionId = 5,
                    SubjectId = 1
                },
                //For editor
                new RolePermission
                {
                    Id = 6,
                    RoleId = editorId,
                    ActionId = 1,
                    SubjectId = 1
                },
                new RolePermission
                {
                    Id = 7,
                    RoleId = editorId,
                    ActionId = 2,
                    SubjectId = 1
                },
                new RolePermission
                {
                    Id = 8,
                    RoleId = editorId,
                    ActionId = 3,
                    SubjectId = 1
                },
                new RolePermission
                {
                    Id = 9,
                    RoleId = editorId,
                    ActionId = 4,
                    SubjectId = 1
                },
                new RolePermission
                {
                    Id = 10,
                    RoleId = editorId,
                    ActionId = 5,
                    SubjectId = 1
                });
        }
    }
}