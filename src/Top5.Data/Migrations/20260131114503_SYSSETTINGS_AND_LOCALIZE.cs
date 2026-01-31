using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Top5.Data.Migrations
{
    /// <inheritdoc />
    public partial class SYSSETTINGS_AND_LOCALIZE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "cityRank",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "pitch",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "nationality",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "country",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "pitch",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "countryRank",
                table: "Teams",
                newName: "Rank");

            migrationBuilder.CreateTable(
                name: "SysSettings",
                columns: table => new
                {
                    PitchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysSettings");

            migrationBuilder.RenameColumn(
                name: "Rank",
                table: "Teams",
                newName: "countryRank");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "cityRank",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "pitch",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "gender",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "nationality",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "pitch",
                table: "Matches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
