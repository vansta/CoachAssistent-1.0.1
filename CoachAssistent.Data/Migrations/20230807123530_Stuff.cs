using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class Stuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_PermissionSubjects_SubjectId",
                table: "RolePermissions");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "RolePermissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "PermissionFields",
                columns: new[] { "Id", "Description", "Name", "SubjectId" },
                values: new object[,]
                {
                    { 1, "Name", "Name", 1 },
                    { 2, null, "Description", 1 },
                    { 3, "Name", "Name", 2 },
                    { 4, null, "Description", 2 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_PermissionSubjects_SubjectId",
                table: "RolePermissions",
                column: "SubjectId",
                principalTable: "PermissionSubjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_PermissionSubjects_SubjectId",
                table: "RolePermissions");

            migrationBuilder.DeleteData(
                table: "PermissionFields",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PermissionFields",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PermissionFields",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PermissionFields",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_PermissionSubjects_SubjectId",
                table: "RolePermissions",
                column: "SubjectId",
                principalTable: "PermissionSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
