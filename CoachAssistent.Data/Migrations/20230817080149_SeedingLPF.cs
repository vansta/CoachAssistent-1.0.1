using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingLPF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LicensePermissionXPermissionFields",
                columns: new[] { "Id", "LicensePermissionId", "PermissionFieldId" },
                values: new object[,]
                {
                    { 101, 120, 50 },
                    { 102, 120, 51 },
                    { 103, 120, 52 },
                    { 104, 121, 50 },
                    { 105, 121, 51 },
                    { 106, 121, 52 },
                    { 107, 122, 50 },
                    { 108, 122, 51 },
                    { 109, 122, 52 },
                    { 110, 123, 50 },
                    { 111, 123, 51 },
                    { 112, 123, 52 }
                });

            migrationBuilder.InsertData(
                table: "PermissionFields",
                columns: new[] { "Id", "Description", "Name", "SubjectId" },
                values: new object[] { 33, null, "subgroup", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "LicensePermissionXPermissionFields",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "PermissionFields",
                keyColumn: "Id",
                keyValue: 33);
        }
    }
}
