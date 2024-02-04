using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class VMSMaintenance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleMaintenances",
                columns: table => new
                {
                    VehicleMaintenanceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaintenanceStatus = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartsReplaced = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ServiceProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SpecialNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMaintenances", x => x.VehicleMaintenanceId);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleRegistrationNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FuelType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleMaintenances");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
