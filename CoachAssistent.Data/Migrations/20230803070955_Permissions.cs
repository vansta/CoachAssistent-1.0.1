using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class Permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "PermissionActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSubjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionFields_PermissionSubjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "PermissionSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_PermissionActions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "PermissionActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_PermissionSubjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "PermissionSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionsXPermissionFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolePermissionId = table.Column<int>(type: "int", nullable: false),
                    PermissionFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionsXPermissionFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissionsXPermissionFields_PermissionFields_PermissionFieldId",
                        column: x => x.PermissionFieldId,
                        principalTable: "PermissionFields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissionsXPermissionFields_RolePermissions_RolePermissionId",
                        column: x => x.RolePermissionId,
                        principalTable: "RolePermissions",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "PermissionActions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "create" },
                    { 2, null, "read" },
                    { 3, null, "update" },
                    { 4, null, "delete" }
                });

            migrationBuilder.InsertData(
                table: "PermissionSubjects",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "exercise" },
                    { 2, null, "segment" },
                    { 3, null, "training" },
                    { 4, null, "group" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("454aaf92-42c4-45a8-b8a7-bedf3ae134e7"), "An administrator can edit everything", "Administrator" },
                    { new Guid("e6bc65a4-e2fe-416c-af36-2a8996694ed0"), "A reader can read", "Reader" },
                    { new Guid("eed85c00-84aa-48f7-ad73-d8f17fc8007a"), "A writer can edit and create", "Writer" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "ActionId", "RoleId", "SubjectId" },
                values: new object[] { 1, 1, new Guid("454aaf92-42c4-45a8-b8a7-bedf3ae134e7"), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionFields_SubjectId",
                table: "PermissionFields",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ActionId",
                table: "RolePermissions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_SubjectId",
                table: "RolePermissions",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionsXPermissionFields_PermissionFieldId",
                table: "RolePermissionsXPermissionFields",
                column: "PermissionFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionsXPermissionFields_RolePermissionId",
                table: "RolePermissionsXPermissionFields",
                column: "RolePermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissionsXPermissionFields");

            migrationBuilder.DropTable(
                name: "PermissionFields");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "PermissionActions");

            migrationBuilder.DropTable(
                name: "PermissionSubjects");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("454aaf92-42c4-45a8-b8a7-bedf3ae134e7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e6bc65a4-e2fe-416c-af36-2a8996694ed0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eed85c00-84aa-48f7-ad73-d8f17fc8007a"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("4e5a8816-a6f9-4761-9ccb-48fd42bc45fe"), "A writer can edit and create", "Writer" },
                    { new Guid("60a167f2-537a-44b0-a97c-99b7e0bdf2ec"), "A reader can read", "Reader" },
                    { new Guid("d9925949-982f-4c65-9092-8dd3dba8f90a"), "An administrator can edit everything", "Administrator" }
                });
        }
    }
}
