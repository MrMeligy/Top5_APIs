using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Top5.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    picUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    level = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: false),
                    dob = table.Column<DateOnly>(type: "date", nullable: false),
                    goals = table.Column<int>(type: "int", nullable: false),
                    assists = table.Column<int>(type: "int", nullable: false),
                    matches = table.Column<int>(type: "int", nullable: false),
                    saves = table.Column<int>(type: "int", nullable: false),
                    rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    captinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    picUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    matches = table.Column<int>(type: "int", nullable: false),
                    goals = table.Column<int>(type: "int", nullable: false),
                    goalsAgainest = table.Column<int>(type: "int", nullable: false),
                    wins = table.Column<int>(type: "int", nullable: false),
                    loses = table.Column<int>(type: "int", nullable: false),
                    pitch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cityRank = table.Column<int>(type: "int", nullable: false),
                    countryRank = table.Column<int>(type: "int", nullable: false),
                    points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.id);
                    table.ForeignKey(
                        name: "FK_Teams_Players_captinId",
                        column: x => x.captinId,
                        principalTable: "Players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    homeTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    awayTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    homeScore = table.Column<int>(type: "int", nullable: false),
                    awayScore = table.Column<int>(type: "int", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pitch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kickOff = table.Column<DateTime>(type: "datetime2", nullable: false),
                    statues = table.Column<int>(type: "int", nullable: false),
                    Teamid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.id);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Teamid",
                        column: x => x.Teamid,
                        principalTable: "Teams",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_awayTeamId",
                        column: x => x.awayTeamId,
                        principalTable: "Teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_homeTeamId",
                        column: x => x.homeTeamId,
                        principalTable: "Teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamPlayers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    playerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    teamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    goals = table.Column<int>(type: "int", nullable: false),
                    assists = table.Column<int>(type: "int", nullable: false),
                    saves = table.Column<int>(type: "int", nullable: false),
                    matches = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPlayers", x => x.id);
                    table.ForeignKey(
                        name: "FK_TeamPlayers_Players_playerId",
                        column: x => x.playerId,
                        principalTable: "Players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamPlayers_Teams_teamId",
                        column: x => x.teamId,
                        principalTable: "Teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    matchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    playerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    teamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    goals = table.Column<int>(type: "int", nullable: false),
                    assists = table.Column<int>(type: "int", nullable: false),
                    saves = table.Column<int>(type: "int", nullable: false),
                    rate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayers", x => x.id);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Matches_matchId",
                        column: x => x.matchId,
                        principalTable: "Matches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Players_playerId",
                        column: x => x.playerId,
                        principalTable: "Players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Teams_teamId",
                        column: x => x.teamId,
                        principalTable: "Teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_awayTeamId",
                table: "Matches",
                column: "awayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_homeTeamId",
                table: "Matches",
                column: "homeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Teamid",
                table: "Matches",
                column: "Teamid");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_matchId",
                table: "MatchPlayers",
                column: "matchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_playerId",
                table: "MatchPlayers",
                column: "playerId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_teamId",
                table: "MatchPlayers",
                column: "teamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_playerId",
                table: "TeamPlayers",
                column: "playerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPlayers_teamId",
                table: "TeamPlayers",
                column: "teamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_captinId",
                table: "Teams",
                column: "captinId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchPlayers");

            migrationBuilder.DropTable(
                name: "TeamPlayers");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
