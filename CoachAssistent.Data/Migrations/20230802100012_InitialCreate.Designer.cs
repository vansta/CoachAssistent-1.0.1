﻿// <auto-generated />
using System;
using CoachAssistent.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoachAssistent.Data.Migrations
{
    [DbContext(typeof(CoachAssistentDbContext))]
    [Migration("20230802100012_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid?>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SegmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TrainingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("SegmentId");

                    b.HasIndex("TrainingId");

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

                    b.Property<int>("Shared")
                        .HasColumnType("int");

                    b.Property<DateTime>("VersionTS")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

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

                    b.Property<Guid?>("OriginalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("OriginalVersionTS")
                        .HasColumnType("datetime2");

                    b.Property<int>("Shared")
                        .HasColumnType("int");

                    b.Property<DateTime>("VersionTS")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

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

            modelBuilder.Entity("CoachAssistent.Models.Domain.SharablesXGroups", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SegmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TrainingId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SegmentId");

                    b.HasIndex("TrainingId");

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

                    b.Property<Guid?>("OriginalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("OriginalVersionTS")
                        .HasColumnType("datetime2");

                    b.Property<int>("Shared")
                        .HasColumnType("int");

                    b.Property<DateTime>("VersionTS")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

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

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("GroupUser");
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
                    b.HasOne("CoachAssistent.Models.Domain.Exercise", "Exercise")
                        .WithMany("Editors")
                        .HasForeignKey("ExerciseId");

                    b.HasOne("CoachAssistent.Models.Domain.Segment", "Segment")
                        .WithMany("Editors")
                        .HasForeignKey("SegmentId");

                    b.HasOne("CoachAssistent.Models.Domain.Training", "Training")
                        .WithMany("Editors")
                        .HasForeignKey("TrainingId");

                    b.HasOne("CoachAssistent.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Segment");

                    b.Navigation("Training");

                    b.Navigation("User");
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

            modelBuilder.Entity("CoachAssistent.Models.Domain.SharablesXGroups", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Exercise", "Exercise")
                        .WithMany("SharablesXGroups")
                        .HasForeignKey("ExerciseId");

                    b.HasOne("CoachAssistent.Models.Domain.Group", "Group")
                        .WithMany("SharablesXGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.Segment", "Segment")
                        .WithMany("SharablesXGroups")
                        .HasForeignKey("SegmentId");

                    b.HasOne("CoachAssistent.Models.Domain.Training", "Training")
                        .WithMany("SharablesXGroups")
                        .HasForeignKey("TrainingId");

                    b.Navigation("Exercise");

                    b.Navigation("Group");

                    b.Navigation("Segment");

                    b.Navigation("Training");
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

            modelBuilder.Entity("GroupUser", b =>
                {
                    b.HasOne("CoachAssistent.Models.Domain.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoachAssistent.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
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

                    b.Navigation("Editors");

                    b.Navigation("SegmentsXExercises");

                    b.Navigation("SharablesXGroups");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Group", b =>
                {
                    b.Navigation("SharablesXGroups");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Segment", b =>
                {
                    b.Navigation("Editors");

                    b.Navigation("SegmentsXExercises");

                    b.Navigation("SharablesXGroups");

                    b.Navigation("TrainingsXSegments");
                });

            modelBuilder.Entity("CoachAssistent.Models.Domain.Training", b =>
                {
                    b.Navigation("Editors");

                    b.Navigation("SharablesXGroups");

                    b.Navigation("TrainingsXSegments");
                });
#pragma warning restore 612, 618
        }
    }
}