using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEngilish.Migrations
{
    /// <inheritdoc />
    public partial class FileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "EngilishPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileNameInSystem",
                table: "EngilishPrograms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "EngilishPrograms");

            migrationBuilder.DropColumn(
                name: "FileNameInSystem",
                table: "EngilishPrograms");
        }
    }
}
