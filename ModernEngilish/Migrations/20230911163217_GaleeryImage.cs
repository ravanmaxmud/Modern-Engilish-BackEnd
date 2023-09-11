using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernEngilish.Migrations
{
    /// <inheritdoc />
    public partial class GaleeryImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileNameInSystem",
                table: "Galleries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "FileNameInSystem",
                table: "Galleries");
        }
    }
}
