using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class Sharebles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Histories_HistoryId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryLogs_Histories_HistoryId",
                table: "HistoryLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Segments_Histories_HistoryId",
                table: "Segments");

            migrationBuilder.DropForeignKey(
                name: "FK_SharablesXGroups_Exercises_ExerciseId",
                table: "SharablesXGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SharablesXGroups_Segments_SegmentId",
                table: "SharablesXGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SharablesXGroups_Trainings_TrainingId",
                table: "SharablesXGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Histories_HistoryId",
                table: "Trainings");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_HistoryId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_SharablesXGroups_ExerciseId",
                table: "SharablesXGroups");

            migrationBuilder.DropIndex(
                name: "IX_SharablesXGroups_SegmentId",
                table: "SharablesXGroups");

            migrationBuilder.DropIndex(
                name: "IX_SharablesXGroups_TrainingId",
                table: "SharablesXGroups");

            migrationBuilder.DropIndex(
                name: "IX_Segments_HistoryId",
                table: "Segments");

            migrationBuilder.DropIndex(
                name: "IX_HistoryLogs_HistoryId",
                table: "HistoryLogs");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_HistoryId",
                table: "Exercises");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("454aaf92-42c4-45a8-b8a7-bedf3ae134e7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e6bc65a4-e2fe-416c-af36-2a8996694ed0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eed85c00-84aa-48f7-ad73-d8f17fc8007a"));

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "OriginalId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "OriginalVersionTS",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "SharingLevel",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "VersionTS",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "SharablesXGroups");

            migrationBuilder.DropColumn(
                name: "SegmentId",
                table: "SharablesXGroups");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "SharablesXGroups");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "SharingLevel",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "SharingLevel",
                table: "Exercises");

            migrationBuilder.AddColumn<Guid>(
                name: "SharebleId",
                table: "Trainings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SharebleId",
                table: "SharablesXGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SharebleId",
                table: "Segments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SharebleId",
                table: "HistoryLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SharebleId",
                table: "Exercises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SharebleId",
                table: "Editors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sharebles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharingLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sharebles", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: new Guid("bc435042-5a53-4a75-97be-32a102ec23e8"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("496360b4-75be-4f56-8fd6-24ad1121f4fa"), "A reader can read", "Reader" },
                    { new Guid("bc435042-5a53-4a75-97be-32a102ec23e8"), "An administrator can edit everything", "Administrator" },
                    { new Guid("ee443c8f-8971-45cb-b8f5-4761ab3853ea"), "A writer can edit and create", "Writer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_SharebleId",
                table: "Trainings",
                column: "SharebleId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_SharebleId",
                table: "SharablesXGroups",
                column: "SharebleId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_SharebleId",
                table: "Segments",
                column: "SharebleId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLogs_SharebleId",
                table: "HistoryLogs",
                column: "SharebleId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_SharebleId",
                table: "Exercises",
                column: "SharebleId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_SharebleId",
                table: "Editors",
                column: "SharebleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Sharebles_SharebleId",
                table: "Editors",
                column: "SharebleId",
                principalTable: "Sharebles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Sharebles_SharebleId",
                table: "Exercises",
                column: "SharebleId",
                principalTable: "Sharebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryLogs_Sharebles_SharebleId",
                table: "HistoryLogs",
                column: "SharebleId",
                principalTable: "Sharebles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Segments_Sharebles_SharebleId",
                table: "Segments",
                column: "SharebleId",
                principalTable: "Sharebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharablesXGroups_Sharebles_SharebleId",
                table: "SharablesXGroups",
                column: "SharebleId",
                principalTable: "Sharebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Sharebles_SharebleId",
                table: "Trainings",
                column: "SharebleId",
                principalTable: "Sharebles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editors_Sharebles_SharebleId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Sharebles_SharebleId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryLogs_Sharebles_SharebleId",
                table: "HistoryLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Segments_Sharebles_SharebleId",
                table: "Segments");

            migrationBuilder.DropForeignKey(
                name: "FK_SharablesXGroups_Sharebles_SharebleId",
                table: "SharablesXGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Sharebles_SharebleId",
                table: "Trainings");

            migrationBuilder.DropTable(
                name: "Sharebles");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_SharebleId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_SharablesXGroups_SharebleId",
                table: "SharablesXGroups");

            migrationBuilder.DropIndex(
                name: "IX_Segments_SharebleId",
                table: "Segments");

            migrationBuilder.DropIndex(
                name: "IX_HistoryLogs_SharebleId",
                table: "HistoryLogs");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_SharebleId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Editors_SharebleId",
                table: "Editors");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("496360b4-75be-4f56-8fd6-24ad1121f4fa"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bc435042-5a53-4a75-97be-32a102ec23e8"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ee443c8f-8971-45cb-b8f5-4761ab3853ea"));

            migrationBuilder.DropColumn(
                name: "SharebleId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "SharebleId",
                table: "SharablesXGroups");

            migrationBuilder.DropColumn(
                name: "SharebleId",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "SharebleId",
                table: "HistoryLogs");

            migrationBuilder.DropColumn(
                name: "SharebleId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "SharebleId",
                table: "Editors");

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OriginalId",
                table: "Trainings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OriginalVersionTS",
                table: "Trainings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SharingLevel",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "VersionTS",
                table: "Trainings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "SharablesXGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SegmentId",
                table: "SharablesXGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
                table: "SharablesXGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "Segments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SharingLevel",
                table: "Segments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SharingLevel",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: new Guid("454aaf92-42c4-45a8-b8a7-bedf3ae134e7"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("454aaf92-42c4-45a8-b8a7-bedf3ae134e7"), "An administrator can edit everything", "Administrator" },
                    { new Guid("e6bc65a4-e2fe-416c-af36-2a8996694ed0"), "A reader can read", "Reader" },
                    { new Guid("eed85c00-84aa-48f7-ad73-d8f17fc8007a"), "A writer can edit and create", "Writer" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_HistoryId",
                table: "Trainings",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_ExerciseId",
                table: "SharablesXGroups",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_SegmentId",
                table: "SharablesXGroups",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_TrainingId",
                table: "SharablesXGroups",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_HistoryId",
                table: "Segments",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLogs_HistoryId",
                table: "HistoryLogs",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_HistoryId",
                table: "Exercises",
                column: "HistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Histories_HistoryId",
                table: "Exercises",
                column: "HistoryId",
                principalTable: "Histories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryLogs_Histories_HistoryId",
                table: "HistoryLogs",
                column: "HistoryId",
                principalTable: "Histories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Segments_Histories_HistoryId",
                table: "Segments",
                column: "HistoryId",
                principalTable: "Histories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharablesXGroups_Exercises_ExerciseId",
                table: "SharablesXGroups",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharablesXGroups_Segments_SegmentId",
                table: "SharablesXGroups",
                column: "SegmentId",
                principalTable: "Segments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharablesXGroups_Trainings_TrainingId",
                table: "SharablesXGroups",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Histories_HistoryId",
                table: "Trainings",
                column: "HistoryId",
                principalTable: "Histories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
