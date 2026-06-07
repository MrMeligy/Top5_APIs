using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Top5.Data.Migrations
{
    /// <inheritdoc />
    public partial class addReservationModelToMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Matches_ReservationId",
                table: "Matches",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Reservations_ReservationId",
                table: "Matches",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Reservations_ReservationId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_ReservationId",
                table: "Matches");
        }
    }
}
