using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachAssistent.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shared = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalVersion = table.Column<int>(type: "int", nullable: false),
                    DeletedTS = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Exercises_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Exercises_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GroupUser_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shared = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalVersion = table.Column<int>(type: "int", nullable: false),
                    DeletedTS = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Shared = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalVersion = table.Column<int>(type: "int", nullable: false),
                    DeletedTS = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "SharablesXGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrainingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharablesXGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrainingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_ExerciseId",
                table: "Attachments",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_OriginalId",
                table: "Exercises",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_UserId",
                table: "Exercises",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UsersId",
                table: "GroupUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_UserId",
                table: "Segments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentsXExercises_SegmentId",
                table: "SegmentsXExercises",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_ExerciseId",
                table: "SharablesXGroups",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_GroupId",
                table: "SharablesXGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_SegmentId",
                table: "SharablesXGroups",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_TrainingId",
                table: "SharablesXGroups",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ExerciseId",
                table: "Tags",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_GroupId",
                table: "Tags",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SegmentId",
                table: "Tags",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TrainingId",
                table: "Tags",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_UserId",
                table: "Trainings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsXSegments_TrainingId",
                table: "TrainingsXSegments",
                column: "TrainingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropTable(
                name: "SegmentsXExercises");

            migrationBuilder.DropTable(
                name: "SharablesXGroups");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "TrainingsXSegments");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
