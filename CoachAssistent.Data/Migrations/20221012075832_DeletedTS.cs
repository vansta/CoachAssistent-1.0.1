using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachAssistent.Data.Migrations
{
    public partial class DeletedTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTS",
                table: "Trainings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTS",
                table: "Segments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedTS",
                table: "Exercises",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_OriginalId",
                table: "Exercises",
                column: "OriginalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Exercises_OriginalId",
                table: "Exercises",
                column: "OriginalId",
                principalTable: "Exercises",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Exercises_OriginalId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_OriginalId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "DeletedTS",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "DeletedTS",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "DeletedTS",
                table: "Exercises");
        }
    }
}
