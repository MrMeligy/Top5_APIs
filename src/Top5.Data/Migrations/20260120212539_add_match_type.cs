using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Top5.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_match_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isComptitve",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isComptitve",
                table: "Matches");
        }
    }
}
