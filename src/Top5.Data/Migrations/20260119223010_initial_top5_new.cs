using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Top5.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial_top5_new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assists",
                table: "TeamPlayers");

            migrationBuilder.DropColumn(
                name: "goals",
                table: "TeamPlayers");

            migrationBuilder.DropColumn(
                name: "matches",
                table: "TeamPlayers");

            migrationBuilder.DropColumn(
                name: "saves",
                table: "TeamPlayers");

            migrationBuilder.DropColumn(
                name: "assists",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "goals",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "matches",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "rate",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "saves",
                table: "Players");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "assists",
                table: "TeamPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "goals",
                table: "TeamPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "matches",
                table: "TeamPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "saves",
                table: "TeamPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assists",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "goals",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "matches",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "rate",
                table: "Players",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "saves",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
