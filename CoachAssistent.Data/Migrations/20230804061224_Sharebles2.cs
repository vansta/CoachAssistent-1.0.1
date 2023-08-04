using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class Sharebles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editors_Exercises_ExerciseId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Editors_Segments_SegmentId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Editors_Sharebles_SharebleId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Editors_Trainings_TrainingId",
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
                name: "IX_HistoryLogs_SharebleId",
                table: "HistoryLogs");

            migrationBuilder.DropIndex(
                name: "IX_Editors_ExerciseId",
                table: "Editors");

            migrationBuilder.DropIndex(
                name: "IX_Editors_SegmentId",
                table: "Editors");

            migrationBuilder.DropIndex(
                name: "IX_Editors_SharebleId",
                table: "Editors");

            migrationBuilder.DropIndex(
                name: "IX_Editors_TrainingId",
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
                name: "HistoryId",
                table: "HistoryLogs");

            migrationBuilder.DropColumn(
                name: "SharebleId",
                table: "HistoryLogs");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "SegmentId",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "SharebleId",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Editors");

            migrationBuilder.RenameColumn(
                name: "SharebleId",
                table: "Trainings",
                newName: "ShareableId");

            migrationBuilder.RenameIndex(
                name: "IX_Trainings_SharebleId",
                table: "Trainings",
                newName: "IX_Trainings_ShareableId");

            migrationBuilder.RenameColumn(
                name: "SharebleId",
                table: "SharablesXGroups",
                newName: "ShareableId");

            migrationBuilder.RenameIndex(
                name: "IX_SharablesXGroups_SharebleId",
                table: "SharablesXGroups",
                newName: "IX_SharablesXGroups_ShareableId");

            migrationBuilder.RenameColumn(
                name: "SharebleId",
                table: "Segments",
                newName: "ShareableId");

            migrationBuilder.RenameIndex(
                name: "IX_Segments_SharebleId",
                table: "Segments",
                newName: "IX_Segments_ShareableId");

            migrationBuilder.RenameColumn(
                name: "SharebleId",
                table: "Exercises",
                newName: "ShareableId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_SharebleId",
                table: "Exercises",
                newName: "IX_Exercises_ShareableId");

            migrationBuilder.AddColumn<Guid>(
                name: "ShareableId",
                table: "HistoryLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ShareableId",
                table: "Editors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Shareables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharingLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shareables", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: new Guid("fd452b26-fb0a-421a-8618-266f869459e9"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("221175ff-8378-4844-afc3-d53151ddad05"), "A reader can read", "Reader" },
                    { new Guid("3b55a0a1-9d51-497b-9ba1-86119b2b79d0"), "A writer can edit and create", "Writer" },
                    { new Guid("fd452b26-fb0a-421a-8618-266f869459e9"), "An administrator can edit everything", "Administrator" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLogs_OriginId",
                table: "HistoryLogs",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLogs_ShareableId",
                table: "HistoryLogs",
                column: "ShareableId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLogs_UserId",
                table: "HistoryLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_ShareableId",
                table: "Editors",
                column: "ShareableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Shareables_ShareableId",
                table: "Editors",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Shareables_ShareableId",
                table: "Exercises",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryLogs_Shareables_OriginId",
                table: "HistoryLogs",
                column: "OriginId",
                principalTable: "Shareables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryLogs_Shareables_ShareableId",
                table: "HistoryLogs",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryLogs_Users_UserId",
                table: "HistoryLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Segments_Shareables_ShareableId",
                table: "Segments",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharablesXGroups_Shareables_ShareableId",
                table: "SharablesXGroups",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Shareables_ShareableId",
                table: "Trainings",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editors_Shareables_ShareableId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Shareables_ShareableId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryLogs_Shareables_OriginId",
                table: "HistoryLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryLogs_Shareables_ShareableId",
                table: "HistoryLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryLogs_Users_UserId",
                table: "HistoryLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Segments_Shareables_ShareableId",
                table: "Segments");

            migrationBuilder.DropForeignKey(
                name: "FK_SharablesXGroups_Shareables_ShareableId",
                table: "SharablesXGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Shareables_ShareableId",
                table: "Trainings");

            migrationBuilder.DropTable(
                name: "Shareables");

            migrationBuilder.DropIndex(
                name: "IX_HistoryLogs_OriginId",
                table: "HistoryLogs");

            migrationBuilder.DropIndex(
                name: "IX_HistoryLogs_ShareableId",
                table: "HistoryLogs");

            migrationBuilder.DropIndex(
                name: "IX_HistoryLogs_UserId",
                table: "HistoryLogs");

            migrationBuilder.DropIndex(
                name: "IX_Editors_ShareableId",
                table: "Editors");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("221175ff-8378-4844-afc3-d53151ddad05"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3b55a0a1-9d51-497b-9ba1-86119b2b79d0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fd452b26-fb0a-421a-8618-266f869459e9"));

            migrationBuilder.DropColumn(
                name: "ShareableId",
                table: "HistoryLogs");

            migrationBuilder.DropColumn(
                name: "ShareableId",
                table: "Editors");

            migrationBuilder.RenameColumn(
                name: "ShareableId",
                table: "Trainings",
                newName: "SharebleId");

            migrationBuilder.RenameIndex(
                name: "IX_Trainings_ShareableId",
                table: "Trainings",
                newName: "IX_Trainings_SharebleId");

            migrationBuilder.RenameColumn(
                name: "ShareableId",
                table: "SharablesXGroups",
                newName: "SharebleId");

            migrationBuilder.RenameIndex(
                name: "IX_SharablesXGroups_ShareableId",
                table: "SharablesXGroups",
                newName: "IX_SharablesXGroups_SharebleId");

            migrationBuilder.RenameColumn(
                name: "ShareableId",
                table: "Segments",
                newName: "SharebleId");

            migrationBuilder.RenameIndex(
                name: "IX_Segments_ShareableId",
                table: "Segments",
                newName: "IX_Segments_SharebleId");

            migrationBuilder.RenameColumn(
                name: "ShareableId",
                table: "Exercises",
                newName: "SharebleId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_ShareableId",
                table: "Exercises",
                newName: "IX_Exercises_SharebleId");

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "HistoryLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SharebleId",
                table: "HistoryLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "Editors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SegmentId",
                table: "Editors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SharebleId",
                table: "Editors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
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
                name: "IX_HistoryLogs_SharebleId",
                table: "HistoryLogs",
                column: "SharebleId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_ExerciseId",
                table: "Editors",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_SegmentId",
                table: "Editors",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_SharebleId",
                table: "Editors",
                column: "SharebleId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_TrainingId",
                table: "Editors",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Exercises_ExerciseId",
                table: "Editors",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Segments_SegmentId",
                table: "Editors",
                column: "SegmentId",
                principalTable: "Segments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Sharebles_SharebleId",
                table: "Editors",
                column: "SharebleId",
                principalTable: "Sharebles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Trainings_TrainingId",
                table: "Editors",
                column: "TrainingId",
                principalTable: "Trainings",
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
    }
}
