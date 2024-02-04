using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetPulse_BackEndDevelopment.Migrations
{
    public partial class vmanurelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManufactureId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ManufactureId",
                table: "Vehicles",
                column: "ManufactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Manufacture_ManufactureId",
                table: "Vehicles",
                column: "ManufactureId",
                principalTable: "Manufacture",
                principalColumn: "ManufactureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Manufacture_ManufactureId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_ManufactureId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ManufactureId",
                table: "Vehicles");
        }
    }
}
