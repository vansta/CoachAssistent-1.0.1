using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shared = table.Column<int>(type: "int", nullable: false),
                    VersionTS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedTS = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shared = table.Column<int>(type: "int", nullable: false),
                    VersionTS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalVersionTS = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTS = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shared = table.Column<int>(type: "int", nullable: false),
                    VersionTS = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalVersionTS = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTS = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Key = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SegmentsXExercises",
                columns: table => new
                {
                    SegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentsXExercises", x => new { x.ExerciseId, x.SegmentId });
                    table.ForeignKey(
                        name: "FK_SegmentsXExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SegmentsXExercises_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTag",
                columns: table => new
                {
                    ExercisesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTag", x => new { x.ExercisesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ExerciseTag_Exercises_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTag",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTag", x => new { x.GroupsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_GroupTag_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SegmentTag",
                columns: table => new
                {
                    SegmentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentTag", x => new { x.SegmentsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_SegmentTag_Segments_SegmentsId",
                        column: x => x.SegmentsId,
                        principalTable: "Segments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SegmentTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharablesXGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharablesXGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Exercises_SharableId",
                        column: x => x.SharableId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Segments_SharableId",
                        column: x => x.SharableId,
                        principalTable: "Segments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Trainings_SharableId",
                        column: x => x.SharableId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagTraining",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    TrainingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTraining", x => new { x.TagsId, x.TrainingsId });
                    table.ForeignKey(
                        name: "FK_TagTraining_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTraining_Trainings_TrainingsId",
                        column: x => x.TrainingsId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingsXSegments",
                columns: table => new
                {
                    TrainingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingsXSegments", x => new { x.SegmentId, x.TrainingId });
                    table.ForeignKey(
                        name: "FK_TrainingsXSegments_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrainingsXSegments_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Editors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Editors_Exercises_SharableId",
                        column: x => x.SharableId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Editors_Segments_SharableId",
                        column: x => x.SharableId,
                        principalTable: "Segments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Editors_Trainings_SharableId",
                        column: x => x.SharableId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Editors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_ExerciseId",
                table: "Attachments",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_SharableId",
                table: "Editors",
                column: "SharableId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_UserId",
                table: "Editors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTag_TagsId",
                table: "ExerciseTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTag_TagsId",
                table: "GroupTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentsXExercises_SegmentId",
                table: "SegmentsXExercises",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentTag_TagsId",
                table: "SegmentTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_GroupId",
                table: "SharablesXGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_SharableId",
                table: "SharablesXGroups",
                column: "SharableId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTraining_TrainingsId",
                table: "TagTraining",
                column: "TrainingsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsXSegments_TrainingId",
                table: "TrainingsXSegments",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupId",
                table: "Users",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Editors");

            migrationBuilder.DropTable(
                name: "ExerciseTag");

            migrationBuilder.DropTable(
                name: "GroupTag");

            migrationBuilder.DropTable(
                name: "SegmentsXExercises");

            migrationBuilder.DropTable(
                name: "SegmentTag");

            migrationBuilder.DropTable(
                name: "SharablesXGroups");

            migrationBuilder.DropTable(
                name: "TagTraining");

            migrationBuilder.DropTable(
                name: "TrainingsXSegments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
