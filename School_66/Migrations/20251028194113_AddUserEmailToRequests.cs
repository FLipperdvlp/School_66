using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_66.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEmailToRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "StudentForms",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "StudentForms",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Requests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "StudentForms");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "StudentForms");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Requests");
        }
    }
}
