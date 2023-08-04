using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionSubjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PermissionSubjects",
                keyColumn: "Id",
                keyValue: 4);

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

            migrationBuilder.UpdateData(
                table: "PermissionSubjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "shareable");

            migrationBuilder.UpdateData(
                table: "PermissionSubjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "group");

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("4948b3ba-6061-4995-ac82-2da4885839e5"), "A writer can edit and create", "Writer" },
                    { new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), "An administrator can edit everything", "Administrator" },
                    { new Guid("ae0afd1f-4667-41a9-bef5-0ee9328be9ca"), "A reader can read", "Reader" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "ActionId", "RoleId", "SubjectId" },
                values: new object[,]
                {
                    { 2, 2, new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), 1 },
                    { 3, 3, new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), 1 },
                    { 4, 4, new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4948b3ba-6061-4995-ac82-2da4885839e5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ae0afd1f-4667-41a9-bef5-0ee9328be9ca"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"));

            migrationBuilder.UpdateData(
                table: "PermissionSubjects",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "exercise");

            migrationBuilder.UpdateData(
                table: "PermissionSubjects",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "segment");

            migrationBuilder.InsertData(
                table: "PermissionSubjects",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 3, null, "training" },
                    { 4, null, "group" }
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
        }
    }
}
