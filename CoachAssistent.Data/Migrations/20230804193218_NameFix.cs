using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachAssistent.Data.Migrations
{
    /// <inheritdoc />
    public partial class NameFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharablesXGroups_Groups_GroupId",
                table: "SharablesXGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SharablesXGroups_Shareables_ShareableId",
                table: "SharablesXGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharablesXGroups",
                table: "SharablesXGroups");

            migrationBuilder.RenameTable(
                name: "SharablesXGroups",
                newName: "ShareablesXGroups");

            migrationBuilder.RenameIndex(
                name: "IX_SharablesXGroups_ShareableId",
                table: "ShareablesXGroups",
                newName: "IX_ShareablesXGroups_ShareableId");

            migrationBuilder.RenameIndex(
                name: "IX_SharablesXGroups_GroupId",
                table: "ShareablesXGroups",
                newName: "IX_ShareablesXGroups_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShareablesXGroups",
                table: "ShareablesXGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareablesXGroups_Groups_GroupId",
                table: "ShareablesXGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShareablesXGroups_Shareables_ShareableId",
                table: "ShareablesXGroups",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareablesXGroups_Groups_GroupId",
                table: "ShareablesXGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ShareablesXGroups_Shareables_ShareableId",
                table: "ShareablesXGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShareablesXGroups",
                table: "ShareablesXGroups");

            migrationBuilder.RenameTable(
                name: "ShareablesXGroups",
                newName: "SharablesXGroups");

            migrationBuilder.RenameIndex(
                name: "IX_ShareablesXGroups_ShareableId",
                table: "SharablesXGroups",
                newName: "IX_SharablesXGroups_ShareableId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareablesXGroups_GroupId",
                table: "SharablesXGroups",
                newName: "IX_SharablesXGroups_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharablesXGroups",
                table: "SharablesXGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharablesXGroups_Groups_GroupId",
                table: "SharablesXGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharablesXGroups_Shareables_ShareableId",
                table: "SharablesXGroups",
                column: "ShareableId",
                principalTable: "Shareables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
