﻿// <auto-generated />
using System;
using CoachAssistent.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoachAssistent.Data.Migrations
{
    [DbContext(typeof(CoachAssistentDbContext))]
    partial class CoachAssistentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoachAssistent.Models.Domain.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Editor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShareableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShareableId");

                    b.HasIndex("UserId");

                    b.ToTable("Editors");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedTS")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("ShareableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShareableId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.HistoryLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EditActionType")
                        .HasColumnType("int");

                    b.Property<Guid?>("OriginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShareableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OriginId");

                    b.HasIndex("ShareableId");

                    b.HasIndex("UserId");

                    b.ToTable("HistoryLogs");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.PermissionAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("PermissionActions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "create"
                        },
                        new
                        {
                            Id = 2,
                            Name = "read"
                        },
                        new
                        {
                            Id = 3,
                            Name = "update"
                        },
                        new
                        {
                            Id = 4,
                            Name = "delete"
                        });
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.PermissionField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("PermissionFields");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.PermissionSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("PermissionSubjects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "exercise"
                        },
                        new
                        {
                            Id = 2,
                            Name = "segment"
                        },
                        new
                        {
                            Id = 3,
                            Name = "training"
                        },
                        new
                        {
                            Id = 4,
                            Name = "group"
                        });
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionId")
                        .HasColumnType("int");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("RoleId");

                    b.HasIndex("SubjectId");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActionId = 1,
                            RoleId = new Guid("fd452b26-fb0a-421a-8618-266f869459e9"),
                            SubjectId = 1
                        });
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.RolePermissionXPermissionField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PermissionFieldId")
                        .HasColumnType("int");

                    b.Property<int>("RolePermissionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionFieldId");

                    b.HasIndex("RolePermissionId");

                    b.ToTable("RolePermissionsXPermissionFields");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fd452b26-fb0a-421a-8618-266f869459e9"),
                            Description = "An administrator can edit everything",
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = new Guid("3b55a0a1-9d51-497b-9ba1-86119b2b79d0"),
                            Description = "A writer can edit and create",
                            Name = "Writer"
                        },
                        new
                        {
                            Id = new Guid("221175ff-8378-4844-afc3-d53151ddad05"),
                            Description = "A reader can read",
                            Name = "Reader"
                        });
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Segment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedTS")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("ShareableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShareableId");

                    b.ToTable("Segments");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.SegmentXExercise", b =>
                {
                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SegmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.HasKey("ExerciseId", "SegmentId");

                    b.HasIndex("SegmentId");

                    b.ToTable("SegmentsXExercises");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Shareable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SharingLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Shareables");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.ShareablesXGroups", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShareableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("ShareableId");

                    b.ToTable("SharablesXGroups");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Training", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DeletedTS")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid>("ShareableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShareableId");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.TrainingXSegment", b =>
                {
                    b.Property<Guid>("SegmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TrainingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.HasKey("SegmentId", "TrainingId");

                    b.HasIndex("TrainingId");

                    b.ToTable("TrainingsXSegments");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<byte[]>("Key")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("LastName")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExerciseTag", b =>
                {
                    b.Property<Guid>("ExercisesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("ExercisesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ExerciseTag");
                });

            modelBuilder.Entity("GroupTag", b =>
                {
                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("GroupsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("GroupTag");
                });

            modelBuilder.Entity("SegmentTag", b =>
                {
                    b.Property<Guid>("SegmentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("SegmentsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("SegmentTag");
                });

            modelBuilder.Entity("TagTraining", b =>
                {
                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.Property<Guid>("TrainingsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TagsId", "TrainingsId");

                    b.HasIndex("TrainingsId");

                    b.ToTable("TagTraining");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Attachment", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Exercise", "Exercise")
                        .WithMany("Attachments")
                        .HasForeignKey("ExerciseId");

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Editor", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Shareable", "Shareable")
                        .WithMany("Editors")
                        .HasForeignKey("ShareableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shareable");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Exercise", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Shareable", "Shareable")
                        .WithMany()
                        .HasForeignKey("ShareableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shareable");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.HistoryLog", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Shareable", "Origin")
                        .WithMany("Copies")
                        .HasForeignKey("OriginId");

                    b.HasOne("CoachAssistent.Models.Domain.Shareable", "Shareable")
                        .WithMany("HistoryLogs")
                        .HasForeignKey("ShareableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Origin");

                    b.Navigation("Shareable");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Member", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Group", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.User", "User")
                        .WithMany("Memberships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.PermissionField", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Permissions.PermissionSubject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.RolePermission", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Permissions.PermissionAction", "Action")
                        .WithMany()
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Role", null)
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Permissions.PermissionSubject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.RolePermissionXPermissionField", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Permissions.PermissionField", "PermissionField")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionFieldId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Permissions.RolePermission", "RolePermission")
                        .WithMany("Fields")
                        .HasForeignKey("RolePermissionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PermissionField");

                    b.Navigation("RolePermission");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Segment", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Shareable", "Shareable")
                        .WithMany()
                        .HasForeignKey("ShareableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shareable");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.SegmentXExercise", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Exercise", "Exercise")
                        .WithMany("SegmentsXExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Segment", "Segment")
                        .WithMany("SegmentsXExercises")
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Segment");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.ShareablesXGroups", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Group", "Group")
                        .WithMany("ShareablesXGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Shareable", "Shareable")
                        .WithMany("ShareablesXGroups")
                        .HasForeignKey("ShareableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Shareable");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Training", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Shareable", "Shareable")
                        .WithMany()
                        .HasForeignKey("ShareableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shareable");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.TrainingXSegment", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Segment", "Segment")
                        .WithMany("TrainingsXSegments")
                        .HasForeignKey("SegmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Training", "Training")
                        .WithMany("TrainingsXSegments")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Segment");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("ExerciseTag", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExercisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupTag", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SegmentTag", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Segment", null)
                        .WithMany()
                        .HasForeignKey("SegmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TagTraining", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Training", null)
                        .WithMany()
                        .HasForeignKey("TrainingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Exercise", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("SegmentsXExercises");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Group", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("ShareablesXGroups");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.PermissionField", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Permissions.RolePermission", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Segment", b =>
                {
                    b.Navigation("SegmentsXExercises");

                    b.Navigation("TrainingsXSegments");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Shareable", b =>
                {
                    b.Navigation("Copies");

                    b.Navigation("Editors");

                    b.Navigation("HistoryLogs");

                    b.Navigation("ShareablesXGroups");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Training", b =>
                {
                    b.Navigation("TrainingsXSegments");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.User", b =>
                {
                    b.Navigation("Memberships");
                });
#pragma warning restore 612, 618
        }
    }
}
