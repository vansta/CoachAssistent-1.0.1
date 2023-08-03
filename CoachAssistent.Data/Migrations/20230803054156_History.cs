using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class History : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("28697854-aa55-480f-a76f-0816fb5cb7fd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("54ef886e-ea8d-407c-b597-f12fda9f4c8f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d5682d78-45e9-41a9-9eb9-9b6d8c315eee"));

            migrationBuilder.DropColumn(
                name: "OriginalId",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "OriginalVersionTS",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "VersionTS",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "VersionTS",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
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

            migrationBuilder.CreateTable(
                name: "HistoryLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditActionType = table.Column<int>(type: "int", nullable: false),
                    OriginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HistoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryLogs_Histories_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("4e5a8816-a6f9-4761-9ccb-48fd42bc45fe"), "A writer can edit and create", "Writer" },
                    { new Guid("60a167f2-537a-44b0-a97c-99b7e0bdf2ec"), "A reader can read", "Reader" },
                    { new Guid("d9925949-982f-4c65-9092-8dd3dba8f90a"), "An administrator can edit everything", "Administrator" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_HistoryId",
                table: "Trainings",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_HistoryId",
                table: "Segments",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_HistoryId",
                table: "Exercises",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLogs_HistoryId",
                table: "HistoryLogs",
                column: "HistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Histories_HistoryId",
                table: "Exercises",
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
                name: "FK_Trainings_Histories_HistoryId",
                table: "Trainings",
                column: "HistoryId",
                principalTable: "Histories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Histories_HistoryId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Segments_Histories_HistoryId",
                table: "Segments");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Histories_HistoryId",
                table: "Trainings");

            migrationBuilder.DropTable(
                name: "HistoryLogs");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_HistoryId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Segments_HistoryId",
                table: "Segments");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_HistoryId",
                table: "Exercises");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4e5a8816-a6f9-4761-9ccb-48fd42bc45fe"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("60a167f2-537a-44b0-a97c-99b7e0bdf2ec"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d9925949-982f-4c65-9092-8dd3dba8f90a"));

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Exercises");

            migrationBuilder.AddColumn<Guid>(
                name: "OriginalId",
                table: "Segments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OriginalVersionTS",
                table: "Segments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VersionTS",
                table: "Segments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "VersionTS",
                table: "Exercises",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("28697854-aa55-480f-a76f-0816fb5cb7fd"), "A reader can read", "Reader" },
                    { new Guid("54ef886e-ea8d-407c-b597-f12fda9f4c8f"), "An administrator can edit everything", "Administrator" },
                    { new Guid("d5682d78-45e9-41a9-9eb9-9b6d8c315eee"), "A writer can edit and create", "Writer" }
                });
        }
    }
}
