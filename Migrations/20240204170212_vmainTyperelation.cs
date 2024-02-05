using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class vmainTyperelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VehicleMaintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeNameId",
                table: "VehicleMaintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_TypeNameId",
                table: "VehicleMaintenances",
                column: "TypeNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceTypes_TypeNameId",
                table: "VehicleMaintenances",
                column: "TypeNameId",
                principalTable: "VehicleMaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceTypes_TypeNameId",
                table: "VehicleMaintenances");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenances_TypeNameId",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "TypeNameId",
                table: "VehicleMaintenances");
        }
    }
}
