using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingLicense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissionsXPermissionFields_RolePermissions_RolePermissionId",
                table: "RolePermissionsXPermissionFields");

            migrationBuilder.InsertData(
                table: "PermissionSubjects",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 4, null, "license" });

            migrationBuilder.InsertData(
                table: "LicensePermissions",
                columns: new[] { "Id", "ActionId", "LicenseId", "Reason", "SubjectId" },
                values: new object[,]
                {
                    { 220, 1, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 4 },
                    { 221, 2, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 4 },
                    { 222, 3, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 4 },
                    { 223, 4, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 4 }
                });

            migrationBuilder.InsertData(
                table: "PermissionFields",
                columns: new[] { "Id", "Description", "Name", "SubjectId" },
                values: new object[,]
                {
                    { 60, "Name", "name", 4 },
                    { 61, null, "description", 4 },
                    { 62, null, "permissions", 4 }
                });

            migrationBuilder.InsertData(
                table: "LicensePermissionXPermissionFields",
                columns: new[] { "Id", "LicensePermissionId", "PermissionFieldId" },
                values: new object[] { 200, 222, 62 });

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissionsXPermissionFields_RolePermissions_RolePermissionId",
                table: "RolePermissionsXPermissionFields",
                column: "RolePermissionId",
                principalTable: "RolePermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissionsXPermissionFields_RolePermissions_RolePermissionId",
                table: "RolePermissionsXPermissionFields");

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "PermissionFields",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "PermissionFields",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "PermissionFields",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "PermissionSubjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissionsXPermissionFields_RolePermissions_RolePermissionId",
                table: "RolePermissionsXPermissionFields",
                column: "RolePermissionId",
                principalTable: "RolePermissions",
                principalColumn: "Id");
        }
    }
}
