using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionActions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, null, "editShareability" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("be0afd1f-4667-41a9-bef5-0ee9328be9ca"), "An editor owns an exercise, segment or training", "Editor" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "ActionId", "RoleId", "SubjectId" },
                values: new object[,]
                {
                    { 5, 5, new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), 1 },
                    { 6, 1, new Guid("be0afd1f-4667-41a9-bef5-0ee9328be9ca"), 1 },
                    { 7, 2, new Guid("be0afd1f-4667-41a9-bef5-0ee9328be9ca"), 1 },
                    { 8, 3, new Guid("be0afd1f-4667-41a9-bef5-0ee9328be9ca"), 1 },
                    { 9, 4, new Guid("be0afd1f-4667-41a9-bef5-0ee9328be9ca"), 1 },
                    { 10, 5, new Guid("be0afd1f-4667-41a9-bef5-0ee9328be9ca"), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PermissionActions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("be0afd1f-4667-41a9-bef5-0ee9328be9ca"));
        }
    }
}
