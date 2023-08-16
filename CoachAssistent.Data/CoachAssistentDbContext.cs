﻿using CoachAssistent.Models.Domain;
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
        public DbSet<License> Licenses => Set<License>();
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
        public DbSet<LicensePermission> LicensePermissions => Set<LicensePermission>();
        public DbSet<LicensePermissionXPermissionField> LicensePermissionXPermissionFields => Set<LicensePermissionXPermissionField>();


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

            Guid adminLicenseId = new("c7b16ea3-26db-4ad0-a66d-697e453d8b0c");
            Guid defaultLicenseId = new("ad48ebbb-ae91-457f-b108-6b86d45ad02c");

            modelBuilder.Entity<License>()
                .HasData(new License
                {
                    Id = adminLicenseId,
                    Name = "Administrator",
                    Description = "Hands off!",
                    Level = 99
                },
                new License
                {
                    Id = defaultLicenseId,
                    Name = "Free user",
                    Description = "Default license",
                    Level = 1
                });

            Guid adminId = new("5E8876EE-A3F0-4714-9566-22411FAA32D4");
            Guid writerId = new("4948B3BA-6061-4995-AC82-2DA4885839E5");
            Guid readerId = new("AE0AFD1F-4667-41A9-BEF5-0EE9328BE9CA");
            //Guid editorId = new Guid("BE0AFD1F-4667-41A9-BEF5-0EE9328BE9CA");
            //Guid defaultId = new Guid("CE0AFD1F-4667-41A9-BEF5-0EE9328BE9CA");
            modelBuilder.Entity<Role>()
                .HasData(new Role
                {
                    Id = adminId,
                    Name = "Administrator",
                    Description = "An administrator can edit everything",
                    Index = 0,
                    MinimalLicenseLevel = 1
                },
                new Role
                {
                    Id = writerId,
                    Name = "Writer",
                    Description = "A writer can edit and create",
                    Index = 2,
                    MinimalLicenseLevel = 1
                },
                new Role
                {
                    Id = readerId,
                    Name = "Reader",
                    Description = "A reader can read",
                    Index = 3,
                    MinimalLicenseLevel = 1
                }
                //new Role
                //{
                //    Id = editorId,
                //    Name = "Editor",
                //    Description = "An editor owns an exercise, segment or training",
                //    Index = 99,
                //    MinimalLicenseLevel = 99
                //},
                //new Role
                //{
                //    Id = defaultId,
                //    Name = "Default user",
                //    Description = "A default role, allowing basic functionality",
                //    Index = 10,
                //    MinimalLicenseLevel = 99
                //}
                );
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
                    Name = "shareable"
                },
                new PermissionSubject
                {
                    Id = 2,
                    Name = "group"
                },
                new PermissionSubject
                {
                    Id = 3,
                    Name = "role"
                });

            List<PermissionField> permissionFields = new List<PermissionField>
            {
                //shareable
                new PermissionField
                {
                    Id = 1,
                    Name = "name",
                    Description = "Name",
                    SubjectId = 1
                },
                new PermissionField
                {
                    Id = 2,
                    Name = "description",
                    SubjectId = 1
                },
                new PermissionField
                {
                    Id = 3,
                    Name = "attachments",
                    SubjectId = 1
                },
                new PermissionField
                {
                    Id = 4,
                    Name = "shareability",
                    SubjectId = 1
                },
                //group
                new PermissionField
                {
                    Id = 30,
                    Name = "name",
                    Description = "Name",
                    SubjectId = 2
                },
                new PermissionField
                {
                    Id = 31,
                    Name = "description",
                    SubjectId = 2
                },
                new PermissionField
                {
                    Id = 32,
                    Name = "member",
                    SubjectId = 2
                },
                //role
                new PermissionField
                {
                    Id = 50,
                    Name = "name",
                    Description = "Name",
                    SubjectId = 3
                },
                new PermissionField
                {
                    Id = 51,
                    Name = "description",
                    SubjectId = 3
                },
                new PermissionField
                {
                    Id = 52,
                    Name = "permissions",
                    SubjectId = 3
                }
            };

            modelBuilder.Entity<PermissionField>()
                .HasData(permissionFields);

            modelBuilder.Entity<RolePermission>()
                .HasData(
                //admin
                new RolePermission
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
                });

            modelBuilder.Entity<RolePermissionXPermissionField>()
                .HasData(new RolePermissionXPermissionField
                {
                    Id = 1,
                    PermissionFieldId = 1,
                    RolePermissionId = 1
                },
                new RolePermissionXPermissionField
                {
                    Id = 2,
                    PermissionFieldId = 2,
                    RolePermissionId = 1
                },
                new RolePermissionXPermissionField
                {
                    Id = 3,
                    PermissionFieldId = 3,
                    RolePermissionId = 1
                },
                new RolePermissionXPermissionField
                {
                    Id = 4,
                    PermissionFieldId = 4,
                    RolePermissionId = 1
                });

            modelBuilder.Entity<LicensePermission>()
                .HasData(
                //For editor
                new LicensePermission
                {
                    Id = 1,
                    LicenseId = defaultLicenseId,
                    ActionId = 2,
                    SubjectId = 1
                },
                new LicensePermission
                {
                    Id = 2,
                    LicenseId = defaultLicenseId,
                    ActionId = 3,
                    SubjectId = 1
                },
                new LicensePermission
                {
                    Id = 3,
                    LicenseId = defaultLicenseId,
                    ActionId = 4,
                    SubjectId = 1
                },
                new LicensePermission
                {
                    Id = 10,
                    LicenseId = defaultLicenseId,
                    ActionId = 1,
                    SubjectId = 2
                },

                new LicensePermission
                {
                    Id = 101,
                    LicenseId = adminLicenseId,
                    ActionId = 2,
                    SubjectId = 1
                },
                new LicensePermission
                {
                    Id = 102,
                    LicenseId = adminLicenseId,
                    ActionId = 3,
                    SubjectId = 1
                },
                new LicensePermission
                {
                    Id = 103,
                    LicenseId = adminLicenseId,
                    ActionId = 4,
                    SubjectId = 1
                },
                new LicensePermission
                {
                    Id = 110,
                    LicenseId = adminLicenseId,
                    ActionId = 1,
                    SubjectId = 2
                },
                new LicensePermission
                {
                    Id = 120,
                    LicenseId = adminLicenseId,
                    ActionId = 1,
                    SubjectId = 3
                },
                new LicensePermission
                {
                    Id = 121,
                    LicenseId = adminLicenseId,
                    ActionId = 2,
                    SubjectId = 3
                },
                new LicensePermission
                {
                    Id = 122,
                    LicenseId = adminLicenseId,
                    ActionId = 3,
                    SubjectId = 3
                },
                new LicensePermission
                {
                    Id = 123,
                    LicenseId = adminLicenseId,
                    ActionId = 4,
                    SubjectId = 3
                });
        }
    }
}