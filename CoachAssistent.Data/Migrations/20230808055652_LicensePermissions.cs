using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class LicensePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editors_Exercises_ExerciseId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Editors_Segments_SegmentId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Editors_Trainings_TrainingId",
                table: "Editors");

            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropTable(
                name: "SharablesXGroups");

            migrationBuilder.DropIndex(
                name: "IX_Editors_ExerciseId",
                table: "Editors");

            migrationBuilder.DropIndex(
                name: "IX_Editors_SegmentId",
                table: "Editors");

            migrationBuilder.DropIndex(
                name: "IX_Editors_TrainingId",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "OriginalId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "OriginalVersionTS",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Shared",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "VersionTS",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "OriginalId",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "OriginalVersionTS",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "Shared",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "VersionTS",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "Shared",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "VersionTS",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "SegmentId",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "TrainingId",
                table: "Editors");

            migrationBuilder.AddColumn<Guid>(
                name: "LicenseId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ShareableId",
                table: "Trainings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ShareableId",
                table: "Segments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ShareableId",
                table: "Exercises",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ShareableId",
                table: "Editors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: false),
                    MinimalLicenseLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shareables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SharingLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shareables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LicensePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicensePermissions_Licenses_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicensePermissions_PermissionActions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "PermissionActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicensePermissions_PermissionSubjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "PermissionSubjects",
                        principalColumn: "Id");
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
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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
                    SubjectId = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditActionType = table.Column<int>(type: "int", nullable: false),
                    ShareableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryLogs_Shareables_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Shareables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoryLogs_Shareables_ShareableId",
                        column: x => x.ShareableId,
                        principalTable: "Shareables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShareablesXGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShareableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareablesXGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareablesXGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareablesXGroups_Shareables_ShareableId",
                        column: x => x.ShareableId,
                        principalTable: "Shareables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicensePermissionXPermissionFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePermissionId = table.Column<int>(type: "int", nullable: false),
                    PermissionFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicensePermissionXPermissionFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicensePermissionXPermissionFields_LicensePermissions_LicensePermissionId",
                        column: x => x.LicensePermissionId,
                        principalTable: "LicensePermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LicensePermissionXPermissionFields_PermissionFields_PermissionFieldId",
                        column: x => x.PermissionFieldId,
                        principalTable: "PermissionFields",
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
                table: "Licenses",
                columns: new[] { "Id", "Description", "Level", "Name" },
                values: new object[,]
                {
                    { new Guid("ad48ebbb-ae91-457f-b108-6b86d45ad02c"), "Default license", 1, "Free user" },
                    { new Guid("c7b16ea3-26db-4ad0-a66d-697e453d8b0c"), "Hands off!", 99, "Administrator" }
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
                    { 1, null, "shareable" },
                    { 2, null, "group" },
                    { 3, null, "role" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Index", "MinimalLicenseLevel", "Name" },
                values: new object[,]
                {
                    { new Guid("4948b3ba-6061-4995-ac82-2da4885839e5"), "A writer can edit and create", 2, 1, "Writer" },
                    { new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), "An administrator can edit everything", 0, 1, "Administrator" },
                    { new Guid("ae0afd1f-4667-41a9-bef5-0ee9328be9ca"), "A reader can read", 3, 1, "Reader" }
                });

            migrationBuilder.InsertData(
                table: "LicensePermissions",
                columns: new[] { "Id", "ActionId", "LicenseId", "Reason", "SubjectId" },
                values: new object[,]
                {
                    { 1, 2, new Guid("ad48ebbb-ae91-457f-b108-6b86d45ad02c"), null, 1 },
                    { 2, 3, new Guid("ad48ebbb-ae91-457f-b108-6b86d45ad02c"), null, 1 },
                    { 3, 4, new Guid("ad48ebbb-ae91-457f-b108-6b86d45ad02c"), null, 1 },
                    { 10, 1, new Guid("ad48ebbb-ae91-457f-b108-6b86d45ad02c"), null, 2 }
                });

            migrationBuilder.InsertData(
                table: "PermissionFields",
                columns: new[] { "Id", "Description", "Name", "SubjectId" },
                values: new object[,]
                {
                    { 1, "Name", "name", 1 },
                    { 2, null, "description", 1 },
                    { 3, null, "attachments", 1 },
                    { 4, null, "shareability", 1 },
                    { 30, "Name", "name", 2 },
                    { 31, null, "description", 2 },
                    { 32, null, "member", 2 },
                    { 50, "Name", "name", 3 },
                    { 51, null, "description", 3 },
                    { 52, null, "permissions", 3 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "ActionId", "Reason", "RoleId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1, null, new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), 1 },
                    { 2, 2, null, new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), 1 },
                    { 3, 3, null, new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), 1 },
                    { 4, 4, null, new Guid("5e8876ee-a3f0-4714-9566-22411faa32d4"), 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissionsXPermissionFields",
                columns: new[] { "Id", "PermissionFieldId", "RolePermissionId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LicenseId",
                table: "Users",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_ShareableId",
                table: "Trainings",
                column: "ShareableId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_ShareableId",
                table: "Segments",
                column: "ShareableId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ShareableId",
                table: "Exercises",
                column: "ShareableId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_ShareableId",
                table: "Editors",
                column: "ShareableId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLogs_OriginId",
                table: "HistoryLogs",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLogs_ShareableId",
                table: "HistoryLogs",
                column: "ShareableId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryLogs_UserId",
                table: "HistoryLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePermissions_ActionId",
                table: "LicensePermissions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePermissions_LicenseId",
                table: "LicensePermissions",
                column: "LicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePermissions_SubjectId",
                table: "LicensePermissions",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePermissionXPermissionFields_LicensePermissionId",
                table: "LicensePermissionXPermissionFields",
                column: "LicensePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_LicensePermissionXPermissionFields_PermissionFieldId",
                table: "LicensePermissionXPermissionFields",
                column: "PermissionFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_GroupId",
                table: "Members",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_RoleId",
                table: "Members",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_UserId",
                table: "Members",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ShareablesXGroups_GroupId",
                table: "ShareablesXGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareablesXGroups_ShareableId",
                table: "ShareablesXGroups",
                column: "ShareableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Shareables_ShareableId",
                table: "Editors",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Shareables_ShareableId",
                table: "Exercises",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Segments_Shareables_ShareableId",
                table: "Segments",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Shareables_ShareableId",
                table: "Trainings",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Editors_Shareables_ShareableId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Shareables_ShareableId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Segments_Shareables_ShareableId",
                table: "Segments");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Shareables_ShareableId",
                table: "Trainings");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Licenses_LicenseId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "HistoryLogs");

            migrationBuilder.DropTable(
                name: "LicensePermissionXPermissionFields");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "RolePermissionsXPermissionFields");

            migrationBuilder.DropTable(
                name: "ShareablesXGroups");

            migrationBuilder.DropTable(
                name: "LicensePermissions");

            migrationBuilder.DropTable(
                name: "PermissionFields");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "Shareables");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "PermissionActions");

            migrationBuilder.DropTable(
                name: "PermissionSubjects");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_LicenseId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_ShareableId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Segments_ShareableId",
                table: "Segments");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ShareableId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Editors_ShareableId",
                table: "Editors");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShareableId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "ShareableId",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "ShareableId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ShareableId",
                table: "Editors");

            migrationBuilder.AddColumn<Guid>(
                name: "OriginalId",
                table: "Trainings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OriginalVersionTS",
                table: "Trainings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Shared",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "VersionTS",
                table: "Trainings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "OriginalId",
                table: "Segments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OriginalVersionTS",
                table: "Segments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Shared",
                table: "Segments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "VersionTS",
                table: "Segments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Shared",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "VersionTS",
                table: "Exercises",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "Editors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SegmentId",
                table: "Editors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingId",
                table: "Editors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => new { x.GroupsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GroupUser_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharablesXGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SegmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrainingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharablesXGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SharablesXGroups_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Editors_ExerciseId",
                table: "Editors",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_SegmentId",
                table: "Editors",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_TrainingId",
                table: "Editors",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UsersId",
                table: "GroupUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_ExerciseId",
                table: "SharablesXGroups",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_GroupId",
                table: "SharablesXGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_SegmentId",
                table: "SharablesXGroups",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SharablesXGroups_TrainingId",
                table: "SharablesXGroups",
                column: "TrainingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Exercises_ExerciseId",
                table: "Editors",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Segments_SegmentId",
                table: "Editors",
                column: "SegmentId",
                principalTable: "Segments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_Trainings_TrainingId",
                table: "Editors",
                column: "TrainingId",
                principalTable: "Trainings",
                principalColumn: "Id");
        }
    }
}
