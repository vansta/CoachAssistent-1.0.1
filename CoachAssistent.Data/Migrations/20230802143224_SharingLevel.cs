using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class SharingLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1816bb8a-3077-40ed-9df8-88f93e36098d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("942eed55-872d-4c54-9f79-a27b1dcfe1ae"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9ba4fb32-98fb-40f7-ab4a-2341de19a1f3"));

            migrationBuilder.RenameColumn(
                name: "Shared",
                table: "Trainings",
                newName: "SharingLevel");

            migrationBuilder.RenameColumn(
                name: "Shared",
                table: "Segments",
                newName: "SharingLevel");

            migrationBuilder.RenameColumn(
                name: "Shared",
                table: "Exercises",
                newName: "SharingLevel");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "SharingLevel",
                table: "Trainings",
                newName: "Shared");

            migrationBuilder.RenameColumn(
                name: "SharingLevel",
                table: "Segments",
                newName: "Shared");

            migrationBuilder.RenameColumn(
                name: "SharingLevel",
                table: "Exercises",
                newName: "Shared");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1816bb8a-3077-40ed-9df8-88f93e36098d"), "A writer can edit and create", "Writer" },
                    { new Guid("942eed55-872d-4c54-9f79-a27b1dcfe1ae"), "A reader can read", "Reader" },
                    { new Guid("9ba4fb32-98fb-40f7-ab4a-2341de19a1f3"), "An administrator can edit everything", "Administrator" }
                });
        }
    }
}
