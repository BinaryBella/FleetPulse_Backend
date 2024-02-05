using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class AccidentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccidentId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AccidentId",
                table: "Vehicles",
                column: "AccidentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Accidents_AccidentId",
                table: "Vehicles",
                column: "AccidentId",
                principalTable: "Accidents",
                principalColumn: "AccidentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Accidents_AccidentId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_AccidentId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "AccidentId",
                table: "Vehicles");
        }
    }
}
