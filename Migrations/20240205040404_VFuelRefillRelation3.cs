using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class VFuelRefillRelation3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceTypes_TypeNameId",
                table: "VehicleMaintenances");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenances_TypeNameId",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TypeNameId",
                table: "VehicleMaintenances");

            migrationBuilder.RenameColumn(
                name: "FuelType",
                table: "FuelRefills",
                newName: "FType");

            migrationBuilder.AddColumn<int>(
                name: "FuelRefillId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_FuelRefillId",
                table: "Vehicles",
                column: "FuelRefillId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMaintenances_Id",
                table: "VehicleMaintenances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceTypes_Id",
                table: "VehicleMaintenances",
                column: "Id",
                principalTable: "VehicleMaintenanceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_FuelRefills_FuelRefillId",
                table: "Vehicles",
                column: "FuelRefillId",
                principalTable: "FuelRefills",
                principalColumn: "FuelRefillId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleMaintenances_VehicleMaintenanceTypes_Id",
                table: "VehicleMaintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_FuelRefills_FuelRefillId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_FuelRefillId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMaintenances_Id",
                table: "VehicleMaintenances");

            migrationBuilder.DropColumn(
                name: "FuelRefillId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "FType",
                table: "FuelRefills",
                newName: "FuelType");

            migrationBuilder.AddColumn<string>(
                name: "FuelType",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

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
    }
}
