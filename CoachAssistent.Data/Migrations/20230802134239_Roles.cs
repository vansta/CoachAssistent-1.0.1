using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memebers_Groups_GroupId",
                table: "Memebers");

            migrationBuilder.DropForeignKey(
                name: "FK_Memebers_Role_RoleId",
                table: "Memebers");

            migrationBuilder.DropForeignKey(
                name: "FK_Memebers_Users_UserId",
                table: "Memebers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memebers",
                table: "Memebers");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Memebers",
                newName: "Members");

            migrationBuilder.RenameIndex(
                name: "IX_Memebers_UserId",
                table: "Members",
                newName: "IX_Members_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Memebers_RoleId",
                table: "Members",
                newName: "IX_Members_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Memebers_GroupId",
                table: "Members",
                newName: "IX_Members_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1816bb8a-3077-40ed-9df8-88f93e36098d"), "A writer can edit and create", "Writer" },
                    { new Guid("942eed55-872d-4c54-9f79-a27b1dcfe1ae"), "A reader can read", "Reader" },
                    { new Guid("9ba4fb32-98fb-40f7-ab4a-2341de19a1f3"), "An administrator can edit everything", "Administrator" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Groups_GroupId",
                table: "Members",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Roles_RoleId",
                table: "Members",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Users_UserId",
                table: "Members",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Groups_GroupId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Roles_RoleId",
                table: "Members");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Users_UserId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

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

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Memebers");

            migrationBuilder.RenameIndex(
                name: "IX_Members_UserId",
                table: "Memebers",
                newName: "IX_Memebers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_RoleId",
                table: "Memebers",
                newName: "IX_Memebers_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Members_GroupId",
                table: "Memebers",
                newName: "IX_Memebers_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memebers",
                table: "Memebers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Memebers_Groups_GroupId",
                table: "Memebers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memebers_Role_RoleId",
                table: "Memebers",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memebers_Users_UserId",
                table: "Memebers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
