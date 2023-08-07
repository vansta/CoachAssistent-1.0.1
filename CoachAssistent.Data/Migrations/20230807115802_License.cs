using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class License : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LicenseId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimalLicenseLevel",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "RolePermissions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Licenses",
                columns: new[] { "Id", "Description", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("ad48ebbb-ae91-457f-b108-6b86d45ad02c"), "Default license", 1, "Free user" },
                    { new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), "Hands off!", 99, "Administrator" }
                });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "Reason",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4948b3ba-6061-4995-ac82-2da4885839e5"),
                columns: new[] { "Index", "MinimalLicenseLevel" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"),
                columns: new[] { "Index", "MinimalLicenseLevel" },
                values: new object[] { 0, 1 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ae0afd1f-4667-41a9-bef5-0ee9328be9ca"),
                columns: new[] { "Index", "MinimalLicenseLevel" },
                values: new object[] { 3, 1 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("be0afd1f-4667-41a9-bef5-0ee9328be9ca"),
                columns: new[] { "Index", "MinimalLicenseLevel" },
                values: new object[] { 99, 99 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LicenseId",
                table: "Users",
                column: "LicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Licenses_LicenseId",
                table: "Users",
                column: "LicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Licenses_LicenseId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropIndex(
                name: "IX_Users_LicenseId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "MinimalLicenseLevel",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "RolePermissions");
        }
    }
}
