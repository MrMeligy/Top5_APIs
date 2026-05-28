using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Top5.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUniqeIndexForMatchPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MatchPlayers_matchId",
                table: "MatchPlayers");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_matchId_playerId",
                table: "MatchPlayers",
                columns: new[] { "matchId", "playerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MatchPlayers_matchId_playerId",
                table: "MatchPlayers");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_matchId",
                table: "MatchPlayers",
                column: "matchId");
        }
    }
}
