using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class MembershipRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MembershipRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipRequests_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembershipRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LicensePermissions",
                columns: new[] { "Id", "ActionId", "LicenseId", "Reason", "SubjectId" },
                values: new object[,]
                {
                    { 101, 2, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 1 },
                    { 102, 3, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 1 },
                    { 103, 4, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 1 },
                    { 110, 1, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 2 },
                    { 120, 1, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 3 },
                    { 121, 2, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 3 },
                    { 122, 3, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 3 },
                    { 123, 4, new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), null, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipRequests_GroupId",
                table: "MembershipRequests",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipRequests_UserId",
                table: "MembershipRequests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembershipRequests");

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "LicensePermissions",
                keyColumn: "Id",
                keyValue: 123);
        }
    }
}
