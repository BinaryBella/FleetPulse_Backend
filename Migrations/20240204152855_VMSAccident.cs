using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class VMSAccident : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accidents",
                columns: table => new
                {
                    AccidentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photos = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    SpecialNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Loss = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DriverInjuredStatus = table.Column<bool>(type: "bit", nullable: false),
                    HelperInjuredStatus = table.Column<bool>(type: "bit", nullable: false),
                    VehicleDamagedStatus = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accidents", x => x.AccidentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accidents");
        }
    }
}
