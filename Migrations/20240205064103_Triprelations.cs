using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class Triprelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TripId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TripId",
                table: "Vehicles",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Trips_TripId",
                table: "Vehicles",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Trips_TripId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_TripId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Vehicles");
        }
    }
}
