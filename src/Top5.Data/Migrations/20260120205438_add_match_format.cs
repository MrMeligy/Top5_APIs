using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Top5.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_match_format : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "matchFormat",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "matchFormat",
                table: "Matches");
        }
    }
}
