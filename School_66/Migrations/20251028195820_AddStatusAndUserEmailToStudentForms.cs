using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School_66.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusAndUserEmailToStudentForms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "StudentForms",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "StudentForms",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "StudentForms",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StudentForms");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "StudentForms");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "StudentForms");
        }
    }
}
