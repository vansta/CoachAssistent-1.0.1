using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoachAssistent.Data.Migrations
{
    public partial class Version : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OriginalId",
                table: "Trainings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OriginalVersion",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OriginalId",
                table: "Segments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OriginalVersion",
                table: "Segments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Segments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OriginalId",
                table: "Exercises",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OriginalVersion",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "OriginalVersion",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "OriginalId",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "OriginalVersion",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Segments");

            migrationBuilder.DropColumn(
                name: "OriginalId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "OriginalVersion",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Exercises");
        }
    }
}
